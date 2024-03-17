using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeRosaWebApp.Models
{
    public class PedidoDetalhe
    {
        [Key]
        public int Cod_PedidoDetalhe { get; set; }
        public int Cod_Pedido { get; set; }
        public int Cod_Produto { get; set; }
        public int Quantidade { get; set; }
        public string Id_User { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public string Conjunto_Pedidos { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Pedido Pedido { get; set; }

    }
}
