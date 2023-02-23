using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;

namespace Drrobo.Utils.Bluetooth.Implementations
{
	public class BluetoothUtil : IBluetoothUtil
    {
        IAdapter _adapter;
        IBluetoothLE _bluetooth;

        public BluetoothUtil()
        {
            _bluetooth = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<ObservableCollection<IDevice>> SearchDevicesAsync()
        {
            var devices = new ObservableCollection<IDevice>();

            if (_bluetooth.State == BluetoothState.Off)
                await App.Current.MainPage.DisplayAlert("AVISO", "Bluetooth desabilitado.", "OK");
            else
            {
                _adapter.ScanTimeout = 1000;
                _adapter.ScanMode = ScanMode.Balanced;
                _adapter.DeviceDiscovered += (obj, a) =>
                {
                    if (!devices.Contains(a.Device))
                        if (!string.IsNullOrEmpty(a.Device.Name))
                            devices.Add(a.Device);
                };

                await _adapter.StartScanningForDevicesAsync();
            }

            return devices;
        }

        public async Task<IDevice> SelectDeviceAsync(IDevice device)
        {
            var result = await App.Current.MainPage
               .DisplayAlert("AVISO", $"Deseja se conectar com {device.Name}", "Conectar", "Cancelar");

            if (!result)
                return null;

            await _adapter.StopScanningForDevicesAsync();

            try
            {
                await _adapter.ConnectToDeviceAsync(device);
                return device;
            }
            catch (DeviceConnectionException ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return null;
            }
        }

        public async Task<bool> SendAsync(IDevice device, string value)
        {
            if (device == null || device.State != DeviceState.Connected)
                return false;

            var services = await device.GetServicesAsync();
            var service = services.LastOrDefault();

            var characteristics = await service.GetCharacteristicsAsync();
            var characteristic = characteristics.FirstOrDefault();

            if (characteristic.CanWrite)
                await characteristic.WriteAsync(Encoding.ASCII.GetBytes(value));

            return true;
        }
    }
}