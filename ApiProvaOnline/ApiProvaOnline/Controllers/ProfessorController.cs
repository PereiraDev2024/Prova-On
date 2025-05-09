using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : Controller
    {

        [HttpPost("CadastrarProfessor")]
        public IActionResult CadastrarProfessor([FromBody] Professor professor)
        {
            try
            {
                ProfessorService service = new ProfessorService();

                service.CadastrarProfessor(professor);

                return Ok(professor);


            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado:" + ex.Message);

            }
        }

        [HttpDelete("RemoverProfessor")]

        public IActionResult RemoverProfessor(int Id)
        {
            try
            {
                ProfessorService service = new ProfessorService();

                return Ok(service.RemoverProfessor(Id));



            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado:" + ex.Message);

            }


        }


        [HttpGet("BuscarProfessor")]

        public IActionResult BuscarProfessor(string Id)
        {
            try
            {
                ProfessorService service = new ProfessorService();

                Professor professor = service.BuscarProfessor(Id);

                return Ok(professor);

            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado:" + ex.Message);

            }

        }



        [HttpGet("ListarProfessor")]

        public IActionResult ListarProfessor()
        {
            try
            {
                ProfessorService service = new ProfessorService();

               List<Professor> professor = service.ListarProfessores();

                return Ok(professor);
            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex.Message);

            }


        }

    }

}
     
