using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.ViewModel

{
    public class ClienteViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O nome só permite 100 caracteres")]
        [DisplayName("Nome completo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = " O usuário é obrigatório!")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "O número de telefone é obrigatório!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O email é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de nascimento")]
        public DateTime DateNasc { get; set; }


    }

}
