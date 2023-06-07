using SQLite;

namespace Drrobo.Modules.Shared.Models
{
	public class ServerModel : BaseModel
	{
        [PrimaryKey, AutoIncrement]
		public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public bool Connectedjoystick { get; set; }

        public bool IsBluetooth { get; set; }
    }
}