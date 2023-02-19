using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Models;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class JumperModel : BaseModel
	{
        public ObservableCollection<IDevice> Devices { get; set; }
        public IDevice ConnectedDevice { get; set; }
        public bool BluetoothConnected { get; set; }
    }
}