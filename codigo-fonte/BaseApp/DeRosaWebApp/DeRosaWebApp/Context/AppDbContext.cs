using DeRosaWebApp.Models;
using DeRosaWebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemCarrinho> ItemCarrinhos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
        public DbSet<Admin> Administradores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Data Source=NENDS;Database=DeRosaDb;Integrated Security=True; TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }                   // exemplo: connectionString: @"Data Source=DESKTOP-QG1NG7E;Database=DeRosaDb;Integrated Security=True; TrustServerCertificate=True"
    }
}
