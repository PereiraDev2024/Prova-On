using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerguntaObjetivaController : ControllerBase
    {
        private readonly PerguntaObjetivaService _service;

        public PerguntaObjetivaController(PerguntaObjetivaService service)
        {
            _service = service;
        }

        [HttpPost("CadastrarPerguntaObjetiva")]
        public async Task<IActionResult> CadastrarPerguntaObjetiva([FromBody] PerguntaObjetiva perguntaObjetiva)
        {
            try
            {
                await _service.CadastrarPerguntaObjetivaAsync(perguntaObjetiva);
                return Ok(perguntaObjetiva);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("DeletarPerguntaObjetiva")]
        public async Task<IActionResult> RemoverPerguntaObjetiva(int id)
        {
            try
            {
                bool resultado = await _service.RemoverPerguntaObjetivaAsync(id);
                if (resultado)
                    return Ok("PerguntaObjetiva Removida com sucesso");
                else
                    return NotFound("PerguntaObjetiva Não encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarPerguntaObjetiva")]
        public async Task<IActionResult> BuscarPerguntaObjetiva(int id)
        {
            try
            {
                var pergunta = await _service.BuscarPerguntaObjetivaAsync(id);
                if (pergunta == null) return NotFound();
                return Ok(pergunta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarPerguntasObjetivas")]
        public async Task<IActionResult> ListarPerguntasObjetivas()
        {
            try
            {
                var perguntas = await _service.ListarPerguntasObjetivasAsync();
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
                var dto = perguntas.Select(p => new ListarPerguntarProvaDTO
                {
                    id = p.Id,
                    titulo = p.Titulo,
                    descricao = p.Descricao,
                    opcaoa = p.OpcaoA,
                    opcaob = p.OpcaoB,
                    opcaoc = p.OpcaoC,
                    opcaod = p.OpcaoD,
                    pontuacao = p.Pontuacao
                });
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarPerguntaObjetiva")]
        public async Task<IActionResult> AtualizarPerguntaObjetiva([FromBody] PerguntaObjetiva perguntaObjetiva)
        {
            try
            {
                await _service.AtualizarPerguntaObjetivaAsync(perguntaObjetiva);
                return Ok(perguntaObjetiva);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
