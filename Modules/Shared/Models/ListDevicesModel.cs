using System;
using System.Collections.ObjectModel;

namespace Drrobo.Modules.Shared.Models
{
	public class ListDevicesModel : BaseModel
    {
        public ObservableCollection<DevicesModel> DevicesList { get; set; }
    }
}