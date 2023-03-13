using System;
using Refit;

namespace Drrobo.Modules.RemotelyControlled.Services
{
	public interface IRemotelyControlledService
	{
        [Post("/movement")]
        Task<string> Movement(string request);
    }
}