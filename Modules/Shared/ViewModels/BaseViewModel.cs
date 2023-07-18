using Drrobo.Modules.Shared.Services.Service;
using Drrobo.Utils.Bluetooth;
using Drrobo.Utils.Translations;
using GalaSoft.MvvmLight;
using Plugin.BLE.Abstractions.Contracts;
using PropertyChanged;

namespace Drrobo.Modules.Shared.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public  BaseViewModel(){}

        public virtual Task InitializeAsync(object navigationData)
            => Task.FromResult(false);
    }
    
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel<T> : BaseViewModel
    {
        public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;


        public BaseViewModel(){}

        private T _model = Activator.CreateInstance<T>();
        public T Model { get { return _model; } set { Set("Model", ref _model, value); } }

        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } set { Set("IsBusy", ref _isBusy, value); } }


        public async Task CommunicationBLE(string message, IDevice device, IBluetoothUtil bluetoothUtil)
        {
            try
            {
                await bluetoothUtil.SendAsync(device, message);
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Erro na comunicação", e.Message, "OK");
            }
        }

        public async Task CommunicationWifi(string message, string url, IUniversalService universalService)
        {
            try
            {
                universalService.RequestAsync(url, message).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Erro na comunicação", e.Message, "OK");
            }
        }
    }
}