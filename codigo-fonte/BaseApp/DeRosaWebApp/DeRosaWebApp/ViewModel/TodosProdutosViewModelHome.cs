using DeRosaWebApp.Models;

namespace DeRosaWebApp.ViewModel
{
    public class TodosProdutosViewModelHome
    {
        public IEnumerable<Produto> Produtos { get; set; }      
        public IEnumerable<Categoria> Categorias { get; set; }  
    }
}
