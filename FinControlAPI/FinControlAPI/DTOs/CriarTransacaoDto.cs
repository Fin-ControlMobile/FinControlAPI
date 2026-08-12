namespace FinControlAPI.DTOs
{
    public class CriarTransacaoDto
    {
        public Guid transacaoId { get; set; }
        public decimal valorTransferencia { get; set; }
        public string? descricao { get; set; }
        public Guid usuarioRemetenteId { get; set; }
        public Guid usuarioDestinatarioId { get; set; }
        public Guid formaPagamentoId { get; set; }
    }
}
