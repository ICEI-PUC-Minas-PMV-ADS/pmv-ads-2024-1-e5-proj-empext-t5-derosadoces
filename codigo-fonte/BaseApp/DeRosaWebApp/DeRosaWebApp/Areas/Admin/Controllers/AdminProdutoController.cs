using DeRosaWebApp.Areas.Admin.ViewModel;
using DeRosaWebApp.Configuration.ConfigImages;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;
using System.Data;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminProdutoController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoriaService _categoriaService;
        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminProdutoController(IProductService productService, ICategoriaService categoriaService,
            IWebHostEnvironment hostingEnvironment, IOptions<ConfigurationImages> myConfiguration)
        {
            _productService = productService;
            _categoriaService = categoriaService;
            _myConfig = myConfiguration.Value;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        [Route("{controller}/Index")]
        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sort = "Nome")
        {
            var list = _productService.PaginationProduct();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                list = list.Where(p => p.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(list, 5, pageIndex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        [HttpGet]
        [Route("{controller}/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _productService.GetById(id);
            var categoria = await _categoriaService.GetById(id);

            ProdutoCategoriaViewModel produtoCategoriaViewModel = new()
            {
                _Produto = produto.Value,
                _Categoria = categoria,
            };

            return View(produtoCategoriaViewModel);
        }

        [HttpGet]
        [Route("{controller}/Create")]
        public IActionResult Create()
        {
            var categorias = _categoriaService.GetAllCategorias();
            // Carregar imagens do diretório
            var imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
            DirectoryInfo dir = new DirectoryInfo(imagesPath);
            FileInfo[] files = dir.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(f => f.Name.EndsWith(".jpg") || f.Name.EndsWith(".png") || f.Name.EndsWith(".gif")).ToArray();

            CreateProdutoViewModel createProdutoViewModel = new CreateProdutoViewModel()
            {
                ListCategorias = categorias,
                AvailableImages = files.Select(file => file.Name).ToList()
            };

            return View(createProdutoViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Create")]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(produto);
                return RedirectToAction("Index", "AdminProduto");

            }
            var imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
            DirectoryInfo dir = new DirectoryInfo(imagesPath);
            FileInfo[] files = dir.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(f => f.Name.EndsWith(".jpg") || f.Name.EndsWith(".png") || f.Name.EndsWith(".gif")).ToArray();
            var categorias = _categoriaService.GetAllCategorias();
            CreateProdutoViewModel createProdutoViewModel = new CreateProdutoViewModel()
            {
                Produto = produto,
                ListCategorias = categorias,
                AvailableImages = files.Select(file => file.Name).ToList()
            };
            ModelState.AddModelError("Erro", "Produto é nulo");
            return View(createProdutoViewModel);
        }

        [HttpGet]
        [Route("{controller}/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _productService.GetById(id);
            var categorias = _categoriaService.GetAllCategorias();  // Certifique-se de que este método é assíncrono
            if (produto == null)
            {
                return NotFound();
            }

            // Carregar imagens do diretório
            var imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
            DirectoryInfo dir = new DirectoryInfo(imagesPath);
            FileInfo[] files = dir.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.Name.EndsWith(".jpg") || f.Name.EndsWith(".png") || f.Name.EndsWith(".gif")).ToArray();

            EditProdutoViewModel editProdutoViewModel = new EditProdutoViewModel()
            {
                Produto = produto.Value,
                ListCategorias = categorias,
                AvailableImages = files.Select(file => file.Name).ToList()
            };
            HttpContext.Session.SetString("Cod_Produto", id.ToString());
            return View(editProdutoViewModel);
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
            ModelState.AddModelError("Erro", "Produto é nulo");
            return View();

        }

        [HttpGet]
        [Route("{controller}/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var produto = await _productService.GetById(id);
            var categoria = await _categoriaService.GetById(id);

            ProdutoCategoriaViewModel produtoCategoriaViewModel = new()
            {
                _Produto = produto.Value,
                _Categoria = categoria,
            };

            return View(produtoCategoriaViewModel);
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
            return _productService.Any(id);
        }
    }
}
