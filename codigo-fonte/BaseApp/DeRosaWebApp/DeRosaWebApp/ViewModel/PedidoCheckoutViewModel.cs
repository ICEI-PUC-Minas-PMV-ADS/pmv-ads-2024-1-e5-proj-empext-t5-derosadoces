using DeRosaWebApp.Models;

namespace DeRosaWebApp.ViewModel
{
    public class PedidoCheckoutViewModel
    {
        public List<Produto> ListProdutosComemorativos { get; set; } = new List<Produto>();
        public DateTime DataEntrega { get; set; } = new DateTime();
    }
}
