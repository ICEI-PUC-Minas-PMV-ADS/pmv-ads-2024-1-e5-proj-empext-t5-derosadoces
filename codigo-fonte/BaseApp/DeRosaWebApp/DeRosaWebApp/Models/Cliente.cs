using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Cliente
    {

        [Key]
        public int Cod_Cliente { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O nome só permite 100 caracteres")]
        [DisplayName("Nome completo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = " O usuário é obrigatório!")]
        public string NomeUsuario { get; set; } 
        [Required(ErrorMessage = "O número de telefone é obrigatório!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório!")]
        [MaxLength(14)]
        [MinLength(14)]
        public string CPF { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "No minimo 8 caracteres.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O email é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de nascimento")]
        public DateTime DateNasc { get; set; }
        public List<Pedido> _Pedidos { get; set; }
        
        public virtual Carrinho _Carrinho { get; set; }
       
    }
}
