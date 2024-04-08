using DeRosaWebApp.Areas.Admin.ViewModel;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly IPedidoService _pedidoService;
        private readonly IProductService _productService;

        public AdminDashboardController(IPedidoService pedidoService, IProductService productService)
        {
            _pedidoService = pedidoService;
            _productService = productService;
        }

        // Método para exibir o painel administrativo
        [HttpGet]
        [Route("{controller}/{mes:int?}")]
        public async Task<IActionResult> Index(int mes = 0, int ano = 0)
        {
            // Verifica se o mês e o ano foram fornecidos, caso contrário, utiliza o mês e o ano atuais
            if (mes == 0)
            {
                mes = DateTime.Now.Month;
            }
            if (ano == 0)
            {
                ano = DateTime.Now.Year;
            }

            // Obtém todos os pedidos do banco de dados
            var pedidosAll = await _pedidoService.GetAll();
            // Filtra os pedidos pelo mês e ano selecionados
            var pedidos = pedidosAll.Value.Where(p => p.DataPedido.Month == mes && p.DataPedido.Year == ano);

            // Calcula estatísticas de vendas
            var dashboardViewModel = CalcularEstatisticasDeVendas(pedidos, mes, ano);

            // Calcula as vendas por produto
            dashboardViewModel.VendasPorProduto = await CalcularVendasPorProduto(pedidos);

            return View(dashboardViewModel);
        }

        // Método para calcular as estatísticas de vendas
        private DashboardViewModel CalcularEstatisticasDeVendas(IEnumerable<Pedido> pedidos, int mes, int ano)
        {
            int NumeroDePedidos = pedidos.Count();
            double totalVendido = pedidos.Sum(p => p.TotalPedido);
            int NumeroDePedidosConcluidos = pedidos.Count(p => p.Concluido == true);
            int NumeroDePedidosPendentes = pedidos.Count(p => p.Concluido == false);

            return new DashboardViewModel
            {
                NumeroDePedidos = NumeroDePedidos,
                TotalVendido = totalVendido,
                NumeroDePedidosEntregues = NumeroDePedidosConcluidos,
                NumeroDePedidosPendentes = NumeroDePedidosPendentes,
                MesSelecionado = mes,
                AnoSelecionado = ano
            };
        }

        // Método para calcular as vendas por produto
        private async Task<Dictionary<(int, string), int>> CalcularVendasPorProduto(IEnumerable<Pedido> pedidos)
        {
            var vendasPorProduto = new Dictionary<(int, string), int>();

            foreach (var pedido in pedidos)
            {
                var produtosPedido = await _pedidoService.DetalhePedidoList(pedido.Cod_Pedido);
                foreach (var produto in produtosPedido)
                {
                    var produtoNome = (await _productService.GetById(produto.Cod_Produto)).Value.Nome;
                    if (vendasPorProduto.ContainsKey((produto.Cod_Produto, produtoNome)))
                    {
                        vendasPorProduto[(produto.Cod_Produto, produtoNome)] += produto.Quantidade;
                    }
                    else
                    {
                        vendasPorProduto.Add((produto.Cod_Produto, produtoNome), produto.Quantidade);
                    }
                }
            }

            return vendasPorProduto;
        }
    }
}
