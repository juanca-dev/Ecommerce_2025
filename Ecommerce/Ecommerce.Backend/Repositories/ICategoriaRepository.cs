using Ecommerce.Shared.Entities;

namespace Ecommerce.Backend.Repositories
{
    public interface ICategoriaRepository
    {
        // de entrada le agregamos las 5 operaciones que se hacen en una base datos.

        Task<IEnumerable<Categoria>> GetAllAsync();

        Task<Categoria> GetByIdAsync(int id);

        Task AddAsync(Categoria categoria);
        Task UpdateAsync(Categoria categoria);
        Task DeleteAsync(int id);

        Task<bool> ExisteNombreAsync(string nombre);     
    }
}
