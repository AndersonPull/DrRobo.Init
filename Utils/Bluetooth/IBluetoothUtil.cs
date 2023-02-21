using System;
using System.Collections.ObjectModel;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Utils.Bluetooth
{
	public interface IBluetoothUtil
	{
        Task<ObservableCollection<IDevice>> SearchDevicesAsync();

        Task<bool> SelectDeviceAsync(IDevice device);

        Task SendAsync(IDevice device, string value);
    }
}

