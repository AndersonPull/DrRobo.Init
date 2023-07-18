using Drrobo.Modules.Shared.Services.Service.Response;

namespace Drrobo.Modules.Shared.Services.Service
{
	public interface IUniversalService
	{
        Task<HealthCheckResponse> HealthCheckAsync(string url, string request);

        Task RequestAsync(string url, string request);
    }
}