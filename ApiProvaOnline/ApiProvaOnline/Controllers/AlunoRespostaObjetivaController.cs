using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoRespostaObjetivaController : ControllerBase
    {
        private readonly AlunoRespostaObjetivaService _service;
        private readonly ICorrecaoService _correcaoService;

        public AlunoRespostaObjetivaController(
            AlunoRespostaObjetivaService service,
            ICorrecaoService correcaoService)
        {
            _service = service;
            _correcaoService = correcaoService;
        }

        [HttpPost("CadastrarAlunoRespostaObjetiva")]
        public async Task<IActionResult> CadastrarAlunoRespostaObjetiva([FromBody] AlunoRespostaObjetiva alunoRespostaObjetiva)
        {
            try
            {
                await _service.CadastrarAlunoRespostaObjetivaAsync(alunoRespostaObjetiva);
                return Ok(alunoRespostaObjetiva);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("RemoverAlunoRespostaObjetivo")]
        public async Task<IActionResult> RemoverAlunoRespostaObjetiva(int Id)
        {
            try
            {
                bool resultado = await _service.RemoverAlunoRespostaObjetivaAsync(Id);
                if (resultado)
                    return Ok("Alteração realizada com sucesso!");
                else
                    return NotFound("Aluno não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarAlunoRespostaObjetiva")]
        public async Task<IActionResult> BuscarAlunoRespostaObjetiva(int Id)
        {
            try
            {
                var resposta = await _service.BuscarAlunoRespostaObjetivaAsync(Id);
                if (resposta == null) return NotFound();
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarAlunoRespostaObjetiva")]
        public async Task<IActionResult> AtualizarAlunoRespostaObjetiva([FromBody] AlunoRespostaObjetiva alunoRespostaObjetiva)
        {
            try
            {
                await _service.AtualizarAlunoRespostaObjetivaAsync(alunoRespostaObjetiva);
                return Ok(alunoRespostaObjetiva);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPost("CorrigirProva")]
        public async Task<IActionResult> CorrigirProva(int alunoId, int provaId)
        {
            try
            {
                await _correcaoService.CorrigirProvaAsync(alunoId, provaId);
                return Ok("Prova corrigida com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao corrigir prova: " + ex.Message);
            }
        }

        [HttpGet("PontuacaoAluno")]
        public async Task<IActionResult> CalcularPontuacao(int alunoId, int provaId)
        {
            try
            {
                var pontuacao = await _correcaoService.CalcularPontuacaoAsync(alunoId, provaId);
                return Ok(new { alunoId, provaId, pontuacao });
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao calcular pontuação: " + ex.Message);
            }
        }

        [HttpGet("ListarPontuacoesProva/{provaId}")]
        public async Task<IActionResult> ListarPontuacoesProva(int provaId)
        {
            try
            {
                var pontuacoes = await _correcaoService.ListarPontuacoesProvaAsync(provaId);
                return Ok(pontuacoes);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar pontuações: " + ex.Message);
            }
        }
    }
}
