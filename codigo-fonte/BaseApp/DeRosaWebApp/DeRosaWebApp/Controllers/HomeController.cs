using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics;

namespace DeRosaWebApp.Controllers
{
    public class HomeController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly IProductService _produtos;
        public HomeController(IProductService produtos)
        {
            _produtos = produtos;
        }
        #endregion
        #region Index
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var list = _produtos.PaginationProductHome();
            var qntProdutosExistentes = _produtos.VerifyQnt();
            if(qntProdutosExistentes == 0)
            {
                Produto TesteProduto = new Produto()
                {
                    Nome = "Bannofe",
                    DescricaoCurta = "Com uma camada de um creme de doce de leite, banana cortada em rodelas e, para finalizar com chave de ouro, um chantilly caseiro.",
                    ImagemUrl = "https://www.shutterstock.com/image-photo/banoffee-cake-banana-caramel-sauce-600nw-2184736287.jpg",
                    Preco = 24.50,
                    PrecoSecundario = 20.00M,
                    IdCategoria = 1,
                    EmEstoque = 20 
                };
                await _produtos.AddTestProduct(TesteProduto);
            }
            if (!string.IsNullOrWhiteSpace(filter))
            {
                list = list.Where(p => p.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(list, 6, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);

        }
        #region Sobre
        public IActionResult Sobre()
        {
            return View();
        }
        #endregion
    }
    #endregion
}