using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.Models;
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
        private readonly IPedidoService _pedidoService;

        public ProdutoController(IProductService produtos, ICategoriaService categorias, IPedidoService pedidoService)
        {
            _produtos = produtos;
            _categorias = categorias;
            _pedidoService = pedidoService; 
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
            var categorias = _categorias.GetAllCategorias();
            if (produtos is not null && categorias is not null)
            {
                TodosProdutosViewModel todosProdutosViewModel = new TodosProdutosViewModel()
                {
                    _Produtos = produtos.Value,
                    _Categorias = categorias
                };
                return View(todosProdutosViewModel);
            }
            ViewBag.Title = "Todos os Produtos";
            return produtos.Result;
        }
        #endregion
        #region Produto pela categoria
        public async Task<IActionResult> ProdutoByCategoria(int cod_categoria)
        {
            var prodCatg = await _categorias.GetByCategoria(cod_categoria);
            string categoriaNome = _categorias.GetNameById(cod_categoria);

            var categorias = _categorias.GetAllCategorias();
            if (prodCatg is null)
            {
                ModelState.AddModelError("Erro", "Nenhum produto com essa categoria");
            }
            TodosProdutosViewModel todosProdutosViewModel = new TodosProdutosViewModel()
            {
                _Produtos = prodCatg.Value,
                _Categorias = categorias
            };
            ViewBag.Title = $"Produtos com a categoria {categoriaNome}";
            return View("Produtos",todosProdutosViewModel);
        }
    }
    #endregion
}
