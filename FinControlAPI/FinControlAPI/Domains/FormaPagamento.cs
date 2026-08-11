using System;
using System.Collections.Generic;

namespace FinControlAPI.Domains;

public partial class FormaPagamento
{
    public Guid formaId { get; set; }

    public string tipo { get; set; } = null!;

    public virtual ICollection<Transacao> Transacao { get; set; } = new List<Transacao>();
}
