using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Drrobo.Modules.Shared.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
		public bool IsBusy { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}