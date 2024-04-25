using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.BusinessRules.Validations;
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
        private readonly IProductRules _productRules;
        public CarrinhoController(Carrinho carrinhoCompra, IProductService productService, IProductRules productRules)
        {
            _carrinhoCompra = carrinhoCompra;
            _productService = productService;
            _productRules = productRules;

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
        #region Adicionar no carrinho
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarNoCarrinho(int cod_produto, int quantidadeAdicionar)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _productRules.VerificaQuantidadeEmEstoque(quantidadeAdicionar, cod_produto);

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

            catch (DeRosaExceptionValidation)
            {
                TempData["ErroAdicionar"] = "Quantidade superior a disponivel do produto, diminua a quantidade.";
                return RedirectToAction("ProdutoDetalhe", "Produto", new { cod_produto = cod_produto });
            }

        }
        #endregion
        #region
        public IActionResult Erro()
        {
            return View();
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
