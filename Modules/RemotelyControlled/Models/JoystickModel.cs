using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class JoystickModel : BaseModel
    {
        public DevicesModel Device { get; set; }
        public BluetoothModel Bluetooth { get; set; } = new BluetoothModel();
        public ObservableCollection<DevicesModel> DevicesList { get; set; }
        public bool WebViewCam { get; set; }
        public bool HaveCam { get; set; }
    }
}