using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class ItemCarrinho
    {
        [Key]
        public string Cod_ItemCarrinho { get; set; }
        public Produto Produto { get; set; }
        [Required]
        [MaxLength(100)]
        public int QntProduto { get; set; }
        [ForeignKey(nameof(Carrinho))]
        public string Cod_Carrinho { get; set; }
    }
}
