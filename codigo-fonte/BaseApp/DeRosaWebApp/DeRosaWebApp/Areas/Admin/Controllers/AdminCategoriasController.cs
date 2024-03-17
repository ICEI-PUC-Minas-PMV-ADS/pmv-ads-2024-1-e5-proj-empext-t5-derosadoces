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
        private readonly AppDbContext _context;
        private readonly ICategoriaService _categoriaService;

        public AdminCategoriasController(AppDbContext context, ICategoriaService categoriaService)
        {
            _context = context;
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

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
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [Route("{controller}/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            var _CtgNomesList = await _context.Categorias.ToListAsync();

            CategoriaViewModel categoriaViewModel = new CategoriaViewModel()
            {
                _Categoria = categoria,
                ListNomes = _CtgNomesList

            };
            return View(categoriaViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Edit")]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {

            if (id != categoria.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryExist = _context.Categorias.FirstOrDefault(p => p.IdCategoria == id);
                    categoryExist.CategoriaNome = categoria.CategoriaNome;
                    _context.Update(categoryExist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.IdCategoria))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }



        [Route("{controller}/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {


            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Delete/{id}")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'AppDbContext.Categorias'  is null.");
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id);
        }
    }
}
