using Azure.Messaging;
using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            // ME CONECTO AL SERVICES
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
            var result = await _service.AddAsync(nuevaCategoria);
            return CreatedAtAction(nameof(GetCategoria), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> ActualizarCategoria(int id, [FromBody] Categoria categoriaActualizada)
        {
            if (id != categoriaActualizada.Id)
            {
                return BadRequest("El ID de la categoría no coincide.");
            }

            await _service.UpdateAsync(categoriaActualizada);
            return NoContent();
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
