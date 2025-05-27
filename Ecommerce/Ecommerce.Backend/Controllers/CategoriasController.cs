using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController(CategoriaService service) : ControllerBase
    {
        private readonly CategoriaService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _service.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _service.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> CrearCategoria([FromBody] Categoria nuevaCategoria)
        {
            if (nuevaCategoria == null)
            {
                return BadRequest();
            }
            var response = await _service.AddAsync(nuevaCategoria);
            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }
            return CreatedAtAction(nameof(GetCategoria), new { id = response.Result!.Id }, response.Result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCategoria(int id, [FromBody] Categoria categoriaActualizada)
        {
            if (id != categoriaActualizada.Id)
            {
                return BadRequest("El ID de la categoría no coincide.");
            }
            var response = await _service.UpdateAsync(categoriaActualizada);

            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }

            return CreatedAtAction(nameof(GetCategoria), new { id = response.Result!.Id }, response.Result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCategoria(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}