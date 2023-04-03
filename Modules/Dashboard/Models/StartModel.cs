using System.Collections.ObjectModel;
using Drrobo.Modules.RemotelyControlled.Enums;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Dashboard.Models
{
	public class StartModel : BaseModel
	{
		public StartModel()
		{
            ListMenu = new List<ItemMenuModel>()
            {
                new ItemMenuModel(){ Title="Drone", Description="", ImageIcon = "drone_icon.png", ImageBack="", Type = RemotelyControlledTypeEnum.Drone},
                new ItemMenuModel(){ Title="Jumper", Description="", ImageIcon = "jumper_icon.png", ImageBack="", Type = RemotelyControlledTypeEnum.Jumper},
                new ItemMenuModel(){ Title="Spider", Description="", ImageIcon = "spider_icon.png", ImageBack="", Type = RemotelyControlledTypeEnum.Spider}
            };

            CommandsList = new ObservableCollection<string>();
            Bluetooth = new BluetoothModel();
            Profile = new ProfileModel();
        }

        public List<ItemMenuModel> ListMenu { get; set; }
        public ObservableCollection<string> CommandsList { get; set; } 
        public string CommandText { get; set; }
        public bool DroneCamOn { get; set; }
        public bool JumperCamOn { get; set; }
        public BluetoothModel Bluetooth { get; set; }
        public ProfileModel Profile { get; set; } 
    }
}