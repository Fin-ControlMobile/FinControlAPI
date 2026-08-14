using FinControlAPI.Domains;

namespace FinControlAPI.Interfaces
{
    public interface ITransacaoRepository
    {
        List<Transacao> Listar(Guid usuarioId);
        List<Transacao> ListarHoje(Guid usuarioId);
        List<Transacao> ListarOntem(Guid usuarioId);
        List<Transacao> ListarRecentes(Guid usuarioId);
        List<Transacao> ListarPorTipoTransacao(Guid usuarioId, Guid formaPagamentoId);
    }
}