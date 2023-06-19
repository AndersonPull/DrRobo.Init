using System;
using System.Collections.ObjectModel;

namespace Drrobo.Modules.Shared.Models
{
	public class ConfigureDevicesModel : BaseModel
    {
        public ObservableCollection<DevicesModel> DevicesList { get; set; }
    }
}