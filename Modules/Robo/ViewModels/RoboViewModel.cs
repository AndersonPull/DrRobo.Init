using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using Drrobo.Modules.Robo.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;
using System.Globalization;
using System.Windows.Input;

namespace Drrobo.Modules.Robo.ViewModels
{
    class RoboViewModel : BaseViewModel<RoboModel>
    {
        public ICommand ListenCommand => new Command(async () => await ListenAsync());

        INavigationService _serviceNavigation;
        private readonly ISpeechToText _speechToText;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        public RoboViewModel(INavigationService serviceNavigation, ISpeechToText speechToText)
        {
            _serviceNavigation = serviceNavigation;
            _speechToText = speechToText;
        }

        private async Task ListenAsync()
        {
            var isGranted = await _speechToText.RequestPermissions(_tokenSource.Token);
            if (!isGranted)
            {
                await Toast.Make("Permission not granted").Show(CancellationToken.None);
                return;
            }
            var recognitionResult = await _speechToText.ListenAsync(
                                                CultureInfo.GetCultureInfo("pt-br"),
                                                new Progress<string>(partialText =>
                                                {
                                                    Model.RecognitionText += partialText + " ";
                                                }), _tokenSource.Token);

            if (recognitionResult.IsSuccessful)
            {
                Model.RecognitionText = recognitionResult.Text;
            }
            else
            {
                await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
                await Application.Current.MainPage.DisplayAlert("Error", recognitionResult.Exception?.Message, "OK");
            }
        }
    }
}