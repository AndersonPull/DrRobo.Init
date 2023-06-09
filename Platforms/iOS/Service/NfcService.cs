using System;
using CoreNFC;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Services.Device;
using UIKit;

namespace Drrobo.Platforms.iOS.Service
{
    public class NfcService : INfcService
    {
        public async Task SendAsync(byte[] bytes)
        {
            var isNfcAvailable = UIDevice.CurrentDevice.CheckSystemVersion(11, 0);
            if (isNfcAvailable && NFCNdefReaderSession.ReadingAvailable)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        var sessionDelegate = new SessionDelegate(bytes);
                        var session = new NFCNdefReaderSession(sessionDelegate, null, true);
                        session.BeginSession();
                        var status = await sessionDelegate.WasDataTransmitted.Task;
                        if (status != NfcTransmissionStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Suitable error message", "Ok");
                        }
                    }
                    catch
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "There was an error while trying to create a NFC session", "Ok");
                    }
                });
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Read is not supported by this tag", "Ok");
            }
        }
    }
}