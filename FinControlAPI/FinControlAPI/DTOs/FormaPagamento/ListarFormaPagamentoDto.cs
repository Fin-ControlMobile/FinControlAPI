namespace FinControlAPI.DTOs.FormaPagamento
{
    public class ListarFormaPagamentoDto
    {
        public Guid formaId { get; set; }
        public string tipo { get; set; } = null!;
    }
}
