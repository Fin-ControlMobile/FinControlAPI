using FinControlAPI.Applications.Services;
using FinControlAPI.DTOs.AutenticacaoDto;
using Microsoft.AspNetCore.Mvc;

namespace FinControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _service.Login(loginDto);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("redefinir-senha")]
        public async Task<IActionResult> RedefinirSenha( [FromBody] RedefinirSenhaDto dto)
        {
            var resultado = await _service.RedefinirSenhaAsync(dto);

            if (!resultado)
                return BadRequest("Token inválido ou expirado.");

            return Ok("Senha redefinida com sucesso.");
        }

        [HttpPost("solicitar-redefinicao")]
        public async Task<IActionResult> SolicitarRedefinicaoSenha([FromBody] SolicitarRedefinicaoSenhaDto dto)
        {
            var resultado = await _service.SolicitarRedefinicaoSenhaAsync(dto);

            if (!resultado)
                return NotFound("E-mail não encontrado.");

            return Ok("Solicitação de redefinição realizada com sucesso.");
        }
    }
}