using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Endereco
    {
        public int Id { get; set; } 
        [Required(ErrorMessage = "Obrigatório informar o CEP!")]
        [UIHint("_CepTemplate")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o logradouro!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Número!")]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Id_User { get; set; }
    }
}
