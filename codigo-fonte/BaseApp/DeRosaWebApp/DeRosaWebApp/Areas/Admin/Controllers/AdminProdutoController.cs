using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System.Data;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminProdutoController : Controller
    {
        private readonly IProductService _productService;
        public AdminProdutoController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        [Route("{controller}/Index")]
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var list = _productService.PaginationProduct();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                list = list.Where(p => p.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(list, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        [HttpGet]
        [Route("{controller}/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _productService.GetById(id);
            return View(produto);
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
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(produto);
                return RedirectToAction("Index");

            }
            return View(produto);
        }

        [HttpGet]
        [Route("{controller}/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {


            var produto = await _productService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }


            return View(produto.Value);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Edit/{id}")]
        public async Task<ActionResult<Produto>> Edit(int id, Produto produto)
        {
            if (produto is not null)
            {

                if (ModelState.IsValid)
                {
                    var result = await _productService.Update(produto, id);
                    return RedirectToAction("Index", "AdminProduto", result);
                }
                else
                {
                    return View();
                }

            }
            ModelState.AddModelError("Erro", "Pedido é nulo");
            return View();

        }

        [HttpGet]
        [Route("{controller}/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var produto = await _productService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _productService.GetById(id) == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            var produto = await _productService.GetById(id);
            if (produto != null)
            {
                await _productService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return  _productService.Any(id);
        }
    }
}
