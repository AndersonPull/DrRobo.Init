﻿using System.Windows.Input;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Dashboard.Enums;
using Drrobo.Modules.Dashboard.Components.Content;
using Drrobo.Modules.Dashboard.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.RemotelyControlled.Enums;
using Drrobo.Modules.RemotelyControlled.ViewModels;
using System.Linq.Expressions;

namespace Drrobo.Modules.Dashboard.ViewModels
{
    public class StartViewModel : BaseViewModel<StartModel>
    {
        public ICommand SetContentTypeCommand => new Command(async (value) => await SetContentTypeAsync((DashboardPageTypeEnum)value));
        public ICommand RemotelyControlledCommand => new Command(async (value) => await RemotelyControlledAsync((RemotelyControlledTypeEnum)value));

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

        private async Task RemotelyControlledAsync(RemotelyControlledTypeEnum type)
        {
            switch (type)
            {
                case RemotelyControlledTypeEnum.Drone:
                    await _serviceNavigation.NavigateToAsync<DroneViewModel>();
                    break;
                case RemotelyControlledTypeEnum.Jumper:
                    await _serviceNavigation.NavigateToAsync<JumperViewModel>();
                    break;
                case RemotelyControlledTypeEnum.Spider:
                    await _serviceNavigation.NavigateToAsync<JumperViewModel>();
                    break;
            }
        }
    }
}