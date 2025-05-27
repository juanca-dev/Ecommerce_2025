using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Ecommerce.Backend.Services
{
    public class FilesService(Cloudinary cloudinary) : IFilesService
    {
        private readonly Cloudinary _cloudinary = cloudinary;

        public async Task<string> UploadImage(string imageBase64)
        {
            if (!string.IsNullOrEmpty(imageBase64))
            {
                var base64Data = imageBase64[(imageBase64.IndexOf(',') + 1)..];
                var imageBytes = Convert.FromBase64String(base64Data);

                using var stream = new MemoryStream(imageBytes);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription("foto.jpg", stream),
                    AssetFolder = "tecnologers"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception("Error al cargar la imagen: " + uploadResult.Error.Message);
                }

                var urlFoto = uploadResult.SecureUrl.ToString();
                return urlFoto;
            }
            return "Ocurrió un error al cargar la imagen";
        }
    }
}