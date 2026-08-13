using FinControlAPI.Domains;
using FinControlAPI.DTOs;

namespace FinControlAPI.Aplication.Conversao
{
    public class ConverterTransacao
    {
        public static LerTransacaoDto ConverterParaDto(Transacao transacao)
        {
            return new LerTransacaoDto
            {
                transacaoId = transacao.transacaoId,
                dataTransacao = transacao.dataTransacao,
                valorTransferencia = transacao.valorTransferencia,
                descricao = transacao.descricao,
                formaPagamentoId = transacao.formaPagamentoId,
                formaPagamentoNome = transacao.formaPagamento.tipo,
                usuarioRemetenteId = transacao.usuarioRemetenteId,
                usuarioRemetenteNome = transacao.usuarioRemetente.nome,
                usuarioDestinatarioId = transacao.usuarioDestinatarioId,
                usuarioDestinatarioNome = transacao.usuarioDestinatario.nome,
            };
        }

        public static Transacao ConverterParaDomain(CriarTransacaoDto transacaoDto)
        {
            return new Transacao
            {
                valorTransferencia = transacaoDto.valorTransferencia,
                descricao = transacaoDto.descricao,
                formaPagamentoId = transacaoDto.formaPagamentoId,
                usuarioDestinatarioId = transacaoDto.usuarioDestinatarioId,
                usuarioRemetenteId = transacaoDto.usuarioRemetenteId
            };
        }
    }
}
