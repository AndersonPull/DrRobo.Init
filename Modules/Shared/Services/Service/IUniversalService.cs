using Refit;

namespace Drrobo.Modules.Shared.Services.Service
{
	public interface IUniversalService
	{
        [Post("{url}")] 
        Task<string> Request(string url, string request);
    }
}