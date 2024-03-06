using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string CategoriaNome { get; set; }
    }
}
