using DeRosaWebApp.Areas.Admin.ViewModel;
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
    public class AdminPedidosController : Controller
    {

        private readonly IPedidoService _pedidoService;
        private readonly IProductService _productService;

        public AdminPedidosController(IPedidoService pedidoService, IProductService productService)
        {
            _pedidoService = pedidoService;
            _productService = productService;
        }

        [HttpGet]
        [Route("{controller}")]
        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sort = "Nome")
        {
            var list = _pedidoService.PaginationPedido();
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
            List<int> Idprodutos = new List<int>();
            List<Produto> AllProd = new List<Produto>();


            var pedido = await _pedidoService.GetById(id);
            if (pedido is null)
            {
                return NotFound();
            }


            var pedidoDetalhe = _pedidoService.DetalhePedido(pedido.Value.Cod_Pedido); //pegando o pedido detalhe
            string[] idsPedido = pedidoDetalhe.Conjunto_Pedidos.Split(",");  // pegando todos os ids de produtos que estão em str pedidos, exemplo "1,2,3,4,5" 
            foreach (string idProd in idsPedido)
            {
                Idprodutos.Add(Convert.ToInt32(idProd)); //depois de separado por virgula, transformei todos em numero e adicionei em uma lista

            }
            foreach (int idProd in Idprodutos) // agora, com base em todos os ids de produtos, posso pesquisar cada produto que foi utilizado
            {
                var item = await _productService.GetById(idProd);
                AllProd.Add(item.Value);
            }
            PedidoViewModel pedidoViewModel = new PedidoViewModel()
            {
                _Pedido = pedido.Value,
                _PedidoDetalhe = pedidoDetalhe,
                _ProdutosPedido = AllProd,
            };

            return View(pedidoViewModel);
        }



        [HttpGet]
        [Route("{controller}/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var _pedido = await _pedidoService.GetById(id);
            return View(_pedido.Value);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/Edit/{id}")]
        public async Task<ActionResult<Pedido>> Edit(int id, Pedido pedido)
        {

            if (pedido is not null)
            {

                var result = await _pedidoService.Update(id, pedido);
                if (ModelState.IsValid)
                {

                    return RedirectToAction("Index", "AdminPedidos", result);
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
            var pedido = await _pedidoService.GetById(id);

            return View(pedido.Value);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/Admin/AdminPedidos/DeleteConfirmed/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var pedido = await _pedidoService.GetById(id);
            if (pedido != null)
            {
                var result = await _pedidoService.Delete(id);
                return RedirectToAction("Index", "AdminPedidos");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
