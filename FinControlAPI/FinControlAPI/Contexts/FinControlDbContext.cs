using System;
using System.Collections.Generic;
using FinControlAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace FinControlAPI.Contexts;

public partial class FinControlDbContext : DbContext
{
    public FinControlDbContext()
    {
    }

    public FinControlDbContext(DbContextOptions<FinControlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FormaPagamento> FormaPagamento { get; set; }

    public virtual DbSet<Transacao> Transacao { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FormaPagamento>(entity =>
        {
            entity.HasKey(e => e.formaId);

            entity.Property(e => e.formaId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.Property(e => e.transacaoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.dataTransacao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.descricao).IsUnicode(false);
            entity.Property(e => e.valorTransferencia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.formaPagamento).WithMany(p => p.Transacao)
                .HasForeignKey(d => d.formaPagamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_FormaPagamento");

            entity.HasOne(d => d.usuarioDestinatario).WithMany(p => p.TransacaousuarioDestinatario)
                .HasForeignKey(d => d.usuarioDestinatarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_Destinatario");

            entity.HasOne(d => d.usuarioRemetente).WithMany(p => p.TransacaousuarioRemetente)
                .HasForeignKey(d => d.usuarioRemetenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_Remetente");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.email, "UQ__Usuario__AB6E61645B896464").IsUnique();

            entity.Property(e => e.usuarioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.primeiroAcesso).HasDefaultValue(true);
            entity.Property(e => e.saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.senha).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
