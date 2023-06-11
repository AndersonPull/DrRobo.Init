using CoreGraphics;
using Drrobo.Modules.Shared.Components.Entry;
using Drrobo.Platforms.MacCatalyst.Renderers;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using UIKit;

[assembly: ExportRenderer(typeof(EntryComponent), typeof(EntryRendererMac))]
namespace Drrobo.Platforms.MacCatalyst.Renderers
{
    public class EntryRendererMac : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //UIControl.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.BorderStyle = UITextBorderStyle.None;
                Element.HeightRequest = 30;
            }
        }
    }
}