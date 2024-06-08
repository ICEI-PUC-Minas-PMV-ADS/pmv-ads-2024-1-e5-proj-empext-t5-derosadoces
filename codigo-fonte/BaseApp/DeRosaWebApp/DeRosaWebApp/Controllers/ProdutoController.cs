using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;

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
            var categoria = await _categorias.GetById(produto.Value.IdCategoria);
            ProdutoDetalheViewModel produtoDetalheViewModel = new ProdutoDetalheViewModel()
            {
                Produto = produto,
                Categoria = categoria
            };
            return View(produtoDetalheViewModel);
        }
        #endregion
        #region Todos os produtos
        [Route("Produtos")]
        [HttpGet]
        public async Task<IActionResult> Produtos(string filter, int pageIndex = 1, string sort = "Nome")
        {
            var prodCatg = _produtos.PaginationProduct();
            var categorias = _categorias.GetAllCategorias();
            if (prodCatg is null)
            {
                ModelState.AddModelError("Erro", "Nenhum produto com essa categoria");
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                prodCatg = prodCatg.Where(p => p.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(prodCatg, 5, pageIndex, sort, "Nome");
            model.Action = "Produtos"; 
            model.RouteValue = new RouteValueDictionary {
            { "filter", filter },
            { "sort", sort }
            };
            TodosProdutosViewModel todosProdutosViewModel = new TodosProdutosViewModel()
            {
                _Produtos = model,
                _Categorias = categorias,
                TotalRecordCount = model.Count
            };
            return View("Produtos", todosProdutosViewModel);
        }
        #endregion
        #region Produto pela categoria
        [Route("Produtos/Categoria/{cod_categoria}")]
        public async Task<IActionResult> ProdutoByCategoria(int cod_categoria, string filter, int pageIndex = 1, string sort = "Nome")
        {
            var prodCatg = _produtos.GetProdutosCategoriaPagination(cod_categoria);
            string categoriaNome = "";
            var categorias = _categorias.GetAllCategorias();

            if (prodCatg is null)
            {
                ModelState.AddModelError("Erro", "Nenhum produto com essa categoria");
            }
            if (cod_categoria == 0)
            {
                categoriaNome = "Produto não encontrado!";
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                prodCatg = prodCatg.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(prodCatg, 5, pageIndex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            TodosProdutosViewModel todosProdutosViewModel = new TodosProdutosViewModel()
            {
                _Produtos = model,
                _Categorias = categorias,
                TotalRecordCount = model.Count
            };

            return View("Produtos", todosProdutosViewModel);
        }
    }
    #endregion
}
