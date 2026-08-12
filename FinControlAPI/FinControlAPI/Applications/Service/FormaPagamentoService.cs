using FinControlAPI.Domains;
using FinControlAPI.DTOs.FormaPagamento;
using FinControlAPI.Interface;
using FinControlAPI.Repositories;
using GestaoPatrimonios.Exceptions;

namespace FinControlAPI.Applications.Service
{
    public class FormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _repository;

        public FormaPagamentoService(IFormaPagamentoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarFormaPagamentoDto> Listar()
        {
            List<FormaPagamento> formasPagamento = _repository.Listar();

            List<ListarFormaPagamentoDto> formasPagamentoDto = formasPagamento.Select(fp => new ListarFormaPagamentoDto
            {
                formaId = fp.formaId,
                tipo = fp.tipo,
            }).ToList();

            return formasPagamentoDto;
        }

        public void Adicionar(CriarFormaPagamentoDto dto)
        {
            FormaPagamento formaPagamentoExistente = _repository.BuscarPorNome(dto.tipo);

            if(formaPagamentoExistente != null)
            {
                throw new DomainException("Ja existe uma forma de pagamento cadastrada com esse nome");
            }

            FormaPagamento formaPagamento = new FormaPagamento
            {
                tipo = dto.tipo
            };

            _repository.Adicionar(formaPagamento);
        }
    }
}
