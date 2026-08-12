using FinControlAPI.Domains;

namespace FinControlAPI.DTOs
{
    public class LerTransacaoDto
    {
        public Guid transacaoId { get; set; }

        public decimal valorTransferencia { get; set; }

        public DateTime dataTransacao { get; set; }

        public string? descricao { get; set; }

        public Guid usuarioRemetenteId { get; set; }
        public string usuarioRemetenteNome { get; set; } = string.Empty;

        public Guid usuarioDestinatarioId { get; set; }
        public string usuarioDestinatarioNome { get; set; } = string.Empty;
        public Guid formaPagamentoId { get; set; }
        public string formaPagamentoNome { get; set; }
    }
}
