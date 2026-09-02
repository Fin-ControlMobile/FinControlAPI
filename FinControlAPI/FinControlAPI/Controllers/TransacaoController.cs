using System.Security.Claims;
using FinControlAPI.Applications.Services;
using FinControlAPI.DTOs.TransacaoDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _service;

        public TransacaoController(TransacaoService service)
        {
            _service = service;
        }

        private Guid ObterUsuarioId()
        {
            return Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<LerTransacaoDto>> Listar()
        {
            Guid usuarioId = ObterUsuarioId();

            return Ok(_service.Listar(usuarioId));
        }

        [HttpGet("hoje")]
        [Authorize]
        public ActionResult<List<LerTransacaoDto>> ListarHoje()
        {
            Guid usuarioId = ObterUsuarioId();

            return Ok(_service.ListarHoje(usuarioId));
        }

        [HttpGet("ontem")]
        [Authorize]
        public ActionResult<List<LerTransacaoDto>> ListarOntem()
        {
            Guid usuarioId = ObterUsuarioId();

            return Ok(_service.ListarOntem(usuarioId));
        }

        [HttpGet("recentes")]
        [Authorize]
        public ActionResult<List<LerTransacaoDto>> ListarRecentes()
        {
            Guid usuarioId = ObterUsuarioId();

            return Ok(_service.ListarRecentes(usuarioId));
        }

        [HttpGet("forma/{formaPagamentoId}")]
        [Authorize]
        public ActionResult<List<LerTransacaoDto>> ListarPorTipoTransacao(
            Guid formaPagamentoId)
        {
            Guid usuarioId = ObterUsuarioId();

            return Ok(
                _service.ListarPorTipoTransacao(
                    usuarioId,
                    formaPagamentoId
                )
            );
        }

        [HttpGet("obterTransacao/{transacaoId}")]
        [Authorize]
        public ActionResult<LerTransacaoDto> ObterTransacao(Guid transacaoId)
        {
            Guid usuarioId = ObterUsuarioId();

            return Ok(
                _service.ObterTransacaoPorId(
                    usuarioId,
                    transacaoId
                    )
                );
        }
    }
}