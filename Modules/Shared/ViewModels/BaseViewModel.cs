using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Services.Service;
using Drrobo.Utils.Bluetooth;
using Drrobo.Utils.Constant;
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

        public BaseViewModel()
        {
        }

        private T _model = Activator.CreateInstance<T>();
        public T Model { get { return _model; } set { Set("Model", ref _model, value); } }

        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } set { Set("IsBusy", ref _isBusy, value); } }


        public async Task Communication(CommunicationEnum communication, string request, IUniversalService universalService = null, IBluetoothUtil bluetoothUtil = null, IDevice device = null)
        {
            try
            {
                switch (communication)
                {
                    case CommunicationEnum.Wifi:
                        universalService.Request(request).GetAwaiter().GetResult();
                        break;
                    case CommunicationEnum.Bluetooth:
                        bluetoothUtil.SendAsync(device, request).GetAwaiter().GetResult();
                        break;
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Erro na comunicação", "Certifique que as configurações estão corretas", "OK");
            }
        }
    }
}