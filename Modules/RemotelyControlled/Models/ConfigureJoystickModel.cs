using System;
using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class ConfigureJoystickModel : BaseModel
	{
        public ObservableCollection<DevicesModel> ServerList { get; set; }
        public DevicesModel Server { get; set; }
		public CommunicationEnum Communication { get; set;}
        public List<string> ServersNames { get; set; }
    }
}