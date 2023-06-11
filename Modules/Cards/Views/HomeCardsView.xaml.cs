using Drrobo.Modules.Cards.ViewModels;

namespace Drrobo.Modules.Cards.Views;

public partial class HomeCardsView : ContentPage
{
    private HomeCardsViewModel ViewModel => (HomeCardsViewModel)BindingContext;

    public HomeCardsView()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.ExecuteOnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        ViewModel.ExecuteOnDisappearing();
    }
}
