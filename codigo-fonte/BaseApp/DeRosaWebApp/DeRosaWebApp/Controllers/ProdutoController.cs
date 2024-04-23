using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Controllers
{
    public class ProdutoController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly IProductService _produtos;
        private readonly ICategoriaService _categorias;
        public ProdutoController(IProductService produtos, ICategoriaService categorias)
        {
            _produtos = produtos;
            _categorias = categorias;
        }
        #endregion
        #region Produto Detalhe
        [HttpGet]
        public async Task<IActionResult> ProdutoDetalhe(int cod_produto)
        {
            var produto = await _produtos.GetById(cod_produto);
            return View(produto);
        }
        #endregion
        #region Todos os produtos
        public async Task<IActionResult> Produtos()
        {
            var produtos = await _produtos.GetAll();
            if (produtos is not null)
            {
                return View(produtos.Value);
            }

            return produtos.Result;
        }
        #endregion
        #region Produto pela categoria
        public async Task<IActionResult> ProdutosByCategoria(int idCategoria)
        {
            var prodCatg = await _categorias.GetByCategoria(idCategoria);
            if (prodCatg is null)
            {
                ModelState.AddModelError("Erro", "Nenhum produto com essa categoria");
            }
            ProdutoViewModel produtoViewModel = new ProdutoViewModel()
            {
                Produtos = prodCatg.Value
            };
            return View(produtoViewModel);
        }
    }
    #endregion
}
