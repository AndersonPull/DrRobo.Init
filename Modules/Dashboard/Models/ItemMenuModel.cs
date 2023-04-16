using System;
using Drrobo.Modules.Dashboard.Enums;

namespace Drrobo.Modules.Dashboard.Models
{
    public class ItemMenuModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageIcon { get; set; }
        public string ImageBack { get; set; }
        public CarouselItems Type { get; set; }
    }
}