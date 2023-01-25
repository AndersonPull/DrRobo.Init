using System;
using Drrobo.Modules.RemotelyControlled.Services;
using Refit;

namespace Drrobo.Utils.Constant
{
    public partial struct Constants
    {
        public static readonly IRemotelyControlledService BaseServeLocal = RestService.For<IRemotelyControlledService>("web-service-gerado-pelo-esp32");
    }
}