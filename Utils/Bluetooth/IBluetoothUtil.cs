using System;
using System.Collections.ObjectModel;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Utils.Bluetooth
{
	public interface IBluetoothUtil
	{
        Task<ObservableCollection<IDevice>> SearchDevicesAsync();

        Task<IDevice> SelectDeviceAsync(IDevice device);

        Task<bool> SendAsync(IDevice device, string value);

        Task<IDevice> ConnectDeviceAsync(Guid guid);
    }
}

