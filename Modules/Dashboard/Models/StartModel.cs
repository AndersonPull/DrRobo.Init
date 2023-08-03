using System.Collections.ObjectModel;
using Drrobo.Modules.Dashboard.Enums;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Dashboard.Models
{
	public class StartModel : BaseModel
	{
		public StartModel()
		{
            ListMenu = new List<ItemMenuModel>()
            {
                new ItemMenuModel(){ Title="Remotely Controlled", Description="", ImageIcon = "joystick_banner_icon.png", ImageBack="", Type = CarouselItems.RemotelyControlled},
                new ItemMenuModel(){ Title="Remotely Controlled", Description="", ImageIcon = "cam_banner_icon.png", ImageBack="", Type = CarouselItems.Camera}
            };

            CommandsList = new ObservableCollection<string>();
            Bluetooth = new BluetoothModel();
            Profile = new ProfileModel();
        }

        public List<ItemMenuModel> ListMenu { get; set; }
        public ObservableCollection<string> CommandsList { get; set; } 
        public string CommandText { get; set; }
        public bool OpenCam { get; set; }
        public BluetoothModel Bluetooth { get; set; }
        public ProfileModel Profile { get; set; }
        public ObservableCollection<DevicesModel> DevicesList { get; set; }
        public bool DevicesOn { get; set; }
        public string DeviceConnectedLabel { get; set; } = $"{DeviceInfo.Name} ~ %";
        public DevicesModel DeviceConnected { get; set; }
    }
}