using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Dashboard.Models
{
	public class ConfigureServerModel : BaseModel
	{
        public ConfigureServerModel()
        {
            ServerList = new List<ServerModel>();
        }

        public List<ServerModel> ServerList { get; set; }
	}
}