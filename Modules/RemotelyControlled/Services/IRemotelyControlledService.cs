using System;
using Refit;

namespace Drrobo.Modules.RemotelyControlled.Services
{
	public interface IRemotelyControlledService
	{
        [Post("/Movement")]
        Task<string> Movement(string request);
    }
}