using System;
using System.Collections.ObjectModel;

namespace Drrobo.Modules.Shared.Models
{
	public class ConfigureDevicesModel : BaseModel
    {
        public DevicesModel Device { get; set; }

        public bool IsUpdate{ get; set; }
    }
}