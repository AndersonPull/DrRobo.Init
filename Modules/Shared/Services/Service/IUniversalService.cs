using Refit;

namespace Drrobo.Modules.Shared.Services.Service
{
	public interface IUniversalService
	{
        [Post("")]
        Task<string> Request(string request);

        [Post("drrobo")]
        Task<string> RequestDrRobo(string request);
    }
}