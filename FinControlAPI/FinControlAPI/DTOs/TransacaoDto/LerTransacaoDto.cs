namespace FinControlAPI.DTOs.TransacaoDto
{
    public class LerTransacaoDto
    {
        public Guid transacaoId { get; set; }

        public decimal valorTransferencia { get; set; }

        public DateTime dataTransacao { get; set; }

        public string? descricao { get; set; }

        public string remetente { get; set; } = null!;

        public string destinatario { get; set; } = null!;

        public string formaPagamento { get; set; } = null!;
    }
}