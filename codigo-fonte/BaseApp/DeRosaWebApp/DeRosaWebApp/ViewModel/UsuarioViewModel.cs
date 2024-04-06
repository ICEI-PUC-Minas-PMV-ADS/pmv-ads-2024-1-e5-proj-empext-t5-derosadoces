using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.ViewModel
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O usuário é obrigatório!")]

        public string Usuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]

        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string ReturnUrl { get; set; }
    }
}
