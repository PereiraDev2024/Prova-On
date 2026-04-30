using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerguntaController : ControllerBase
    {
        private readonly PerguntaService _service;

        public PerguntaController(PerguntaService service)
        {
            _service = service;
        }

        [HttpPost("CadastrarPergunta")]
        public async Task<IActionResult> CadastrarPergunta([FromBody] Pergunta pergunta)
        {
            try
            {
                await _service.CadastrarPerguntaAsync(pergunta);
                return Ok(pergunta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("DeletarPergunta")]
        public async Task<IActionResult> RemoverPergunta(int Id)
        {
            try
            {
                bool resultado = await _service.RemoverPerguntaAsync(Id);
                if (resultado)
                    return Ok("Pergunta removida com sucesso");
                else
                    return NotFound("Pergunta não encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarPergunta")]
        public async Task<IActionResult> BuscarPergunta(int id)
        {
            try
            {
                var pergunta = await _service.BuscarPerguntaAsync(id);
                if (pergunta == null) return NotFound();
                return Ok(pergunta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarPergunta")]
        public async Task<IActionResult> ListarPergunta()
        {
            try
            {
                var perguntas = await _service.ListarPerguntaAsync();
                return Ok(perguntas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarPorProva/{provaId}")]
        public async Task<IActionResult> ListarPorProva(int provaId)
        {
            try
            {
                var perguntas = await _service.ListarPorProvaAsync(provaId);
                return Ok(perguntas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarPergunta")]
        public async Task<IActionResult> AtualizarPergunta([FromBody] Pergunta pergunta)
        {
            try
            {
                await _service.AtualizarPerguntaAsync(pergunta);
                return Ok(pergunta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
