using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : Controller
    {
        [HttpPost("CadastrarAluno")]
        public IActionResult CadastrarAluno([FromBody] Aluno aluno)
        {
            try
            {
                AlunoService service = new AlunoService();

                service.CadastrarAluno(aluno);

                return Ok(aluno);

            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado:" + ex.Message);
            }

        }
        [HttpDelete("RemoverAluno")]

        public IActionResult RemoverAluno(int id)
        {
            try
            {
                AlunoService service = new AlunoService();

                return Ok(service.RemoverAluno(id));


            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado:" + ex.Message);

            }


        }

        [HttpGet("BuscarAluno")]

        public IActionResult BuscarAluno(int id)
        {
            try
            {
                AlunoService service = new AlunoService();

                Aluno aluno = service.buscarAluno(id);

                return Ok(aluno);

            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado:" + ex.Message);

            }

        }

        [HttpGet("ListarAlunos")]

        public IActionResult ListarAlunos(int id)
        {

            try
            {
                AlunoService service = new AlunoService();

                List<Aluno> alunos = new List<Aluno>();

                return Ok(alunos);
            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado:" + ex.Message);

            }

        
        }
        [HttpPut("AtualizarAluno")]

        public IActionResult AtualizarAluno(Aluno alunos)
        {

            try
            {
                AlunoService service = new AlunoService();

                service.AtualizarAluno(alunos);

                return Ok(alunos);


            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado:" + ex.Message);
            }
        
        
        }


    }
}
