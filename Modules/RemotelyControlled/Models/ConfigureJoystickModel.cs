using System;
using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class ConfigureJoystickModel : BaseModel
	{
        public ObservableCollection<ServerModel> ServerList { get; set; }
        public ServerModel Server { get; set; }
		public CommunicationEnum Communication { get; set;}
        public List<string> ServersNames { get; set; }
    }
}