using System;
using System.Windows.Input;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Dashboard.Enums;
using Drrobo.Modules.Dashboard.Components.Content;
using Drrobo.Modules.Dashboard.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Dashboard.ViewModels
{
    public class StartViewModel : BaseViewModel<BaseModel>
    {
        private List<CarouselButtonModel> _carouselButton;
        public List<CarouselButtonModel> CarouselButton { get { return _carouselButton; } set { Set("CarouselButton", ref _carouselButton, value); } }

        public ICommand SetContentTypeCommand => new Command(async (value) => await SetContentTypeAsync((DashboardPageTypeEnum)value));
        public ICommand RemotelyControlledCommand => new Command(async () => await RemotelyControlledAsync());

        private Dictionary<DashboardPageTypeEnum, Lazy<ContentView>> ContentType =
            new Dictionary<DashboardPageTypeEnum, Lazy<ContentView>>
            {
                { DashboardPageTypeEnum.Cmd, new Lazy<ContentView>(()=> new CmdContent())},
                { DashboardPageTypeEnum.Dashboard, new Lazy<ContentView>(()=> new DashBoardContent())},
                { DashboardPageTypeEnum.Profile, new Lazy<ContentView>(()=> new ProfileContent())}
            };

        public DashboardPageTypeEnum CurrentContent { get; set; } = DashboardPageTypeEnum.Dashboard;

        INavigationService _serviceNavigation;

        public StartViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;

            CarouselButton = new List<CarouselButtonModel>()
            {
                new CarouselButtonModel(){ Title="Drone", ImageIcon = "drone_icon.png", ImageBack=""},
                new CarouselButtonModel(){ Title="Jumper", ImageIcon = "jumper_icon.png", ImageBack=""},
                new CarouselButtonModel(){ Title="Spider", ImageIcon = "spider_icon.png", ImageBack=""}
            };
        }

        private async Task SetContentTypeAsync(DashboardPageTypeEnum item)
        {
            IsBusy = true;
            var currentView = Application.Current.MainPage;
            if (currentView.Navigation != null)
                currentView = currentView.Navigation.NavigationStack.Last();

            var MyView = currentView as StartView;

            if (CurrentContent == item)
                return;

            CurrentContent = item;

            await MyView.SetContent(ContentType[CurrentContent].Value);
            IsBusy = false;
        }

        private async Task RemotelyControlledAsync()
        {
            await _serviceNavigation.NavigateToAsync<RemotelyControlled.ViewModels.JumperViewModel>();
        }
    }
}