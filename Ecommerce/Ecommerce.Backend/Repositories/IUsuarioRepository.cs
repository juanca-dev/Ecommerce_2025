using Ecommerce.Shared.Entities;

namespace Ecommerce.Backend.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();

        Task<Usuario> GetByIdAsync(int id);

        Task AddAsync(Usuario usuario);

        Task UpdateAsync(Usuario usuario);

        Task DeleteAsync(int id);

        Task<bool> ExisteCorreoAsync(string correo);

        Task ChangePasswordAsync(Usuario usuario);

        Task<Usuario> GetByEmailAsync(string email);

        Task ResetPasswordAsync(Usuario usuario);

        Task ActiveUser(Usuario usuario);
    }
}