using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models.Management
{
    public class ManagementHome
    {
        public int Id { get; set; }
        [StringLength(60,ErrorMessage ="No máximo 60 caracteres!")]
        public string TituloSemana { get; set; }
    }
}
