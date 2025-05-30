using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Models
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña actual es requerida.")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es requerida.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes confirmar la nueva contraseña.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}