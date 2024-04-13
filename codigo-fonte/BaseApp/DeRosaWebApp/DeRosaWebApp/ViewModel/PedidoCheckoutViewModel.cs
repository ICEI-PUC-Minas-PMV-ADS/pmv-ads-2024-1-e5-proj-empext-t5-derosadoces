using DeRosaWebApp.Models;

namespace DeRosaWebApp.ViewModel
{
    public class PedidoCheckoutViewModel
    {
        public Pedido _Pedido { get; set; }
        public List<Produto> PedidosComemorativos { get; set; } = new List<Produto>();
    }
}
