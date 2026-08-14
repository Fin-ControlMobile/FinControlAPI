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

        public List<LerTransacaoDto> Listar(Guid usuarioId)
        {
            List<Transacao> transacoes = _repository.Listar(usuarioId);

            List<LerTransacaoDto> transacoesDto = new List<LerTransacaoDto>();

            foreach (var transacao in transacoes)
            {
                transacoesDto.Add(ConverterParaDto(transacao));
            }

            return transacoesDto;
        }

        public List<LerTransacaoDto> ListarHoje(Guid usuarioId)
        {
            List<Transacao> transacoes = _repository.ListarHoje(usuarioId);

            List<LerTransacaoDto> transacoesDto = new List<LerTransacaoDto>();

            foreach (var transacao in transacoes)
            {
                transacoesDto.Add(ConverterParaDto(transacao));
            }

            return transacoesDto;
        }

        public List<LerTransacaoDto> ListarOntem(Guid usuarioId)
        {
            List<Transacao> transacoes = _repository.ListarOntem(usuarioId);

            List<LerTransacaoDto> transacoesDto = new List<LerTransacaoDto>();

            foreach (var transacao in transacoes)
            {
                transacoesDto.Add(ConverterParaDto(transacao));
            }

            return transacoesDto;
        }

        public List<LerTransacaoDto> ListarRecentes(Guid usuarioId)
        {
            List<Transacao> transacoes = _repository.ListarRecentes(usuarioId);

            List<LerTransacaoDto> transacoesDto = new List<LerTransacaoDto>();

            foreach (var transacao in transacoes)
            {
                transacoesDto.Add(ConverterParaDto(transacao));
            }

            return transacoesDto;
        }

        public List<LerTransacaoDto> ListarPorTipoTransacao(
            Guid usuarioId,
            Guid formaPagamentoId)
        {
            List<Transacao> transacoes =
                _repository.ListarPorTipoTransacao(usuarioId, formaPagamentoId);

            List<LerTransacaoDto> transacoesDto = new List<LerTransacaoDto>();

            foreach (var transacao in transacoes)
            {
                transacoesDto.Add(ConverterParaDto(transacao));
            }

            return transacoesDto;
        }
    }
}