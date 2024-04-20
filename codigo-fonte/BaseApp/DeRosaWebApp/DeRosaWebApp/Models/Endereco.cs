using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Endereco
    {

        public int Id { get; set; }

        [UIHint("_CepTemplate")]
        [Required(ErrorMessage = "Obrigatório informar o CEP!")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido. Use o formato: 00000-000")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o logradouro!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Número!")]
        [Range(1, int.MaxValue, ErrorMessage = "O número deve ser maior que zero.")]
        public int Numero { get; set; }

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres.")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o bairro!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a cidade!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a UF!")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve ter 2 caracteres.")]
        public string UF { get; set; }

        public string Id_User { get; set; }

        [StringLength(100, ErrorMessage = "A rua deve ter no máximo 100 caracteres.")]
        public string Rua { get; set; }
    }
}
