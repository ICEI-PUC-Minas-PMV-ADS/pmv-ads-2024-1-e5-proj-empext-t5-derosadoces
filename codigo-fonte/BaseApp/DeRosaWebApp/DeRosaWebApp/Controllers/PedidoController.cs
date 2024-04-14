using DeRosaWebApp.BusinessRules.Interfaces;
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
        private readonly UserManager<IdentityUser> _userMananger;
        public PedidoController(Carrinho carrinho, IPedidoService pedidoService, UserManager<IdentityUser> userManager, IProductService productService, IPedidoRules pedidoRules, ICategoriaService categoriaService)
        {
            _carrinho = carrinho;
            _pedidoService = pedidoService;
            _userMananger = userManager;
            _productService = productService;
            _pedidoRules = pedidoRules;
            _categoriaService = categoriaService;
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
        public async Task<IActionResult> Checkout()
        {
            var items = _carrinho.GetItemCarrinhos();
            PedidoCheckoutViewModel pedido = new();
            foreach (var item in items)
            {
                var categoria = await _categoriaService.GetById(item.Produto.IdCategoria);
                if (categoria is not null && categoria.CategoriaNome.Equals("Comemorativos"))
                {
                    pedido.PedidosComemorativos.Add(item.Produto);
                }
            }

            return View(pedido);
        }
        #endregion

        #region Checkout completo
        [HttpPost]
        public async Task<IActionResult> Checkout(PedidoCheckoutViewModel pedido)
        {
            try
            {
                int totalItemsPedido = 0;
                double precoTotalPedido = 0.0;
                List<ItemCarrinho> itemCarrinhos = _carrinho.GetItemCarrinhos();
                _carrinho.ListItemCarrinho = itemCarrinhos;

                foreach (var item in itemCarrinhos)
                {
                    totalItemsPedido += item.QntProduto;
                    precoTotalPedido += item.QntProduto * item.Produto.Preco;
                    pedido._Pedido.ProdutosPedido.Add(item);
                    var categoria = await _categoriaService.GetById(item.Produto.IdCategoria);
                    if (categoria is not null && categoria.CategoriaNome.Equals("Comemorativos"))
                    {
                        pedido.PedidosComemorativos.Add(item.Produto);
                    }

                }
                if (_carrinho.ListItemCarrinho.Count == 0)
                {
                    ModelState.AddModelError("", "Resumo");
                }

                if (ModelState.IsValid)
                {

                    var user_id = _userMananger.GetUserId(User);
                    pedido._Pedido.TotalItensPedido = totalItemsPedido;
                    pedido._Pedido.TotalPedido = precoTotalPedido;
                    pedido._Pedido.DataExpiracao = pedido._Pedido.DataPedido.AddMinutes(30);
                    pedido._Pedido.Id_User = user_id;

                    _pedidoRules.VerificaComemorativosSeteDiasAntecedencia(pedido.PedidosComemorativos, pedido._Pedido.DataParaEntregar);


                    var result = await _pedidoService.CriarPedido(pedido._Pedido, user_id);
                    if (result is not null)
                    {
                        ViewBag.CheckoutCompletoMensagem = "Resumo do pedido";
                        ViewBag.TotalPedido = precoTotalPedido;

                        return View("Resumo", pedido._Pedido);

                    }
                    else
                    {
                        ModelState.AddModelError("Erro", "Erro ao realizar pedido!");
                        return View(pedido);
                    }
                }
                else
                {
                    ModelState.AddModelError("ErroDados", "Verifique todos os campos e tente novamente!");
                    return View(pedido);
                }
            }
            catch (PedidoExceptionValidation ex)
            {
                ModelState.AddModelError("ErroDados", ex.Message);
                return View(pedido);
            }

        }
        #endregion
        #region Resumo
        public async Task<ActionResult<Pedido>> Resumo(Pedido pedido, int cod_pedido)
        {
            try
            {
                await _pedidoService.VerificarPedidosExpirados();

                if (pedido._PedidoDetalhes is null || pedido is null)
                {
                    var getPedido = await _pedidoService.GetById(cod_pedido);
                    pedido.Id_User = getPedido.Value.Id_User;
                    pedido.Nome = getPedido.Value.Nome;
                    pedido.DataPedido = getPedido.Value.DataPedido;
                    pedido.DataExpiracao = getPedido.Value.DataExpiracao;
                    pedido.Cep = getPedido.Value.Cep;
                    pedido.Rua = getPedido.Value.Rua;
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



        }
        #endregion
        #region Remover pedido
        [HttpPost]
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
