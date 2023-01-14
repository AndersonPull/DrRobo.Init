using Drrobo.Modules.RemotelyControlled.Enums;

namespace Drrobo.Modules.Dashboard.Models
{
	public class StartModel
	{
		public StartModel()
		{
            ListMenu = new List<ItemMenuModel>()
            {
                new ItemMenuModel(){ Title="Drone", Description="", ImageIcon = "drone_icon.png", ImageBack="", Type = RemotelyControlledTypeEnum.Drone},
                new ItemMenuModel(){ Title="Jumper", Description="", ImageIcon = "jumper_icon.png", ImageBack="", Type = RemotelyControlledTypeEnum.Jumper},
                new ItemMenuModel(){ Title="Spider", Description="", ImageIcon = "spider_icon.png", ImageBack="", Type = RemotelyControlledTypeEnum.Spider}
            };
        }

        public List<ItemMenuModel> ListMenu { get; set; }
    }
}