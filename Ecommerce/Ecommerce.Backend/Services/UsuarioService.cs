using Ecommerce.Backend.Repositories;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Models;

namespace Ecommerce.Backend.Services
{
    public class UsuarioService(IUsuarioRepository repository, IFilesService service)
    {
        private readonly IUsuarioRepository _repository = repository;
        private readonly IFilesService _service = service;

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            if (await _repository.ExisteCorreoAsync(usuario.Correo))
            {
                throw new Exception($"El correo {usuario.Correo} ya está en uso.");
            }

            if (usuario.URLFoto != null)
            {
                usuario.URLFoto = await _service.UploadImage(usuario.URLFoto);
            }

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            await _repository.AddAsync(usuario);
            return usuario;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            var usuarioExistente = await _repository.GetByIdAsync(usuario.Id) ?? throw new Exception($"El usuario con id {usuario.Id} no fue encontrado.");
            if (string.IsNullOrEmpty(usuario.URLFoto))
            {
                usuario.URLFoto = usuarioExistente.URLFoto;
            }
            else if (usuario.URLFoto != usuarioExistente.URLFoto)
            {
                usuario.URLFoto = await _service.UploadImage(usuario.URLFoto);
            }

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            await _repository.UpdateAsync(usuario);
            return usuario;
        }

        public async Task<Usuario> ActiveUser(Usuario usuario)
        {
            await _repository.ActiveUser(usuario);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioAsync(string correo)
        {
            var usuario = await _repository.GetByEmailAsync(correo) ?? throw new Exception($"Usuario con correo {correo} no fue encontrado.");
            return usuario;
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO model)
        {
            var usuario = await _repository.GetByEmailAsync(model.Email);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, usuario.Password))
            {
                throw new UnauthorizedAccessException("La contraseña actual es incorrecta.");
            }

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            await _repository.UpdateAsync(usuario);
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO model)
        {
            var usuario = await _repository.GetByEmailAsync(model.Email) ?? throw new Exception($"Usuario con correo {model.Email} no fue encontrado.");
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            await _repository.ResetPasswordAsync(usuario);
        }
    }
}