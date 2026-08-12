using FinControlAPI.Applications.Service;
using FinControlAPI.DTOs.FormaPagamento;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly FormaPagamentoService _service;

        public FormaPagamentoController(FormaPagamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarFormaPagamentoDto>> Listar()
        {
            List<ListarFormaPagamentoDto> formasPagamento = _service.Listar();
            return Ok(formasPagamento);
        }

        [HttpPost]
        public ActionResult Adicionar (CriarFormaPagamentoDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
