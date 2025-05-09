using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : Controller
    {
        [HttpPost("CadastrarAluno")]
        public IActionResult CadastrarAluno()
        {
            try
            {
                AlunoService service = new AlunoService();

                return Ok(service);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


        }
    }
}
