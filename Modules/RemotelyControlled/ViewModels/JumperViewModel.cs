﻿using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System.Collections.ObjectModel;
using Drrobo.Modules.RemotelyControlled.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using System.Windows.Input;
using Mopups.Services;
using System.Text;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
    public class JumperViewModel : BaseViewModel<JumperModel>
    {
        public ICommand BluetoothPopupCommand => new Command(async () => await BluetoothPopupAsync());
        public ICommand MovementJumperCommand => new Command(async (value) => await MovementJumperAsync((string)value));

        IAdapter _adapter;
        IBluetoothLE _bluetooth;

        INavigationService _serviceNavigation;
        public JumperViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
            _bluetooth = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;

            MessagingCenter.Subscribe<IDevice>(this, "DeviceSelectedBluetooth", async (selectedDevice)
                => await DeviceSelected(selectedDevice));
        }

        private async Task BluetoothPopupAsync()
        {
            await SearchDevicesAsync();

            await MopupService.Instance.PushAsync(new Components.Popup.BluetoothPopup(Model.Devices));
        }

        public async Task SearchDevicesAsync()
        {
            if (_bluetooth.State == BluetoothState.Off)
                await App.Current.MainPage.DisplayAlert("AVISO", "Bluetooth desabilitado.", "OK");
            else
            {
                Model.Devices = new ObservableCollection<IDevice>();

                _adapter.ScanTimeout = 1000;
                _adapter.ScanMode = ScanMode.Balanced;
                _adapter.DeviceDiscovered += (obj, a) =>
                {
                    if (!Model.Devices.Contains(a.Device))
                        if (!string.IsNullOrEmpty(a.Device.Name))
                            Model.Devices.Add(a.Device);
                };

                await _adapter.StartScanningForDevicesAsync();
            }
        }

        public async Task DeviceSelected(IDevice device)
        {
            Model.ConnectedDevice = device;

            var result = await App.Current.MainPage
                .DisplayAlert("AVISO", $"Deseja se conectar com {Model.ConnectedDevice.Name}", "Conectar", "Cancelar");

            if (!result)
                return;

            await _adapter.StopScanningForDevicesAsync();

            try
            {
                await _adapter.ConnectToDeviceAsync(Model.ConnectedDevice);
                MessagingCenter.Send<string>("true", "BluetoothConnected");
            }
            catch (DeviceConnectionException ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public async Task MovementJumperAsync(string value)
        {
            if (Model.ConnectedDevice == null ||
                Model.ConnectedDevice.State != DeviceState.Connected)
            {
                MessagingCenter.Send<string>("false", "BluetoothConnected");
                return;
            }

            var services = await Model.ConnectedDevice.GetServicesAsync();
            var service = services.LastOrDefault();

            var characteristics = await service.GetCharacteristicsAsync();
            var characteristic = characteristics.FirstOrDefault();

            if (characteristic.CanWrite)
                await characteristic.WriteAsync(Encoding.ASCII.GetBytes(value));
        }
    }
}