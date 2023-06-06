using System.Collections.ObjectModel;
using Drrobo.Modules.Dashboard.Enums;
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
                new ItemMenuModel(){ Title="Remotely Controlled", Description="", ImageIcon = "joystick_banner_icon.png", ImageBack="", Type = CarouselItems.RemotelyControlled},
                new ItemMenuModel(){ Title="Cards", Description="", ImageIcon = "cards_banner_icon.png", ImageBack="", Type = CarouselItems.Cards}
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