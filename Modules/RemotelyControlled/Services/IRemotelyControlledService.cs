using System;
using Drrobo.Modules.Shared.Services.Dtos.Response;
using Refit;

namespace Drrobo.Modules.RemotelyControlled.Services
{
	public interface IRemotelyControlledService
	{
        [Post("/Movement")]
        Task<string> Movement(string request);
    }
}