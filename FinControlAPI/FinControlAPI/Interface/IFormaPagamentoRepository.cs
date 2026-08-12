using FinControlAPI.Domains;

namespace FinControlAPI.Interface
{
    public interface IFormaPagamentoRepository
    {
        List<FormaPagamento> Listar();
        FormaPagamento BuscarPorNome(string nome);
        void Adicionar(FormaPagamento formaPagamento);

    }
}
