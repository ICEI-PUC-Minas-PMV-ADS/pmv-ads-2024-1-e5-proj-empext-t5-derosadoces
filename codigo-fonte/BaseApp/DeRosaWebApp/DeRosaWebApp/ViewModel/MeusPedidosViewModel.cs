using DeRosaWebApp.Models;

namespace DeRosaWebApp.ViewModel
{
    public class MeusPedidosViewModel
    {
        public IEnumerable<Pedido>  Pedidos { get; set; }   
        public IEnumerable<Produto> ProdutosPedido { get; set; }
    }
}
