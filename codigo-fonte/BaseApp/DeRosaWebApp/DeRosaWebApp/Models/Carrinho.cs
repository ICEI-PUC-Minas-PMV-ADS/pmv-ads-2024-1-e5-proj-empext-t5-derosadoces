using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Carrinho
    {
        [Key]
        public string Cod_Carrinho { get; set; }
        public List<ItemCarrinho> ListItemCarrinho { get; set; }
        public int QuantidadeTotal { get; set; }
    }
}
