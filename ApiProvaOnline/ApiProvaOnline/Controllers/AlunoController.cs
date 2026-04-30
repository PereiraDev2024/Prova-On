using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _service;

        public AlunoController(AlunoService service)
        {
            _service = service;
        }

        [HttpPost("CadastrarAluno")]
        public async Task<IActionResult> CadastrarAluno([FromBody] Aluno aluno)
        {
            try
            {
                await _service.CadastrarAlunoAsync(aluno);
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("RemoverAluno")]
        public async Task<IActionResult> RemoverAluno(int id)
        {
            try
            {
                bool resultado = await _service.RemoverAlunoAsync(id);
                if (resultado)
                    return Ok("O Aluno foi removido");
                else
                    return BadRequest("O Aluno não foi encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarAluno")]
        public async Task<IActionResult> BuscarAluno(int id)
        {
            try
            {
                var aluno = await _service.BuscarAlunoAsync(id);
                if (aluno == null) return NotFound();
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarAlunos")]
        public async Task<IActionResult> ListarAlunos()
        {
            try
            {
                var alunos = await _service.ListarAlunoAsync();
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarAluno")]
        public async Task<IActionResult> AtualizarAluno([FromBody] Aluno aluno)
        {
            try
            {
                await _service.AtualizarAlunoAsync(aluno);
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("Historico")]
        public async Task<ActionResult<IEnumerable<HistoricoAlunoDTO>>> ObterHistorico(int id)
        {
            try
            {
                var historico = await _service.HistoricoDeAlunoAsync(id);
                var lista = historico.ToList();
                if (lista.Count == 0)
                    return NotFound("Nenhum histórico encontrado para o aluno.");
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
