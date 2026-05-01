using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [EnableCors("PermitirLocalhost")]
    [ApiController]
    [Route("[controller]")]
    public class ProvaController : ControllerBase
    {
        private readonly ProvaService _service;

        public ProvaController(ProvaService service)
        {
            _service = service;
        }

        [HttpPost("CadastrarProva")]
        public async Task<IActionResult> CadastrarProva([FromBody] Prova prova)
        {
            try
            {
                await _service.CadastrarProvaAsync(prova);
                return Ok(prova);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("RemoverProva")]
        public async Task<IActionResult> RemoverProva(int Id)
        {
            try
            {
                bool resultado = await _service.RemoverProvaAsync(Id);
                if (resultado)
                    return Ok(resultado);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarProva")]
        public async Task<IActionResult> BuscarProva(int Id)
        {
            try
            {
                var prova = await _service.BuscarProvaAsync(Id);
                if (prova == null) return NotFound();
                return Ok(prova);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarProva")]
        public async Task<IActionResult> ListarProva()
        {
            try
            {
                var provas = await _service.ListarProvaAsync();
                return Ok(provas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarProva")]
        public async Task<IActionResult> AtualizarProva([FromBody] Prova prova)
        {
            try
            {
                await _service.AtualizarProvaAsync(prova);
                return Ok(prova);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarProvasProfessor/{professorId}")]
        public async Task<IActionResult> ListarProvasProfessor(int professorId)
        {
            try
            {
                var provas = await _service.ListarProvasProfessorAsync(professorId);
                return Ok(provas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar provas: {ex.Message}");
            }
        }

        [HttpGet("BuscarProvaPorCodigo/{codigo}")]
        public async Task<IActionResult> BuscarProvaPorCodigo(string codigo)
        {
            try
            {
                var prova = await _service.BuscarProvaPorCodigoAsync(codigo);
                if (prova == null) return NotFound("Código de acesso inválido.");
                return Ok(prova);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
