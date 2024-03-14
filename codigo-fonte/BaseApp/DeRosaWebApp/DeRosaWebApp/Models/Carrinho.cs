using DeRosaWebApp.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeRosaWebApp.Models
{
    public class Carrinho
    {
        private readonly AppDbContext _context;
        public Carrinho(AppDbContext context)
        {
            _context = context;
        }

        [Key]
        public string Cod_Carrinho { get; set; }
        public List<ItemCarrinho> ListItemCarrinho { get; set; }
        public int QuantidadeTotal { get; set; }


        public static Carrinho GetCarrinho(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();
            string codCarrinho = session.GetString("Cod_Carrinho") ?? Guid.NewGuid().ToString();
            session.SetString("Cod_Carrinho", codCarrinho);
            return new Carrinho(context)
            {
                Cod_Carrinho = codCarrinho,
            };

        }
        public void AdicionarNoCarrinho(Produto produto)
        {
            var itemCarrinho = _context.ItemCarrinhos.SingleOrDefault(p => p.Produto.Cod_Produto == produto.Cod_Produto
            && p.Cod_Carrinho == Cod_Carrinho);
            if (itemCarrinho is not null)
            {
                itemCarrinho.QntProduto++;
                _context.SaveChanges();
            }
            else
            {

                ItemCarrinho itemCompras = new ItemCarrinho()
                {
                    Cod_ItemCarrinho = Guid.NewGuid().ToString(),
                    Produto = produto,
                    QntProduto = 1,
                    Cod_Carrinho = Cod_Carrinho

                };
                _context.ItemCarrinhos.Add(itemCompras);
                _context.SaveChanges();
            }

        }
        public int GetTotalItens()
        {
            int total = 0;
            foreach (ItemCarrinho i in ListItemCarrinho)
            {
                total += i.QntProduto;
            }
            return total;
        }
        public List<ItemCarrinho> GetItemCarrinhos()
        {
            return ListItemCarrinho ??= _context.ItemCarrinhos
                .Where(p => p.Cod_Carrinho == Cod_Carrinho)
                .Include(p => p.Produto)
                .ToList();

        }
        public void RemoverDoCarrinho(Produto produto)
        {
            var itemCarrinho = _context.ItemCarrinhos.SingleOrDefault(p => p.Produto.Cod_Produto == produto.Cod_Produto && p.Cod_Carrinho == Cod_Carrinho);
            if (itemCarrinho is not null && itemCarrinho.QntProduto > 1)
            {
                itemCarrinho.QntProduto--;
            }
            else if (itemCarrinho.QntProduto == 1)
            {
                _context.ItemCarrinhos.Remove(itemCarrinho);
            }
            _context.SaveChanges();

        }
        public void LimparCarrinho()
        {
            var carrinhoItens = _context.ItemCarrinhos.Where(i => i.Cod_Carrinho == Cod_Carrinho);
            _context.ItemCarrinhos.RemoveRange(carrinhoItens);
            _context.SaveChanges();


        }
        public double GetTotalCarrinho()
        {
            var total = _context.ItemCarrinhos.Where(p => p.Cod_Carrinho == Cod_Carrinho)
                .Select(p => p.Produto.Preco * p.QntProduto)
                .Sum();
            return total;

        }

    }
}
