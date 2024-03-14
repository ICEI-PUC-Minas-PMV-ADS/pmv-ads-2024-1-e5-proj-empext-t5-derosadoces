using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Controllers
{
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly Carrinho _carrinhoCompra;
        private readonly IProductService _productService;
        public CarrinhoController(Carrinho carrinhoCompra, IProductService productService)
        {
            _carrinhoCompra = carrinhoCompra;
            _productService = productService;
        }
        [Authorize]
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetItemCarrinhos();
            _carrinhoCompra.ListItemCarrinho = itens;
            var carrinhoViewModel = new CarrinhoViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetTotalCarrinho()
            };
            return View(carrinhoViewModel);

        }

        public async Task<IActionResult> AdicionarNoCarrinho(int cod_produto)
        {
            if (User.Identity.IsAuthenticated)
            {
                var produtoSelecionado = await _productService.GetById(cod_produto);
                if (produtoSelecionado is not null)
                {
                    _carrinhoCompra.AdicionarNoCarrinho(produtoSelecionado.Value);
                    return RedirectToAction("Index", "Carrinho");
                }
                else
                {
                    return Problem();
                }

            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }


        }
        [Authorize]

        public async Task<IActionResult> RemoverItemNoCarrinhoCompra(int cod_produto)
        {
            var produtoSelecionado = await _productService.GetById(cod_produto);
            if (produtoSelecionado is not null)
            {
                _carrinhoCompra.RemoverDoCarrinho(produtoSelecionado.Value);
                return RedirectToAction("Index");
            }
            else
            {
                return produtoSelecionado.Result;
            }
        }
        [Authorize]

        public IActionResult LimparCarrinho()
        {
            _carrinhoCompra.LimparCarrinho();
            return RedirectToAction("Index");
        }
    }
}
