using GalaSoft.MvvmLight;

namespace Drrobo.Modules.Shared.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } set { Set("IsBusy", ref _isBusy, value); } }
    }

    public class BaseViewModel<T> : BaseViewModel
    {
        private T _model = Activator.CreateInstance<T>();
        public T Model { get { return _model; } set { Set("Model", ref _model, value); } }
    }
}