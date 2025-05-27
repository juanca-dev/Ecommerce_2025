namespace Ecommerce.Backend.Services
{
    public interface IFilesService
    {
        Task<string> UploadImage(string imageBase64);
    }
}