using System;
using System.Collections.Generic;

namespace FinControlAPI.Domains;

public partial class Usuario
{
    public Guid usuarioId { get; set; }

    public string nome { get; set; } = null!;

    public string email { get; set; } = null!;

    public byte[] senha { get; set; } = null!;

    public decimal saldo { get; set; }

    public bool primeiroAcesso { get; set; }

    public virtual ICollection<TokenRedefinicaoSenha> TokenRedefinicaoSenha { get; set; } = new List<TokenRedefinicaoSenha>();

    public virtual ICollection<Transacao> TransacaousuarioDestinatario { get; set; } = new List<Transacao>();

    public virtual ICollection<Transacao> TransacaousuarioRemetente { get; set; } = new List<Transacao>();

    public virtual ICollection<Dispositivo> dispositivo { get; set; } = new List<Dispositivo>();
}
