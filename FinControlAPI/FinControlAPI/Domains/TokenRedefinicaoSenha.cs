using System;
using System.Collections.Generic;

namespace FinControlAPI.Domains;

public partial class TokenRedefinicaoSenha
{
    public int Id { get; set; }

    public Guid UsuarioId { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime ExpiraEm { get; set; }

    public bool Utilizado { get; set; }
}
