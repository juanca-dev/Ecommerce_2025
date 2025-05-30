using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Backend.Data
{
    public class SeedDb(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriasAsync();
            await CheckUsersAsync("Super Admin", "tecnologershn@gmail.com", "Tecno.2025", "Administrador", "+50433077964");
        }

        private async Task<Usuario> CheckUsersAsync(string nombre, string correo, string password, string perfil, string telefono)
        {
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuarioExistente != null)
            {
                return usuarioExistente!;
            }

            Usuario usuario = new()
            {
                Correo = correo,
                Nombre = nombre,
                Perfil = perfil,
                URLFoto = "https://res.cloudinary.com/dbsaxzz05/image/upload/v1725662135/dqsw7mavp77po9xwjjgw.jpg",
                Password = password,
                Telefono = telefono,
            };

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        private async Task CheckCategoriasAsync()
        {
            if (!_context.Categorias.Any())
            {
                _context.Categorias.Add(new Categoria { Nombre = "Tecnologia" });
                _context.Categorias.Add(new Categoria { Nombre = "Deportes" });
                _context.Categorias.Add(new Categoria { Nombre = "Hogar" });
                await _context.SaveChangesAsync();
            }
        }
    }
}