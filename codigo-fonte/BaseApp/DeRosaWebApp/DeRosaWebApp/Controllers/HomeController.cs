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
        private readonly IManageSite _manageSite;
        public HomeController(IProductService produtos, IManageSite manageSite)
        {
            _produtos = produtos;
            _manageSite = manageSite;   
        }
        #endregion
        #region Index
        public async Task<IActionResult> Index()
        {
            var manage = await _manageSite.GetManagementsHome();

            var list = await _produtos.GetAll();
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Produtos = list.Value,
                ManagementHome = manage

            };
            return View(homeViewModel);

        }
        #region Sobre
        public async Task<IActionResult> Sobre()
        {
            var manage = await _manageSite.GetManagementSobre();
            return View(manage);
        }
        #endregion
    }
    #endregion
}