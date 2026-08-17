using FinControlAPI.Domains;
using FinControlAPI.DTOs.TransacaoDto;
using FinControlAPI.Interfaces;

namespace FinControlAPI.Applications.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _repository;

        public TransacaoService(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        public LerTransacaoDto ConverterParaDto(Transacao transacao)
        {
            return new LerTransacaoDto
            {
                transacaoId = transacao.transacaoId,
                valorTransferencia = transacao.valorTransferencia,
                dataTransacao = transacao.dataTransacao,
                descricao = transacao.descricao,
                remetente = transacao.usuarioRemetente.nome,
                destinatario = transacao.usuarioDestinatario.nome,
                formaPagamento = transacao.formaPagamento.tipo
            };
        }

        public Transacao ManipularValor(Guid usuarioId, Transacao transacao)
        {
            if (transacao.usuarioRemetenteId == usuarioId)
            {
                transacao.valorTransferencia *= -1;
            }
            return transacao;
        }

        public List<LerTransacaoDto> Listar(Guid usuarioId)
        {
            List<LerTransacaoDto> listaTransacacoes = _repository.Listar(usuarioId).Select(t => ConverterParaDto(ManipularValor(usuarioId, t))).ToList();

            return  listaTransacacoes;
        }

        public List<LerTransacaoDto> ListarHoje(Guid usuarioId)
        {
            List<LerTransacaoDto> listaTransacacoes = _repository.ListarHoje(usuarioId).Select(t => ConverterParaDto(ManipularValor(usuarioId,t))).ToList();
            return listaTransacacoes;
        }

        public List<LerTransacaoDto> ListarOntem(Guid usuarioId)
        {
            List<LerTransacaoDto> listaTransacacoes = _repository.ListarOntem(usuarioId).Select(t => ConverterParaDto(ManipularValor(usuarioId, t))).ToList();
            return listaTransacacoes;
        }

        public List<LerTransacaoDto> ListarRecentes(Guid usuarioId)
        {
            List<LerTransacaoDto> listaTransacacoes = _repository.ListarRecentes(usuarioId).Select(t => ConverterParaDto(ManipularValor(usuarioId, t))).ToList();
            return listaTransacacoes;
        }

        public List<LerTransacaoDto> ListarPorTipoTransacao(
            Guid usuarioId,
            Guid formaPagamentoId)
        {
            List<LerTransacaoDto> listaTransacacoes = _repository.ListarPorTipoTransacao(usuarioId, formaPagamentoId).Select(t => ConverterParaDto(ManipularValor(usuarioId, t))).ToList();
            return listaTransacacoes;
        }
    }
}