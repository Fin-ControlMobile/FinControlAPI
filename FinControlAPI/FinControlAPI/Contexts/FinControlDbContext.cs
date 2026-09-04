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

    public virtual DbSet<Dispositivo> Dispositivo { get; set; }

    public virtual DbSet<FormaPagamento> FormaPagamento { get; set; }

    public virtual DbSet<TokenRedefinicaoSenha> TokenRedefinicaoSenha { get; set; }

    public virtual DbSet<Transacao> Transacao { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dispositivo>(entity =>
        {
            entity.Property(e => e.dispositivoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.nomeDispositivo)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasMany(d => d.usuario).WithMany(p => p.dispositivo)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioDispositivo",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DispositivoUsuario_Usuario"),
                    l => l.HasOne<Dispositivo>().WithMany()
                        .HasForeignKey("dispositivoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DispositivoUsuario_Dispositivo"),
                    j =>
                    {
                        j.HasKey("dispositivoId", "usuarioId");
                    });
        });

        modelBuilder.Entity<FormaPagamento>(entity =>
        {
            entity.HasKey(e => e.formaId);

            entity.Property(e => e.formaId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TokenRedefinicaoSenha>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__TokenRed__3213E83FA9CECF89");

            entity.Property(e => e.tokenHash)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.usuario).WithMany(p => p.TokenRedefinicaoSenha)
                .HasForeignKey(d => d.usuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TokenRedefinicaoSenha_Usuario");
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
            entity.HasIndex(e => e.email, "UQ__Usuario__AB6E61649D386303").IsUnique();

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
