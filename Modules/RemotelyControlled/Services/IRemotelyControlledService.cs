using System;
using Drrobo.Modules.Shared.Services.Dtos.Response;
using Refit;

namespace Drrobo.Modules.RemotelyControlled.Services
{
	public interface IRemotelyControlledService
	{
        [Get("/up")]
        Task Up();

        [Get("/down")]
        Task Down();

        [Get("/front")]
        Task Front();

        [Get("/back")]
        Task Back();

        [Get("/left")]
        Task Left();

        [Get("/right")]
        Task Right();

        [Get("/stop")]
        Task Stop();
    }
}