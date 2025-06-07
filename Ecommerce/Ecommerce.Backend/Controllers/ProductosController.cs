using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController(ProductoService service) : ControllerBase
    {
        private readonly ProductoService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _service.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest();
            }

            await _service.AddAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            var result = await _service.UpdateAsync(producto);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 9)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Los parámetros 'page' y 'pageSize' deben ser mayores a 0.");
            }

            var (productos, totalCount) = await _service.GetPaginatedAsync(page, pageSize);

            var response = new PaginationResponse<Producto>
            {
                Items = [.. productos],
                TotalCount = totalCount
            };

            return Ok(response);
        }
    }
}