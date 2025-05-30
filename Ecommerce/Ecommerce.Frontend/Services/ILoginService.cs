namespace Ecommerce.Frontend.Services
{
    public interface ILoginService
    {
        Task Login(string token);

        Task Logout();
    }
}