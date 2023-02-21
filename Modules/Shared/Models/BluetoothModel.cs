using System;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;

namespace Drrobo.Modules.Shared.Models
{
	public class BluetoothModel : BaseModel
	{
        public ObservableCollection<IDevice> Devices { get; set; }
        public IDevice ConnectedDevice { get; set; }
        public bool BluetoothConnected { get; set; }
    }
}