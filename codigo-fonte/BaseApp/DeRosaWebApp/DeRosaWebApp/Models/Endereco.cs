using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [UIHint("_CepTemplate")]
        [Required(ErrorMessage = "Obrigatório informar o CEP!")]
        //[RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido. Use o formato: 00000-000")]
        [StringLength(9, ErrorMessage = "O cep deve ter no máximo 9 caracteres.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o logradouro!")]
        [StringLength(100, ErrorMessage = "O logradouro deve ter no máximo 100 caracteres.")]
        [MinLength(10, ErrorMessage = "O logradouro deve conter no mínimo 10 caracteres.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o número!")]
        [Range(1, 9999, ErrorMessage = "O número deve ser entre 1 e 9999.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O complemento é obrigatório!")]
        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O complemento deve ter no mínimo 3 caracteres.")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o bairro!")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        [MinLength(4, ErrorMessage = "O bairro deve ter no mínimo 4 caracteres.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a cidade!")]
        [StringLength(80, ErrorMessage = "A cidade deve ter no máximo 80 caracteres.")]
        [MinLength(4, ErrorMessage = "A cidade deve ter no mínimo 4 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a UF!")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve ter exatamente 2 caracteres.")]
        public string UF { get; set; }

        public string Id_User { get; set; }

    }
}
