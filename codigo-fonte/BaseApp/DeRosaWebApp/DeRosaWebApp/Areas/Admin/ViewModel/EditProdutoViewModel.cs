using DeRosaWebApp.Models;

namespace DeRosaWebApp.Areas.Admin.ViewModel
{
    public class EditProdutoViewModel
    {
        public Produto Produto { get; set; }
        public IEnumerable<Categoria> ListCategorias { get; set; }
    }
}
