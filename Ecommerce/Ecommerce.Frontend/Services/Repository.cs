using System.Text;
using System.Text.Json;

namespace Ecommerce.Frontend.Services
{
    public class Repository(HttpClient httpClient) : IRepository
    {
        private readonly HttpClient _httpClient = httpClient;

        private static JsonSerializerOptions JsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<object> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, JsonDefaultOptions)!;
        }

        public async Task<T> GetByIdAsync<T>(string url, int id)
        {
            var requestUrl = $"{url}/{id}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, JsonDefaultOptions)!;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            return response;
        }
    }
}