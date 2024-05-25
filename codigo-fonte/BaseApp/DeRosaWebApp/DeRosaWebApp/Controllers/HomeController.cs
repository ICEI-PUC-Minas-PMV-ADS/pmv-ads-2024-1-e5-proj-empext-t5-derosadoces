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