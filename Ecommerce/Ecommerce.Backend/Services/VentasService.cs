using Ecommerce.Backend.Repositories;
using Ecommerce.Shared.Entities;

namespace Ecommerce.Backend.Services
{
    public class VentaService(IVentaRepository repository, IProductoRepository productoRepository)
    {
        private readonly IVentaRepository _repository = repository;
        private readonly IProductoRepository _productRepository = productoRepository;

        public async Task SaveCartAsync(Carrito carrito)
        {
            await _repository.SaveCartAsync(carrito);
        }

        public async Task<IEnumerable<Carrito>> GetCartsAsync()
        {
            return await _repository.GetCartsAsync();
        }

        public async Task<Carrito> GetCartAsync(int id)
        {
            return await _repository.GetCartAsync(id);
        }

        public async Task UpdateCartAsync(Carrito carrito)
        {
            await _repository.UpdateCartAsync(carrito);
        }

        public async Task DeleteCartAsync(int id)
        {
            await _repository.DeleteCartAsync(id);
        }
    }
}