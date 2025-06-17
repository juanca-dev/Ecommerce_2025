using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController(VentaService service) : ControllerBase
    {
        private readonly VentaService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        {
            var carritos = await _service.GetCartsAsync();
            return Ok(carritos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetCarrito(int id)
        {
            var carrito = await _service.GetCartAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            return Ok(carrito);
        }

        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            await _service.SaveCartAsync(carrito);
            return CreatedAtAction(nameof(GetCarrito), new { id = carrito.Id }, carrito);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return BadRequest();
            }
            await _service.UpdateCartAsync(carrito);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            await _service.DeleteCartAsync(id);
            return NoContent();
        }
    }
}