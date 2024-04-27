using DeRosaWebApp.Models;
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
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "No minimo 8 caracteres!")]
        [StringLength(200, ErrorMessage = "No máximo 200 caracteres!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de nascimento")]
        public DateTime DateNasc { get; set; }
        public List<Endereco> _Enderecos { get; set; } = new List<Endereco>();


    }

}