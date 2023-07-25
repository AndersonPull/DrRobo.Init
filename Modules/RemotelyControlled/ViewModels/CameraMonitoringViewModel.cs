using System.Collections.ObjectModel;
using System.Windows.Input;
using Drrobo.Modules.RemotelyControlled.Models;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Services.Service;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Utils;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class CameraMonitoringViewModel : BaseViewModel<CameraMonitoringModel>
    {
        public ICommand GetDevicesCommand => new Command(async () => await GetDevicesAsync());
        public ICommand NextCommand => new Command(async () => await NextAsync());
        public ICommand BackCommand => new Command(async () => await BackAsync());

        IUniversalService _universalService;

        DevicesData _deviceData;
        public CameraMonitoringViewModel(IUniversalService universalService)
		{
            _universalService = universalService;

            _deviceData = new DevicesData();
        }

        private async Task GetDevicesAsync()
        {
            Model.DevicesList = new ObservableCollection<DevicesModel>();
            foreach (var item in _deviceData.GetAll())
                if (item.Type == DeviceTypeEnum.Camera.Description())
                    Model.DevicesList.Add(item);

            if (Model.DevicesList.Count > 0)
            {
                Model.DevicesList.FirstOrDefault().CurrentlyMonitoring = true;
                await AccessCamAsync();
            }
        }

        private async Task NextAsync()
        {
            if(Model.Position < Model.DevicesList.Count - 1)
            {
                Model.Position += 1;
                Model.DevicesList[Model.Position].CurrentlyMonitoring = true;
                Model.DevicesList[Model.Position -1].CurrentlyMonitoring = false;

                await AccessCamAsync(Model.Position);
            }
        }

        private async Task BackAsync()
        {
            if (Model.Position > 0)
            {
                Model.Position -= 1;
                Model.DevicesList[Model.Position].CurrentlyMonitoring = true;
                Model.DevicesList[Model.Position + 1].CurrentlyMonitoring = false;

                await AccessCamAsync(Model.Position);
            }
        }

        private async Task AccessCamAsync(int position = 0)
            => Model.DevicesList[position].CameraON = await _universalService
                .AccessCamAsync(Model.DevicesList[position].URLCamera);
    }
}