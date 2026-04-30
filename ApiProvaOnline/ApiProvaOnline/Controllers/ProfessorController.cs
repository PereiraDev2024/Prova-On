using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService _service;

        public ProfessorController(ProfessorService service)
        {
            _service = service;
        }

        [HttpPost("LoginProfessor")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                return BadRequest(new { mensagem = "Email e senha são obrigatórios." });

            var professor = await _service.LoginProfessorAsync(request.Email, request.Senha);

            if (professor == null)
                return Unauthorized(new { mensagem = "Credenciais inválidas." });

            professor.Senha = string.Empty;

            return Ok(new
            {
                sucesso = true,
                professor = professor
            });
        }

        [HttpPost("CadastrarProfessor")]
        public async Task<IActionResult> CadastrarProfessor([FromBody] Professor professor)
        {
            try
            {
                await _service.CadastrarProfessorAsync(professor);
                return Ok(professor);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("RemoverProfessor")]
        public async Task<IActionResult> RemoverProfessor(int Id)
        {
            try
            {
                return Ok(await _service.RemoverProfessorAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarProfessor")]
        public async Task<IActionResult> BuscarProfessor(int Id)
        {
            try
            {
                var professor = await _service.BuscarProfessorAsync(Id);
                if (professor == null) return NotFound();
                return Ok(professor);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarProfessor")]
        public async Task<IActionResult> ListarProfessor()
        {
            try
            {
                var professores = await _service.ListarProfessoresAsync();
                return Ok(professores);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarProfessor")]
        public async Task<IActionResult> AtualizarProfessor([FromBody] Professor professor)
        {
            try
            {
                await _service.AtualizarProfessorAsync(professor);
                return Ok(professor);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
