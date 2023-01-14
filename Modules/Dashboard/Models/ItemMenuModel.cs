using System;
using Drrobo.Modules.RemotelyControlled.Enums;

namespace Drrobo.Modules.Dashboard.Models
{
    public class ItemMenuModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageIcon { get; set; }
        public string ImageBack { get; set; }
        public RemotelyControlledTypeEnum Type { get; set; }
    }
}