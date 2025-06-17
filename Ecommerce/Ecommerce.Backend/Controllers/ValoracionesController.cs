using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValoracionesController(ProductoService service) : ControllerBase
    {
        private readonly ProductoService _service = service;

        [HttpGet("{id}")]

        public async Task<IActionResult> GetValoracionPromedio(int id)
        {
            var valoracion = await _service.GetRatingAsync(id);
            return Ok(valoracion);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] Valoracion valoracion)
        {
            if (valoracion == null)
            {
                return BadRequest();
            }

            var result = await _service.AddRatingAsync(valoracion);
            if (!result.Success)
            {
                return BadRequest(result.Message);

            }
            return CreatedAtAction(nameof(GetValoracionPromedio), new { id = valoracion.ProductoId }, valoracion);

        }

    }
}
