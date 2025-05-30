using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(UsuarioService service) : ControllerBase
    {
        private readonly UsuarioService _service = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            var nuevoUsuario = await _service.AddAsync(usuario);
            return CreatedAtAction(nameof(Get), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || usuario.Id != id)
            {
                return BadRequest();
            }
            var usuarioExistente = await _service.GetByIdAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }
            await _service.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string email)
        {
            var usuario = await _service.GetUsuarioAsync(email);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.ChangePasswordAsync(model);
                return Ok("Contraseña cambiada con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.ResetPasswordAsync(model);
                return Ok("Contraseña restablecida con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ActiveUser")]
        public async Task<IActionResult> ActiveUser([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.ActiveUser(usuario);
                return Ok("Usuario activado con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}