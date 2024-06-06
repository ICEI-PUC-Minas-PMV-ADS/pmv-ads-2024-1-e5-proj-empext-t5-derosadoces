using Microsoft.EntityFrameworkCore;
using PeckProductsApp.Models;

namespace PeckProductsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProdutoPack> ProdutoPacks { get; set; }
        public DbSet<Sabor> Sabores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoPack>()
                .Property(p => p.Valor)
                .HasColumnType("decimal(10,2)"); // Configurar precisão e escala

            base.OnModelCreating(modelBuilder);
        }
    }
}
