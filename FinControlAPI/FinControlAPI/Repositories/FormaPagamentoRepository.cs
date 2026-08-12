using FinControlAPI.Contexts;
using FinControlAPI.Domains;
using FinControlAPI.Interface;

namespace FinControlAPI.Repositories
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly FinControlDbContext _context;

        public FormaPagamentoRepository (FinControlDbContext context)
        {
            _context = context;
        }

        public List<FormaPagamento> Listar()
        {
            return _context.FormaPagamento.ToList();
        }

        public FormaPagamento BuscarPorNome(string nome)
        {
            return _context.FormaPagamento.FirstOrDefault(fp => fp.tipo == nome);
        }

        public void Adicionar(FormaPagamento formaPagamento)
        {
            _context.FormaPagamento.Add(formaPagamento);
            _context.SaveChanges();
        }
    }
}
