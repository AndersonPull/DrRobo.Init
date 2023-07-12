using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Shared.Views;

public partial class ListDevicesView : ContentPage
{
    private ListDevicesViewModel ViewModel => (ListDevicesViewModel)BindingContext;

    public ListDevicesView()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel.GetDevicesCommand.CanExecute(null))
            ViewModel.GetDevicesCommand.Execute(null);
    }
}