using Ecommerce.Shared.Entities;

namespace Ecommerce.Backend.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();

        Task<Categoria> GetByIdAsync(int id);

        Task AddAsync(Categoria categoria);

        Task UpdateAsync(Categoria categoria);

        Task DeleteAsync(int id);

        Task<bool> ExisteNombreAsync(string nombre);
    }
}