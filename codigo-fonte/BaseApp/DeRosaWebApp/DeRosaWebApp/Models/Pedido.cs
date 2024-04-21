using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace DeRosaWebApp.Models
{
    public class Pedido
    {
        [Key]
        [DisplayName("Identificador do pedido")]
        public int Cod_Pedido { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(200,ErrorMessage ="No máximo 200 caracteres!")]
        public string Nome { get; set; }

        [DisplayName("Data do pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        public DateTime DataExpiracao { get; set; }

        [DisplayName("Data para entrega")]
        public DateTime DataParaEntregar { get; set; }

        [Required(ErrorMessage = "O pedido deve conter o cep!")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido. Use o formato: 00000-000")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O pedido deve conter o número da residência!")]
        [Range(1, 999, ErrorMessage = "O número deve ser entre 1 e 999")]
        public int Numero { get; set; }

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O complemento deve ter no mínimo 3 caracteres.")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O pedido deve conter o bairro!")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O bairro deve ter no mínimo 3 caracteres.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O pedido deve conter a cidade!")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "A cidade deve ter no mínimo 3 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O pedido deve conter o estado!")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve ter exatamente 2 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O pedido deve conter o telefone!")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        [StringLength(14,ErrorMessage ="No máximo 11 caracteres, com o formato 000 0 00000000")]
        public string Telefone { get; set; }

        [StringLength(100, ErrorMessage = "O logradouro deve ter no máximo 100 caracteres.")]
        [MinLength(10, ErrorMessage = "O logradouro deve ter no mínimo 10 caracteres.")]
        public string Logradouro { get; set; }

        [DisplayName("Total de itens do pedido")]
        [Required(ErrorMessage = "A quantidade total de itens é obrigatória!")]
        public int TotalItensPedido { get; set; }

        [DisplayName("Total do pedido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O total do pedido deve ser maior que zero.")]
        public double TotalPedido { get; set; }

        public bool Concluido { get; set; }
        public bool Entregue { get; set; }
        public bool Pago { get; set; }
        [StringLength(250)]
        public string Conjunto_IdProdutos { get; set; }
        public string Id_User { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<PedidoDetalhe> _PedidoDetalhes { get; set; }

        public List<ItemCarrinho> ProdutosPedido { get; set; } = new List<ItemCarrinho>();
    }
}
