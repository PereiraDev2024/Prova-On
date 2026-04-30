using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoRespostaController : ControllerBase
    {
        private readonly AlunoRespostaService _service;

        public AlunoRespostaController(AlunoRespostaService service)
        {
            _service = service;
        }

        [HttpPost("CadastrarAlunoResposta")]
        public async Task<IActionResult> CadastrarAlunoResposta([FromBody] AlunoResposta alunoResposta)
        {
            try
            {
                await _service.CadastrarAlunoRespostaAsync(alunoResposta);
                return Ok(alunoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("RemoverAlunoResposta")]
        public async Task<IActionResult> RemoverAlunoResposta(int id)
        {
            try
            {
                bool resultado = await _service.RemoverAlunoRespostaAsync(id);
                if (resultado)
                    return Ok(resultado);
                else
                    return NotFound("Resposta não encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarAlunoResposta")]
        public async Task<IActionResult> BuscarAlunoResposta(int id)
        {
            try
            {
                var resposta = await _service.BuscarAlunoRespostaAsync(id);
                if (resposta == null) return NotFound();
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarAlunoResposta")]
        public async Task<IActionResult> AtualizarAlunoResposta([FromBody] AlunoResposta alunoResposta)
        {
            try
            {
                bool resultado = await _service.AtualizarAlunoRespostaAsync(alunoResposta);
                if (resultado)
                    return Ok(resultado);
                else
                    return NotFound("Resposta não encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
