using System;
using Drrobo.Modules.Dashboard.Enums;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Dashboard.Models
{
	public class ProfileModel : BaseModel
    {
		public LanguagesEnum Language { get; set; }
	}
}