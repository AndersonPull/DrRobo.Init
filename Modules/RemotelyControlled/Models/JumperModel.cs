using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Models;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class JumperModel : BaseModel
	{
        public BluetoothModel Bluetooth { get; set; } = new BluetoothModel();
    }
}