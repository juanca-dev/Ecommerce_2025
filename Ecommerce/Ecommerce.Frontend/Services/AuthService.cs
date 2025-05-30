using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.Frontend.Services
{
    public class AuthService(AuthenticationStateProvider authenticationStateProvider)
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

        public async Task<string?> GetUserNameAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("unique_name")?.Value;
        }

        public async Task<string?> GetUserPhotoAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("Foto")?.Value;
        }

        public async Task<string?> GetUserRoleAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity!.IsAuthenticated;
        }

        public async Task<string?> GetNameAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("Nombre")?.Value;
        }
    }
}