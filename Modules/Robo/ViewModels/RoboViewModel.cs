using CommunityToolkit.Maui.Media;
using Drrobo.Modules.Robo.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;
using System.Globalization;

namespace Drrobo.Modules.Robo.ViewModels
{
    class RoboViewModel : BaseViewModel<RoboModel>
    {
        INavigationService _serviceNavigation;
        private readonly ISpeechToText _speechToText;
        public RoboViewModel(INavigationService serviceNavigation, ISpeechToText speechToText)
        {
            _serviceNavigation = serviceNavigation;
            _speechToText = speechToText;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await ListenAsync();
            await base.InitializeAsync(navigationData);
        }

        private async Task ListenAsync()
        {
            var tokenSource = new CancellationTokenSource();
            if (!await _speechToText.RequestPermissions(tokenSource.Token))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Permission not granted", "OK");
                return;
            }

            var recognitionResult = await _speechToText
                .ListenAsync(
                    CultureInfo.GetCultureInfo("pt-br"),
                    new Progress<string>(async partialText => await ActionAsync(partialText)),
                    tokenSource.Token);

            if (!recognitionResult.IsSuccessful)
                await Application.Current.MainPage.DisplayAlert("Error", recognitionResult.Exception?.Message, "OK");
        }

        public async Task ActionAsync(string value)
        {
            if (value.Contains("horas"))
                Model.RecognitionText = $"São {DateTime.Now.ToString("HH:mm")}";
            else
                Model.RecognitionText = $"Desculpe, não entendi o que você falou";

            await TextToSpeech.Default.SpeakAsync(Model.RecognitionText);
        }
    }
}