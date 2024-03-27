using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace DeRosaWebApp.Models
{
    public class Pedido
    {
        [Key]
        [DisplayName("Identificador do pedido")]
        public int Cod_Pedido { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório!")]

        public string Nome { get; set; }
        [DisplayName("Data do pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public DateTime DataExpiracao { get; set; }
        [Required(ErrorMessage = "O pedido deve conter o cep!")]
        // [MaxLength(8, ErrorMessage = "O Cep deve conter 8 números!")]
        // [MinLength(8, ErrorMessage = "O Cep deve conter 8 números!")]
        public int Cep { get; set; }
        [Required(ErrorMessage = "O pedido deve conter o endereço!")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "O pedido deve conter o número da residência!")]
        [Range(1, 999, ErrorMessage = "O número deve ser entre 1 e 999")]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O pedido deve conter o bairro!")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O pedido deve conter a cidade!")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O pedido deve conter o estado!")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O pedido deve conter o telefone!")]
        public string Telefone { get; set; }
        public List<ItemCarrinho> ProdutosPedido { get; set; }
        [Required]
        [DisplayName("Total de itens do pedido")]
        public int TotalItensPedido { get; set; }
        [DisplayName("Total do pedido")]
        public double TotalPedido { get; set; }
        public bool Concluido { get; set; }
        public bool Entregue { get; set; }
        public bool Pago { get; set; }
        public string Conjunto_IdProdutos { get; set; }
        public string Id_User { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<PedidoDetalhe> _PedidoDetalhes { get; set; }

    }
}
