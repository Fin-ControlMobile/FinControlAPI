using FinControlAPI.Applications.Services;
using FinControlAPI.DTOs.UsuarioDto;
using FinControlAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<LerUsuarioDto>> Listar()
        {
            var usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<LerUsuarioDto> ObterPorId(Guid id)
        {
            LerUsuarioDto usuario = _service.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public ActionResult Adicionar(CriarUsuarioDto usuarioDto)
        {
            try
            {
                _service.Adicionar(usuarioDto);
                return StatusCode(201);
            }

            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
