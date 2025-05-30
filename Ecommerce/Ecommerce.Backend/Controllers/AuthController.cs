using Ecommerce.Backend.Data;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(DataContext context, IConfiguration config) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly string secretKey = config.GetSection("Jwt").GetValue<string>("key")!;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            //validamos el loginDTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //buscamos el usuario en la bd
            var usuario = await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Correo == login.Email);

            // si el usuario existe y esta activo
            if (usuario != null && usuario.Activo)
            {
                //valida el estado del usuario
                if (!usuario.Activo)
                {
                    return Unauthorized(new { Message = "El usuario se encuentra inhabilitado, por favor comuniquese con el administrador", isSuccess = false, token = "" });
                }

                //generacion de token
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.Name, login.Email));
                claims.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil));
                claims.AddClaim(new Claim("Nombre", usuario.Nombre));
                claims.AddClaim(new Claim("Foto", usuario.URLFoto ?? "default.jpg"));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMonths(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                if (BCrypt.Net.BCrypt.Verify(login.Password, usuario.Password))
                {
                    return Ok(new { Message = "Inicio de sesión exitoso.", isSuccess = true, token = tokencreado });
                }
            }

            return Unauthorized(new { Message = "Inicio de sesión fallido. Usuario o contraseña incorrectos.", isSuccess = false, token = "" });
        }
    }
}