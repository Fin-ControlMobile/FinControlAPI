using FinControlAPI.Domains;

namespace FinControlAPI.Interface
{
    public interface ITransacaoRepository
    {
        public List<Transacao> ListarPorUsuario(Guid id);
        public Transacao ObterPorId(Guid id);
        public bool UsuarioExiste(Guid id);
        public bool TransacaoExiste(Guid id);
        public bool TipoPagamentoExiste(Guid id);
        public List<Transacao> ObterPorTipoPagamento(Guid usuarioId,Guid idTransacao);
        public List<Transacao> ObterPorExtracao(Guid idUsuario, bool recebimento);
        public void FazerTransferencia(Transacao transacao);
    }
}
