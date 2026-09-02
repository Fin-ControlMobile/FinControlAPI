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
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            try
            {
                var token = _service.Login(loginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}