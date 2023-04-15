using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.RemotelyControlled.Models
{
	public class JoystickModel : BaseModel
    {
        public BluetoothModel Bluetooth { get; set; } = new BluetoothModel();
    }
}