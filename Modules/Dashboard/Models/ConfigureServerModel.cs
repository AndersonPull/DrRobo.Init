using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Dashboard.Models
{
	public class ConfigureServerModel : BaseModel
	{
        public ObservableCollection<ServerModel> ServerList { get; set; }
	}
}