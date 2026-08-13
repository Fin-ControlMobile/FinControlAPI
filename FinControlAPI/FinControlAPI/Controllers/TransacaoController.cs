using ChamaJussaAPI.Exceptions;
using FinControlAPI.Aplication.Service;
using FinControlAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        public TransacaoService _service;
        public TransacaoController(TransacaoService service)
        {
            _service = service;
        }

        [HttpGet("ListaPorUsuario/{id}")]
        public ActionResult<List<LerTransacaoDto>> ListarPorUsuario(Guid id)
        {
            try
            {
            return Ok(_service.ListarPorUsuario(id));
            } catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ObterTransacaoPorId/{id}")]
        public ActionResult<LerTransacaoDto> ObterTransacaoPorId(Guid id)
        {
            try
            {
                return Ok(_service.ObterTransacaoPorId(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ObterPorTipoPagamento/{usuarioId}/{tipoId}")]
        public ActionResult<List<LerTransacaoDto>> ObterPorTipoPagamento(Guid usuarioId, Guid tipoId)
        {
            try
            {
                return Ok(_service.ObterPorTipoPagamento(usuarioId, tipoId));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ObterPorExtracao/{usuarioId}/{recebimento}")]
        public ActionResult<List<LerTransacaoDto>> ObterPorExtracao(Guid usuarioId, bool recebimento)
        {
            try
            {
                return Ok(_service.ObterPorExtracao(usuarioId, recebimento));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("FazerTransferencia")]
        public ActionResult FazerTransferencia(CriarTransacaoDto dto)
        {
            try
            {
                _service.FazerTransferencia(dto);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
