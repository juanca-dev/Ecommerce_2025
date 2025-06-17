using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController(ProductoService service) : ControllerBase
    {
        private readonly ProductoService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comentario comentario)
        {
            if (comentario == null)
            {
                return BadRequest("Comentario no puede ser nulo.");
            }
            var resultado = await _service.AddComentarioAsync(comentario);
            if (!resultado.Success)
            {
                return BadRequest(resultado.Message);
            }
            return CreatedAtAction(nameof(GetComentarios), new { productoId = comentario.ProductoId }, resultado.Result);
        }

        [HttpGet("{productoId}")]
        public async Task<IActionResult> GetComentarios(int productoId)
        {
            var comentarios = await _service.GetComentariosAsync(productoId);
            return Ok(comentarios);
        }
    }
}