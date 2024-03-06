using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models.Entities
{
    public class Admin
        
    {
        [Key]
        [Required]
        public string Cod_Admin { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public string Nome_Fantasia { get; set; }
    }
}
