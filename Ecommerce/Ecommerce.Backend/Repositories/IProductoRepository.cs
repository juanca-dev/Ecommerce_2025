using Ecommerce.Shared.Entities;

namespace Ecommerce.Backend.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();

        Task<Producto> GetByIdAsync(int id);

        Task AddAsync(Producto producto);

        Task UpdateAsync(Producto producto);

        Task DeleteAsync(int id);

        Task<Categoria> GetCategoriaByIdAsync(int id);
    }
}