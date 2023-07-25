using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class CameraMonitoringModel : BaseModel
	{
        public ObservableCollection<DevicesModel> DevicesList { get; set; }

        public int Position { get; set; }
    }
}