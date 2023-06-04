using SQLite;

using Drrobo.Modules.Dashboard.Enums;
using System.Globalization;

namespace Drrobo.Modules.Dashboard.Models
{
	public class LanguageModel
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public LanguagesEnum Name { get; set; }
        public string CultureInfo { get; set; }
    }
}