using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Controllers
{
    [Authorize]

    public class PedidoController : Controller
    {
        private readonly Carrinho _carrinho;
        private readonly IPedidoService _pedidoService;
        public PedidoController(Carrinho carrinho, IPedidoService pedidoService)
        {
            _carrinho = carrinho;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
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
                ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um pedido?");
            }
            pedido.TotalItensPedido = totalItemsPedido;
            pedido.TotalPedido = precoTotalPedido;
            if (ModelState.IsValid)
            {
                var result = await _pedidoService.CriarPedido(pedido);
                if (result is not null)
                {
                    ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido ;)";
                    ViewBag.TotalPedido = _carrinho.GetTotalCarrinho();
                    _carrinho.LimparCarrinho();
                    return View("~/Views/Pedido/Resumo.cshtml", pedido);


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

    }
}
