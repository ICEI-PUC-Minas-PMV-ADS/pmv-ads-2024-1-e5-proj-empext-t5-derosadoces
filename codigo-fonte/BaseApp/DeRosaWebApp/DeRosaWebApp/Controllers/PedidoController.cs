﻿using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.BusinessRules.Validations;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Controllers
{
    [Authorize]

    public class PedidoController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly Carrinho _carrinho;
        private readonly IPedidoService _pedidoService;
        private readonly IProductService _productService;
        private readonly IPedidoRules _pedidoRules;
        private readonly ICategoriaService _categoriaService;
        private readonly IEnderecoService _enderecoService;
        private readonly IClienteService _clienteService;
        private readonly UserManager<IdentityUser> _userMananger;
        public PedidoController(Carrinho carrinho, IPedidoService pedidoService, UserManager<IdentityUser> userManager, IProductService productService,
            IPedidoRules pedidoRules, ICategoriaService categoriaService, IEnderecoService enderecoService, IClienteService clienteService)
        {
            _carrinho = carrinho;
            _pedidoService = pedidoService;
            _userMananger = userManager;
            _productService = productService;
            _pedidoRules = pedidoRules;
            _categoriaService = categoriaService;
            _enderecoService = enderecoService;
            _clienteService = clienteService;
        }
        #endregion
        #region Meus pedidos
        [HttpGet]
        public async Task<IActionResult> MeusPedidos(string user_id)
        {

            MeusPedidosViewModel pedidos = await _pedidoService.GetMeusPedidos(user_id);
            await Task.Delay(1000);
            return View(pedidos);

        }
        #endregion
        #region Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            var produtosCarrinho = _carrinho.GetItemCarrinhos();
            foreach (var produto in produtosCarrinho)
            {
                var categoria = _categoriaService.GetById(produto.Produto.IdCategoria);
                if (categoria is not null && !categoria.Result.CategoriaNome.Equals("Comemorativos"))
                {
                    DateTime dataAtual = DateTime.Today;
                    int diasAteSexta = ((int)DayOfWeek.Friday - (int)dataAtual.DayOfWeek + 7) % 7;
                    DateTime proximaSexta = DateTime.Today;
                    if (diasAteSexta <= 1)
                    {
                        proximaSexta = dataAtual.AddDays(diasAteSexta + 7);
                    }
                    else
                    {
                        proximaSexta = dataAtual.AddDays(diasAteSexta);
                    }
                    ViewBag.EntregaPedidoNormal = true;
                    ViewBag.DataEntrega = proximaSexta.ToString().Substring(0, 10);
                }
            }
            return View();
        }

        #endregion
        #region Atualizar forma de entrega
        [HttpPost]
        public IActionResult AtualizarFormaDeEntrega(string formaEntrega)
        {
            var retiradaLocal = formaEntrega == "local";
            HttpContext.Session.SetString("RetiradaLocal", retiradaLocal ? "true" : "false");
            return Json(new { success = true, message = "Forma de entrega atualizada.", currentSetting = retiradaLocal });
        }

        #endregion

        #region Checkout completo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(DateTime dataParaEntregar)
        {
            try
            {
               
                int totalItemsPedido = 0;
                double precoTotalPedido = 0.0;

                Pedido pedido = new Pedido();

                List<ItemCarrinho> itemCarrinhos = _carrinho.GetItemCarrinhos();
                List<Produto> ProdutosComemorativos = new List<Produto>();
                _carrinho.ListItemCarrinho = itemCarrinhos;
                foreach (var item in itemCarrinhos)
                {
                    totalItemsPedido += item.QntProduto;
                    precoTotalPedido += item.QntProduto * item.Produto.Preco;
                    pedido.ProdutosPedido.Add(item);

                    var categoria = await _categoriaService.GetById(item.Produto.IdCategoria);
                    if (categoria is not null && categoria.CategoriaNome.Equals("Comemorativos"))
                    {
                        ProdutosComemorativos.Add(item.Produto);
                    }
                }
                if (_carrinho.ListItemCarrinho.Count == 0)
                {
                    ModelState.AddModelError("", "Resumo");
                }
                var getString = HttpContext.Session.GetString("RetiradaLocal");
                var user_id = _userMananger.GetUserId(User);
                var getCliente = await _clienteService.GetClienteByUserId(user_id);

                Endereco getEndereco;

                if (getString == "false")
                {
                    getEndereco = await _enderecoService.GetEnderecoById(getCliente.IdEndereco);
                }
                else
                {
                    getEndereco = new Endereco();
                }

                await _enderecoService.GetEnderecoById(getCliente.IdEndereco);
                string valorSessaoAgendado = HttpContext.Session.GetString("Agendado");



                if (string.Equals(valorSessaoAgendado, "true"))
                {
                    pedido.Agendado = true;
                }
                else
                {
                    pedido.Agendado = false;
                }

                if (getString == "true")
                {
                    pedido.Logradouro = "Alameda Serra dos Ipês Rosa";
                    pedido.Bairro = "Bairro Serras Altas";
                    pedido.Cep = "37.718-522";
                    pedido.Cidade = "Poços de Caldas";
                    pedido.Numero = 81;
                    pedido.Complemento = "Condomínio Residencial Serras Altas Golf State";
                    pedido.Estado = "MG";
                }
                else
                {
                    pedido.Logradouro = getEndereco.Logradouro;
                    pedido.Bairro = getEndereco.Bairro;
                    pedido.Cep = getEndereco.CEP;
                    pedido.Cidade = getEndereco.Cidade;
                    pedido.Numero = getEndereco.Numero;
                    pedido.Complemento = getEndereco.Complemento;
                    pedido.Estado = getEndereco.UF;
                }
               
                pedido.Nome = getCliente.Nome;
                pedido.Telefone = getCliente.Telefone;
                pedido.TotalItensPedido = totalItemsPedido;
                pedido.TotalPedido = precoTotalPedido;
                pedido.DataExpiracao = pedido.DataPedido.AddMinutes(30);
                pedido.Id_User = user_id;

                if (!string.Equals(getString, "true"))
                {
                    pedido.TotalPedido += 20.00;
                    pedido.Frete = 20.00;
                }
                if (dataParaEntregar.ToString() == "01/01/0001 00:00:00")
                {
                    DateTime dataAtual = DateTime.Today;
                    int diasAteSexta = ((int)DayOfWeek.Friday - (int)dataAtual.DayOfWeek + 7) % 7;
                    DateTime proximaSexta = dataAtual;
                    if (diasAteSexta <= 1)
                    {
                        proximaSexta = dataAtual.AddDays(diasAteSexta + 7);
                    }
                    else
                    {
                        proximaSexta = dataAtual.AddDays(diasAteSexta);

                    }
                    pedido.DataParaEntregar = proximaSexta;

                }
                else
                {
                    pedido.DataParaEntregar = dataParaEntregar;
                }

                _pedidoRules.VerificaCidade(pedido.Cidade);
                _pedidoRules.VerificaComemorativosSeteDiasAntecedencia(ProdutosComemorativos, dataParaEntregar);

                var result = await _pedidoService.CriarPedido(pedido, user_id);
                if (result is not null)
                {

                    ViewBag.CheckoutCompletoMensagem = "Resumo do pedido";
                    ViewBag.TotalPedido = precoTotalPedido + pedido.Frete;

                    return View("Resumo", pedido);
                }
                else
                {
                    ModelState.AddModelError("Erro", "Erro ao realizar pedido!");
                    return View(pedido);
                }
            }
            catch (DeRosaExceptionValidation ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }

            catch (NullReferenceException)
            {
                ViewBag.Erro = "Volte a página anterior e selecione um endereço!";
                return View();
            }
        }

        #endregion
        #region Resumo
        public async Task<ActionResult<Pedido>> Resumo(Pedido pedido, int cod_pedido)
        {
            try
            {
                var id_user = _userMananger.GetUserId(User);
                _pedidoService.VerificarProprietarioDoPedido(id_user, cod_pedido);
                await _pedidoService.VerificarPedidosExpirados();

                if (pedido._PedidoDetalhes is null || pedido is null)
                {
                    var getPedido = await _pedidoService.GetById(cod_pedido);
                    pedido.Id_User = getPedido.Value.Id_User;
                    pedido.Nome = getPedido.Value.Nome;
                    pedido.DataPedido = getPedido.Value.DataPedido;
                    pedido.DataParaEntregar = getPedido.Value.DataParaEntregar;
                    pedido.DataExpiracao = getPedido.Value.DataExpiracao;
                    pedido.Cep = getPedido.Value.Cep;
                    pedido.Numero = getPedido.Value.Numero;
                    pedido.Complemento = getPedido.Value.Complemento;
                    pedido.Bairro = getPedido.Value.Bairro;
                    pedido.Cidade = getPedido.Value.Cidade;
                    pedido.Estado = getPedido.Value.Estado;
                    pedido.Telefone = getPedido.Value.Telefone;
                    pedido.ProdutosPedido = getPedido.Value.ProdutosPedido;
                    pedido.TotalItensPedido = getPedido.Value.TotalItensPedido;
                    pedido.TotalPedido = getPedido.Value.TotalPedido;
                    pedido.Concluido = getPedido.Value.Concluido;
                    pedido.Entregue = getPedido.Value.Entregue;
                    pedido.Pago = getPedido.Value.Pago;
                    pedido.Frete = getPedido.Value.Frete;
                    pedido.Conjunto_IdProdutos = getPedido.Value.Conjunto_IdProdutos;

                    var pedidoDetalhe = await _pedidoService.DetalhePedidoList(cod_pedido);


                    foreach (var item in pedidoDetalhe)
                    {
                        var produto = await _productService.GetById(item.Cod_Produto);
                        item.Produto = produto.Value;
                    }
                    pedido._PedidoDetalhes = pedidoDetalhe;

                    ViewBag.CheckoutCompletoMensagem = "Resumo do pedido";
                    ViewBag.TotalPedido = pedido.TotalPedido;
                    await Task.Delay(1000);

                    return View(pedido);
                }
                return View(pedido);
            }
            catch (NullReferenceException)
            {
                ViewBag.Erro = "O pedido foi expirado.";
                return View("Erro");
            }
            catch (DeRosaExceptionValidation e)
            {
                ViewBag.Erro = e.Message;
                return View("Erro");
            }
        }
        #endregion
        #region Remover pedido
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Pedido>> RemovePedido(int cod_pedido)
        {
            var pedido = await _pedidoService.GetById(cod_pedido);
            if (pedido != null)
            {
                await _pedidoService.Remove(pedido.Value);
                return RedirectToAction("Index", "Home");
            }
            return new BadRequestObjectResult("Erro ao remover pedido");
        }
        #endregion
        #region Erro
        public IActionResult Erro(string message)
        {
            if (message is not null)
            {
                ViewBag.Erro = message;
            }
            return View();
        }
        #endregion


    }
}
