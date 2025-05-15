using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdministrativoController : Controller
    {

        [HttpPost("CadastrarAdministrativo")]
        public IActionResult CadastrarAdministrativo([FromBody] Administrativo administrativo)
        {
            try
            {

                AdministrativoService service = new AdministrativoService();

                service.CadastrarAdministrativo(administrativo);

                return Ok(administrativo);

            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete ("RemoverAdministrativo")]

        public IActionResult RemoverAdministrativo(int Id)
        {

            try
            {

                AdministrativoService service =new AdministrativoService();

                return Ok(service.RemoverAdministrativo(Id));


            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado:" + ex.Message);

            }

        }


        [HttpGet("BuscarAdministrativo")]

        public IActionResult BuscarAdministrativo(int Id)
        {

            try
            {

                AdministrativoService service = new AdministrativoService();

                Administrativo administrativo = service.BuscarAdministrativo(Id);

                return Ok(administrativo);


            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex.Message);
            }

        }

        [HttpGet("ListarAdministrativo")]

        public IActionResult ListarAdministrativo(int Id)
        {

            try
            {

                AdministrativoService service = new AdministrativoService();

                List<Administrativo> administrativo = service.ListarAdministrativo();

                return Ok(administrativo);


            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex.Message);
            }

        }

        [HttpGet("AtualizarAdministrativo")]

        public IActionResult AtualizarAdministrativo([FromBody] Administrativo administrativo)
        {

            try
            {

                AdministrativoService service = new AdministrativoService();

                service.AtualizarAdministrativo(administrativo);

                return Ok(administrativo);


            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex.Message);
            }

        }

    }
}
