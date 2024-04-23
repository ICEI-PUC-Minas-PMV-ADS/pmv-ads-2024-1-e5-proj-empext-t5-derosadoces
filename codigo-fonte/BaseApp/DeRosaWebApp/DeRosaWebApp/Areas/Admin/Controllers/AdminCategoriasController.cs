using DeRosaWebApp.Areas.Admin.ViewModel;
using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Data;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public AdminCategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [Route("{controller}/Index")]

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "CategoriaNome")
        {
            var ctgPagination = _categoriaService.PaginationCategoria();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                ctgPagination = ctgPagination.Where(p => p.CategoriaNome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(ctgPagination, 5, pageindex, sort, "CategoriaNome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        [HttpGet]
        [Route("{controller}/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            return View(categoria);
        }

        [HttpGet]
        [Route("{controller}/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Create")]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaService.Add(categoria);
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        [Route("{controller}/Edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            return View(categoria);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaService.Update(id, categoria);
                ViewBag.Sucesso = "Alterações concluídas!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Erro", "Verifique todos os campos e tente novamente!");
            return View();
        }



        [Route("{controller}/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Delete/{id}")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaService.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> CategoriaExists(int id)
        {
            return await _categoriaService.Any(id);
        }
    }
}
