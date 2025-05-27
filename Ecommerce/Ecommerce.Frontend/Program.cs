using Ecommerce.Frontend.Helpers;
using Ecommerce.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace Ecommerce.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices();
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<UploadFiles>();
            var url = "https://localhost:7033";
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });

            await builder.Build().RunAsync();
        }
    }
}