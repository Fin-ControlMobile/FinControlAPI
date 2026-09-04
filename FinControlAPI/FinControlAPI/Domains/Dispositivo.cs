using System;
using System.Collections.Generic;

namespace FinControlAPI.Domains;

public partial class Dispositivo
{
    public Guid dispositivoId { get; set; }

    public string nomeDispositivo { get; set; } = null!;

    public virtual ICollection<Usuario> usuario { get; set; } = new List<Usuario>();
}
