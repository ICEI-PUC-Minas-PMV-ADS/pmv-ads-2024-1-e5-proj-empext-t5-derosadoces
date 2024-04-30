using DeRosaWebApp.Models;

namespace DeRosaWebApp.Areas.Admin.ViewModel
{
    public class CreateProdutoViewModel
    {
        public Produto Produto { get; set; }
        public IEnumerable<Categoria> ListCategorias { get; set; }
        public List<string> AvailableImages { get; set; } = new List<string>();
    }
}
