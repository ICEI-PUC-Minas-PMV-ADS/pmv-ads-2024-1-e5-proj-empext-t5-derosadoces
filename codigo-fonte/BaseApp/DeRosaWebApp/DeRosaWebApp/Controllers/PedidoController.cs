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
        private readonly UserManager<IdentityUser> _userMananger;
        public PedidoController(Carrinho carrinho, IPedidoService pedidoService, UserManager<IdentityUser> userManager, IProductService productService)
        {
            _carrinho = carrinho;
            _pedidoService = pedidoService;
            _userMananger = userManager;
            _productService = productService;
        }
        #endregion
        #region Meus pedidos
        [HttpGet]
        public async Task<IActionResult> MeusPedidos(string user_id)
        {
            try
            {
                MeusPedidosViewModel pedidos = await _pedidoService.GetMeusPedidos(user_id);
                await _pedidoService.VerificarPedidosExpirados();
                await Task.Delay(1000); 
                return View(pedidos);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View("Erro");
            }

        }
        #endregion
        #region Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        #endregion
        
        #region Checkout completo
        [HttpPost]
        public async Task<IActionResult> Checkout(Pedido pedido)
        {
            int totalItemsPedido = 0;
            double precoTotalPedido = 0.0;
            List<ItemCarrinho> itemCarrinhos = _carrinho.GetItemCarrinhos();
            _carrinho.ListItemCarrinho = itemCarrinhos;

            foreach (var item in itemCarrinhos)
            {
                totalItemsPedido += item.QntProduto;
                precoTotalPedido += item.QntProduto * item.Produto.Preco;
            }
            if (_carrinho.ListItemCarrinho.Count == 0)
            {
                ModelState.AddModelError("", "Resumo");
            }

            if (ModelState.IsValid)
            {

                var user_id = _userMananger.GetUserId(User);
                pedido.TotalItensPedido = totalItemsPedido;
                pedido.TotalPedido = precoTotalPedido;
                pedido.DataExpiracao = pedido.DataPedido.AddMinutes(30);
                pedido.Id_User = user_id;
                var result = await _pedidoService.CriarPedido(pedido, user_id);
                if (result is not null)
                {
                    ViewBag.CheckoutCompletoMensagem = "Resumo do pedido";
                    ViewBag.TotalPedido = _carrinho.GetTotalCarrinho();
                   
                    return View("Resumo", pedido);

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

                    var pedidoDetalhe =  await _pedidoService.DetalhePedidoList(cod_pedido);


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
            catch(NullReferenceException)
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
            var pedido =  await _pedidoService.GetById(cod_pedido);
            if (pedido != null)
            {
               await  _pedidoService.Remove(pedido.Value);
                return RedirectToAction("Index", "Home");
            }
            return new BadRequestObjectResult("Erro ao remover pedido");
        }
        #endregion
        #region Erro
        public IActionResult Erro()
        {
            return View();
        }
        #endregion

    }
}
