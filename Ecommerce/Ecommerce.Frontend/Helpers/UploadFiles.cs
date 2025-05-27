using Microsoft.AspNetCore.Components.Forms;

namespace Ecommerce.Frontend.Helpers
{
    public class UploadFiles
    {
        public static async Task<string> UploadImagesAsync(IBrowserFile file)
        {
            if (file == null || file.Size == 0)
            {
                return "Archivo inválido o vacío";
            }

            if (file.Size > 5 * 1024 * 1024)
            {
                return "El archivo es demasiado grande. El tamaño máximo permitido es 5 MB.";
            }

            if (!file.ContentType.StartsWith("image/"))
            {
                return "Solo se permiten archivos de imagen.";
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                var base64String = Convert.ToBase64String(memoryStream.ToArray());
                var imageUrl = $"data:{file.ContentType};base64,{base64String}";
                return imageUrl;
            }
            catch (Exception ex)
            {
                return $"Error al cargar el archivo: {ex.Message}";
            }
        }
    }
}