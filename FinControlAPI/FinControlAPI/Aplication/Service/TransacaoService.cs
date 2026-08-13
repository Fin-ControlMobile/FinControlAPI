using FinControlAPI.DTOs;
using FinControlAPI.Interface;
using FinControlAPI.Aplication.Conversao;
using ChamaJussaAPI.Exceptions;
using FinControlAPI.Domains;

namespace FinControlAPI.Aplication.Service
{
    public class TransacaoService
    {
        public ITransacaoRepository _repository;

        public TransacaoService(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        public Transacao ManipularValor(Guid usuarioId,Transacao transacao)
        {
            if (transacao.usuarioRemetenteId == usuarioId)
            {
                transacao.valorTransferencia *= -1;
            }
            return transacao;
        }

        public List<LerTransacaoDto> ListarPorUsuario(Guid id)
        {
            if (!_repository.UsuarioExiste(id))
            {
                throw new DomainException("Usuário não encontrado");
            }
            return _repository.ListarPorUsuario(id).Select(t => ConverterTransacao.ConverterParaDto(ManipularValor(id, t))).ToList();
        }

        public LerTransacaoDto ObterTransacaoPorId(Guid id)
        {
            if (!_repository.TransacaoExiste(id))
            {
                throw new DomainException("Transação não encontrada");
            }
            return ConverterTransacao.ConverterParaDto(_repository.ObterPorId(id));
        }

        public List<LerTransacaoDto> ObterPorTipoPagamento(Guid usuarioId, Guid tipoId)
        {
            if (!_repository.UsuarioExiste(usuarioId))
            {
                throw new DomainException("Usuário não encontrado");
            }
            if (!_repository.TipoPagamentoExiste(tipoId))
            {
                throw new DomainException("Tipo de pagamento não encontrado");
            }

            List<LerTransacaoDto> dto = _repository.ObterPorTipoPagamento(usuarioId, tipoId).Select(t => ConverterTransacao.ConverterParaDto(ManipularValor(usuarioId, t))).ToList();
       
            if(dto.Count == 0)
            {
                throw new DomainException("Nenhuma transação encontrada para o tipo de pagamento informado");
            }

            return dto;
        }   

        public List<LerTransacaoDto> ObterPorExtracao(Guid usuarioId, bool recebimento)
        {
            if (!_repository.UsuarioExiste(usuarioId))
            {
                throw new DomainException("Usuário não encontrado");
            }


            List<LerTransacaoDto> dto = _repository.ObterPorExtracao(usuarioId, recebimento).Select(t => ConverterTransacao.ConverterParaDto(ManipularValor(usuarioId, t))).ToList();
            if(dto.Count == 0)
            {
                throw new DomainException("Nenhuma transação encontrada para o tipo de extração informado");
            }

            return dto;
        }

        public void FazerTransferencia(CriarTransacaoDto transacaoDto)
        {
            // Nao tera validacao de destinatario, pois o mesmo pode nao existir no sistema
            if (!_repository.UsuarioExiste(transacaoDto.usuarioRemetenteId))
            {
                throw new DomainException("Usuário remetente não encontrado");
            }


            if (!_repository.TipoPagamentoExiste(transacaoDto.formaPagamentoId))
            {
                throw new DomainException("Tipo de pagamento não encontrado");
            }

            if(transacaoDto.valorTransferencia <= 0)
            {
                throw new DomainException("Valor de transferência nao pode ser negativo");
            }

            if(transacaoDto.usuarioRemetenteId == transacaoDto.usuarioDestinatarioId)
            {
                throw new DomainException("Usuário remetente e destinatário não podem ser o mesmo");
            }

            Transacao transacao = ConverterTransacao.ConverterParaDomain(transacaoDto);

            _repository.FazerTransferencia(transacao);
        }
    }
}
