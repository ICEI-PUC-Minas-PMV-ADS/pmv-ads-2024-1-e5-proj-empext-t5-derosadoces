using DeRosaWebApp.Models;
using ReflectionIT.Mvc.Paging;

namespace DeRosaWebApp.ViewModel
{
    public class TodosProdutosViewModel
    {
        public PagingList<Produto> _Produtos { get; set; }
        public IEnumerable<Categoria> _Categorias { get; set; }
        public int TotalRecordCount { get; set; }   
    }
}
