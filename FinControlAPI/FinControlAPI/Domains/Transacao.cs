using System;
using System.Collections.Generic;

namespace FinControlAPI.Domains;

public partial class Transacao
{
    public Guid transacaoId { get; set; }

    public decimal valorTransferencia { get; set; }

    public DateTime dataTransacao { get; set; }

    public string? descricao { get; set; }

    public Guid usuarioRemetenteId { get; set; }

    public Guid usuarioDestinatarioId { get; set; }

    public Guid formaPagamentoId { get; set; }

    public virtual FormaPagamento formaPagamento { get; set; } = null!;

    public virtual Usuario usuarioDestinatario { get; set; } = null!;

    public virtual Usuario usuarioRemetente { get; set; } = null!;
}
