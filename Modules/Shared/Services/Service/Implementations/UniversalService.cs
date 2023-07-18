using System.Text;
using Drrobo.Modules.Shared.Services.Service.Response;
using Newtonsoft.Json;

namespace Drrobo.Modules.Shared.Services.Service.Implementations
{
	public class UniversalService : IUniversalService
    {
        private readonly HttpClient _httpClient;
        public UniversalService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HealthCheckResponse> HealthCheckAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return JsonConvert
                    .DeserializeObject<HealthCheckResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro na requisição HTTP: {e.Message}");
                return null;
            }
        }

        public async Task RequestAsync(string url, string request)
        {
            try
            {
                var content = new StringContent(request, Encoding.UTF8, "text/plain");
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro na requisição HTTP: {e.Message}");
            }
        }
    }
}

