using System;
using Drrobo.Modules.Shared.Enums;
using SQLite;

namespace Drrobo.Modules.Shared.Models
{
	public class DevicesModel : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public bool Isjoystick { get; set; }

        public bool IsBluetooth { get; set; }

        public bool HaveCamera { get; set; }

        public string URLCamera { get; set; }

        public string Image { get; set; }

        public string Type { get; set; }

        public bool IsSelected { get; set; }
    }
}