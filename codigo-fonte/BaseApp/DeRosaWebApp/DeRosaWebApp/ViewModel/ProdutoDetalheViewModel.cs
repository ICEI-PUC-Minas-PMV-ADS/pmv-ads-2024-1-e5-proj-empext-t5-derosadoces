using DeRosaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.ViewModel
{
    public class ProdutoDetalheViewModel
    {
        public ActionResult<Produto> Produto { get; set; }   
        public Categoria Categoria { get; set; }
    }
}
