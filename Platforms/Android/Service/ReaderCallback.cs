using System;
using Android.Nfc;

namespace Drrobo.Platforms.Android.Service
{
    public class ReaderCallback : Java.Lang.Object, NfcAdapter.IReaderCallback
    {
        public TaskCompletionSource<Tag> NFCTag { get; set; }

        public void OnTagDiscovered(Tag tag)
        {
            var isSuccess = NFCTag?.TrySetResult(tag);
            if (null == NFCTag || !isSuccess.Value)
                NfcService.DetectedTag = tag;
        }
    }
}

