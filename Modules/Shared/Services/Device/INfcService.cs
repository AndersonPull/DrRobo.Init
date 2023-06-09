using System;
namespace Drrobo.Modules.Shared.Services.Device
{
	public interface INfcService
	{
        public void ConfigureNfcAdapter(){}
        public void EnableForegroundDispatch(){}
        public void DisableForegroundDispatch(){}
        public void UnconfigureNfcAdapter(){}
        public Task SendAsync(byte[] bytes);
        public Task<bool> OpenNFCSettingsAsync() => Task.FromResult(true);
    }
}