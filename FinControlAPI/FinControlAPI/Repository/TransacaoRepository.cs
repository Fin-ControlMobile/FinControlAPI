using FinControlAPI.Contexts;
using FinControlAPI.Domains;
using FinControlAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinControlAPI.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        public FinControlDbContext _context;

        public TransacaoRepository(FinControlDbContext context)
        {
            _context = context;
        }

        public List<Transacao> ListarPorUsuario(Guid id)
        {
            return _context.Transacao
                .Where(t => t.usuarioRemetenteId == id || t.usuarioDestinatarioId == id)
                .Include(t => t.formaPagamento)
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .ToList();
        }
        public Transacao ObterPorId(Guid id)
        {
            return _context.Transacao
                .Include(t => t.formaPagamento)
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .Where(t => t.transacaoId == id)
                .FirstOrDefault();
        }

        public bool UsuarioExiste(Guid id)
        {
            return _context.Usuario.Any(u => u.usuarioId == id);
        }

        public bool TransacaoExiste(Guid id)
        {
            return _context.Transacao.Any(t => t.transacaoId == id);
        }

        public bool TipoPagamentoExiste(Guid id)
        {
            return _context.FormaPagamento.Any(f => f.formaId == id);
        }
        public List<Transacao> ObterPorTipoPagamento(Guid usuarioId,Guid id)
        {
            return _context.Transacao
                .Where(t => t.formaPagamentoId == id && (t.usuarioRemetenteId == usuarioId || t.usuarioDestinatarioId == usuarioId))
                .Include(t => t.formaPagamento)
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .ToList();
        }

        public List<Transacao> ObterPorExtracao(Guid usuarioId ,bool recebimento)
        {
            if (recebimento)
            {
                return _context.Transacao
                    .Where(t => t.usuarioDestinatarioId == usuarioId)
                    .Include(t => t.formaPagamento)
                    .Include(t => t.usuarioRemetente)
                    .Include(t => t.usuarioDestinatario)
                    .ToList();
            }
            else if(!recebimento)
            {
                return _context.Transacao
                    .Where(t => t.usuarioRemetenteId == usuarioId)
                    .Include(t => t.formaPagamento)
                    .Include(t => t.usuarioRemetente)
                    .Include(t => t.usuarioDestinatario)
                    .ToList();
            }
            else
            {
                return null;
            }
        }
        public void FazerTransferencia(Transacao transacao)
        {
            _context.Transacao.Add(transacao);
            _context.SaveChanges();
        }
    }
}
