using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Serialization;
using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    public class ProvaController : Controller
    {

        [HttpPost("CadastrarProva")]
        public IActionResult CadastrarProva([FromBody] Prova prova)
        {
            try
            {
                ProvaService service = new ProvaService();

                service.CadastrarProva(prova);

                return Ok(prova);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex);
            }

        }


        [HttpDelete("RemoverProva")]
        public IActionResult RemoverProva(int Id)
        {
            try
            {
                ProvaService service = new ProvaService();

                bool Resultado = service.RemoverProva(Id);

                if (Resultado)
                {

                    return Ok(Resultado);
                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex);

            }
        
        
        
        }




        [HttpGet("BuscarProva")]

        public IActionResult BuscarProva(int Id)
        {
            try
            {
                ProvaService service = new ProvaService();

                Prova prova = service.BuscarProva(Id);

                return Ok(prova);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex);
            }
        
        
        }

        [HttpGet("ListarProva")]

        public IActionResult ListarProva()
        {

            try
            {
                ProvaService service = new ProvaService();

                List<Prova> prova = service.ListarProva();

                return Ok(prova);
            }
            catch (Exception ex)
            {

                return BadRequest("Erro inesperado: " + ex);

            }
        
        }


        [HttpPut("AtualizarProva")]

        public IActionResult AtualizarProva([FromBody]Prova prova)
        {
            try
            {
                ProvaService service = new ProvaService();

                service.AtualizarProva(prova);

                return Ok(prova);

            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex);
            }
        
        
        }


    }
}
