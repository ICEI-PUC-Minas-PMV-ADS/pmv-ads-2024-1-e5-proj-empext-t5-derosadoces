using DeRosaWebApp.Models;

namespace DeRosaWebApp.ViewModel
{
    public class TodosProdutosViewModel
    {
        public IEnumerable<Produto> _Produtos { get; set; }
        public IEnumerable<Categoria> _Categorias { get; set; }
    }
}
