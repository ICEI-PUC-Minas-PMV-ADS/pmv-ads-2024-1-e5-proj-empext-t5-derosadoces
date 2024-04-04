using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.ViewModel
{
    public class EsqueciSenhaViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}