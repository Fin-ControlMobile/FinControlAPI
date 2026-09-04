using System;
using System.Collections.Generic;

namespace FinControlAPI.Domains;

public partial class TokenRedefinicaoSenha
{
    public int id { get; set; }

    public Guid usuarioId { get; set; }

    public string tokenHash { get; set; } = null!;

    public DateTime expiraEm { get; set; }

    public bool utilizado { get; set; }

    public virtual Usuario usuario { get; set; } = null!;
}
