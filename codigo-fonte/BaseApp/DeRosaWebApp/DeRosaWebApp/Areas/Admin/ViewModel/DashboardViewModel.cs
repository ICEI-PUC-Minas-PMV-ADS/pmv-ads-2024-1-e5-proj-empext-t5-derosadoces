using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace DeRosaWebApp.Areas.Admin.ViewModel
{
    public class DashboardViewModel
    {
        public int NumeroDePedidos { get; set; }
        public double TotalVendido { get; set; }
        public int NumeroDePedidosEntregues { get; set; }
        public int NumeroDePedidosPendentes { get; set; }
        public Dictionary<(int, string), int> VendasPorProduto { get; set; }
        public int MesSelecionado { get; set; }
        public int AnoSelecionado { get; set; }
    }
}
