using System.Collections.Generic; // Adicione esta linha

namespace PeckProductsApp.Models
{
    public class ProdutoPack
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeUnidades { get; set; }
        public decimal Valor { get; set; }
        public ICollection<Sabor> Sabores { get; set; }
    }

    public class Sabor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ProdutoPackId { get; set; }
        public ProdutoPack ProdutoPack { get; set; }
    }
}
