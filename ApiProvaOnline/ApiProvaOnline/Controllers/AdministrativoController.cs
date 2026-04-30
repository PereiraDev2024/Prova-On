using ApiProvaOnline.Model;
using ApiProvaOnline.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiProvaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrativoController : ControllerBase
    {
        private readonly AdministrativoService _service;

        public AdministrativoController(AdministrativoService service)
        {
            _service = service;
        }

        [HttpPost("CadastrarAdministrativo")]
        public async Task<IActionResult> CadastrarAdministrativo([FromBody] Administrativo administrativo)
        {
            try
            {
                await _service.CadastrarAdministrativoAsync(administrativo);
                return Ok(administrativo);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpDelete("RemoverAdministrativo")]
        public async Task<IActionResult> RemoverAdministrativo(int Id)
        {
            try
            {
                bool resultado = await _service.RemoverAdministrativoAsync(Id);
                if (resultado)
                    return Ok("Alteração realizada com sucesso!");
                else
                    return NotFound("Administrador não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("BuscarAdministrativo")]
        public async Task<IActionResult> BuscarAdministrativo(int Id)
        {
            try
            {
                var administrativo = await _service.BuscarAdministrativoAsync(Id);
                if (administrativo == null) return NotFound();
                return Ok(administrativo);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpGet("ListarAdministrativo")]
        public async Task<IActionResult> ListarAdministrativo()
        {
            try
            {
                var administrativos = await _service.ListarAdministrativoAsync();
                return Ok(administrativos);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }

        [HttpPut("AtualizarAdministrativo")]
        public async Task<IActionResult> AtualizarAdministrativo([FromBody] Administrativo administrativo)
        {
            try
            {
                await _service.AtualizarAdministrativoAsync(administrativo);
                return Ok(administrativo);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inesperado: " + ex.Message);
            }
        }
    }
}
