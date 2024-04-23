using System.ComponentModel.DataAnnotations;
using System.ComponentModel; 

namespace DeRosaWebApp.Models
{
    public class Categoria
    {
        [Key]
        [DisplayName("Identificador da Categoria")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter entre 3 e 100 caracteres.")]
        [DisplayName("Nome da Categoria")]
        public string CategoriaNome { get; set; }
    }
}
