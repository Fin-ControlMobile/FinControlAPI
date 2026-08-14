using FinControlAPI.Contexts;
using FinControlAPI.Domains;
using FinControlAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinControlAPI.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly FinControlDbContext _context;

        public TransacaoRepository(FinControlDbContext context)
        {
            _context = context;
        }

        public List<Transacao> Listar(Guid usuarioId)
        {
            return _context.Transacao
                .AsNoTracking()
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .Include(t => t.formaPagamento)
                .Where(t =>
                    t.usuarioRemetenteId == usuarioId ||
                    t.usuarioDestinatarioId == usuarioId)
                .ToList();
        }

        public List<Transacao> ListarHoje(Guid usuarioId)
        {
            DateTime inicio = DateTime.Today;
            DateTime fim = inicio.AddDays(1);

            return _context.Transacao
                .AsNoTracking()
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .Include(t => t.formaPagamento)
                .Where(t =>
                    (t.usuarioRemetenteId == usuarioId ||
                     t.usuarioDestinatarioId == usuarioId) &&
                    t.dataTransacao >= inicio &&
                    t.dataTransacao < fim)
                .ToList();
        }

        public List<Transacao> ListarOntem(Guid usuarioId)
        {
            DateTime inicio = DateTime.Today.AddDays(-1);
            DateTime fim = DateTime.Today;

            return _context.Transacao
                .AsNoTracking()
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .Include(t => t.formaPagamento)
                .Where(t =>
                    (t.usuarioRemetenteId == usuarioId ||
                     t.usuarioDestinatarioId == usuarioId) &&
                    t.dataTransacao >= inicio &&
                    t.dataTransacao < fim)
                .ToList();
        }

        public List<Transacao> ListarRecentes(Guid usuarioId)
        {
            DateTime inicio = DateTime.Today.AddDays(-14);
            DateTime fim = DateTime.Today.AddDays(1);

            return _context.Transacao
                .AsNoTracking()
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .Include(t => t.formaPagamento)
                .Where(t =>
                    (t.usuarioRemetenteId == usuarioId ||
                     t.usuarioDestinatarioId == usuarioId) &&
                    t.dataTransacao >= inicio &&
                    t.dataTransacao < fim)
                .ToList();
        }

        public List<Transacao> ListarPorTipoTransacao(
            Guid usuarioId,
            Guid formaPagamentoId)
        {
            return _context.Transacao
                .AsNoTracking()
                .Include(t => t.usuarioRemetente)
                .Include(t => t.usuarioDestinatario)
                .Include(t => t.formaPagamento)
                .Where(t =>
                    (t.usuarioRemetenteId == usuarioId ||
                     t.usuarioDestinatarioId == usuarioId) &&
                    t.formaPagamentoId == formaPagamentoId)
                .ToList();
        }
    }
}