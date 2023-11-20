using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Combustivel> Combustiveis { get; set; }
        public virtual DbSet<Veiculo> Veiculos { get; set; }
        public virtual DbSet<Motorista> Motoristas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Abastecimento> Abastecimentos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Token); // Chave primária
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Nome).HasMaxLength(50).IsUnicode(false).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false).IsRequired();
                entity.Property(e => e.Senha).HasMaxLength(50).IsUnicode(false).IsRequired();
            });
            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Placa).HasMaxLength(7).IsUnicode(false).IsRequired();
                entity.Property(e => e.NomeVeiculo).HasMaxLength(50).IsUnicode(false).IsRequired();
                entity.Property(e => e.Fabricante).HasMaxLength(255).IsUnicode(false).IsRequired();
                entity.Property(e => e.Observacoes).IsUnicode(false);
            });

            modelBuilder.Entity<Veiculo>()
                .HasOne(a => a.Combustivel)
                .WithMany()
                .HasForeignKey(a => a.CombustivelId);

            modelBuilder.Entity<Combustivel>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Descricao).HasMaxLength(20).IsUnicode(false);
            });
            modelBuilder.Entity<Motorista>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Nome).HasMaxLength(255).IsUnicode(false).IsRequired();
                entity.Property(e => e.CPF).HasMaxLength(14).IsUnicode(false).IsRequired();
                entity.Property(e => e.NumeroCNH).HasMaxLength(11).IsUnicode(false).IsRequired();
                entity.Property(e => e.CategoriaCNH).HasMaxLength(1).IsUnicode(false).IsRequired();
                entity.Property(e => e.DataNascimento).HasMaxLength(10).IsUnicode(false).IsRequired();
            });
            modelBuilder.Entity<Abastecimento>()
             .HasOne(a => a.Veiculo)
             .WithMany()
             .HasForeignKey(a => a.VeiculoId);

            modelBuilder.Entity<Abastecimento>()
                .HasOne(a => a.MotoristaResponsavel)
                .WithMany()
                .HasForeignKey(a => a.MotoristaResponsavelId);

            modelBuilder.Entity<Abastecimento>()
                .HasOne(a => a.Combustivel)
                .WithMany()
                .HasForeignKey(a => a.CombustivelId);
        }
    }
}
