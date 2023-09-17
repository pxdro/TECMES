using Microsoft.EntityFrameworkCore;
using TECMES.Shared.Models;

namespace TECMES.Server.Context
{
    public class TECMESContext : DbContext
    {
        public TECMESContext(DbContextOptions<TECMESContext> options) : base(options)
        { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<OrdemProducao> OrdensProducao { get; set; }
        public DbSet<Producao> Producoes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Status)
                .HasConversion<string>()
                .HasMaxLength(10);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.OrdemProducao)
                .HasConversion<string>()
                .HasMaxLength(10);

            modelBuilder.Entity<Produto>()
                .HasIndex(pr => pr.Nome)
                .IsUnique();

            modelBuilder.Entity<OrdemProducao>()
                .Property(op => op.Liberado)
                .HasConversion<string>()
                .HasMaxLength(10);

            modelBuilder.Entity<Producao>()
                .HasIndex(pe => pe.OrdemProducaoId)
                .IsUnique();

        }
    }
}
