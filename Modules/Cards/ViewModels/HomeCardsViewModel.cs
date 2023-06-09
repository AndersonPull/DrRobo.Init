using System;
using System.Text;
using System.Windows.Input;
using Drrobo.Modules.Cards.Models;
using Drrobo.Modules.Shared.Services.Device;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Cards.ViewModels
{
	public class HomeCardsViewModel : BaseViewModel<HomeCardsModel>
    {
        public ICommand ReadCardCommand => new Command(async () => await ReadCardAsync());

        INfcService _nfcService;
        public HomeCardsViewModel(INfcService nfcService)
		{
            _nfcService = nfcService;
        }

        private async Task ReadCardAsync()
        {
            if (await _nfcService.OpenNFCSettingsAsync())
            {
                _nfcService.ConfigureNfcAdapter();
                _nfcService.EnableForegroundDispatch();
            }
            Model.StringData = string.Empty;
            byte[] bytes = Encoding.ASCII.GetBytes(Model.StringData);
            await _nfcService.SendAsync(bytes);
        }
    }
}

