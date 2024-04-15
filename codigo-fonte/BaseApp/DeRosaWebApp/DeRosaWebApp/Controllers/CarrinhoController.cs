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
        #region Construtor, propriedades e injeção de dependência
        private readonly Carrinho _carrinhoCompra;
        private readonly IProductService _productService;
        public CarrinhoController(Carrinho carrinhoCompra, IProductService productService)
        {
            _carrinhoCompra = carrinhoCompra;
            _productService = productService;
        }
        #endregion
        #region Index
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
        #endregion
        #region Adicionar no carinho
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarNoCarrinho(int cod_produto, int quantidadeAdicionar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var produtoSelecionado = await _productService.GetById(cod_produto);
                if (produtoSelecionado is not null)
                {
                    _carrinhoCompra.AdicionarNoCarrinho(produtoSelecionado.Value, quantidadeAdicionar);
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
        #endregion
        #region Remover item no carrinho
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
        #endregion
        #region Limpar carrinho
        [Authorize]

        public IActionResult LimparCarrinho()
        {
            _carrinhoCompra.LimparCarrinho();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
