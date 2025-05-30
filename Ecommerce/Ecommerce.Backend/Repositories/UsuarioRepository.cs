using Ecommerce.Backend.Data;
using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Backend.Repositories
{
    public class UsuarioRepository(DataContext context) : IUsuarioRepository
    {
        private readonly DataContext _context = context;

        public async Task ActiveUser(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Activo = usuario.Activo;
                _context.Entry(usuarioExistente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id) ?? throw new Exception($"Usuario con id {id} no fue encontrado.");
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            return await _context.Usuarios.AnyAsync(c => c.Correo == correo);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios
               .ToListAsync();

            return usuarios;
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            var usuario = await _context.Usuarios
              .FirstOrDefaultAsync(u => u.Correo == email) ?? throw new Exception($"Usuario con email {email} no fue encontrado.");
            return usuario;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios
               .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception($"Usuario con id {id} no fue encontrado.");

            return usuario;
        }

        public async Task ResetPasswordAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.URLFoto = usuario.URLFoto;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }
    }
}