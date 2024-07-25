using System.Diagnostics.CodeAnalysis;
using GestaoProduto.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoProduto.Infrastructure.Context
{
    [ExcludeFromCodeCoverage]
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(e =>
            {
                e.HasKey(x => x.Codigo);
                e.Property(x => x.Codigo)
                    .ValueGeneratedOnAdd();
                e.Property(de => de.Descricao)
                    .IsRequired()
                    .HasMaxLength(200);
                e.Property(de => de.Situacao);
                e.Property(de => de.DataFabricacao);
                e.Property(de => de.DataValidade);
                e.HasOne(de => de.Fornecedor)
                    .WithOne()
                    .HasForeignKey<Fornecedor>(s => s.CodigoCliente);
            });

            modelBuilder.Entity<Fornecedor>(e =>
            {
                e.HasKey(de => de.Codigo);
                e.Property(x => x.Codigo)
                    .ValueGeneratedOnAdd();
                e.Property(x => x.Cnpj)
                    .HasMaxLength(14);
            });
        }
    }
}