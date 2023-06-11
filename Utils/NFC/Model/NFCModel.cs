using System;
using Drrobo.Modules.Shared.Models;
using Plugin.NFC;

namespace Drrobo.Utils.NFC.Model
{
	public class NFCModel : BaseModel
	{
        public string ALERT_TITLE { get; set; } = "NFC";
        public string MIME_TYPE { get; set; } = "application/com.companyname.nfcsample";
        public bool NfcIsEnabled { get; set; }
        public bool IsDeviceiOS { get; set; }
        public bool DeviceIsListening { get; set; }
        public bool EventsAlreadySubscribed { get; set; }
        public NFCNdefTypeFormat Type { get; set; }
        public bool MakeReadOnly { get; set; }
        public object ChkReadOnly { get; set; }
    }
}