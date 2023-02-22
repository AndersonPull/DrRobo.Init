using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace Drrobo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCompatibility()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .ConfigureMauiHandlers((handlers) =>
			{
				#if MACCATALYST
					handlers.AddCompatibilityRenderer(typeof(Drrobo.Modules.Shared.Components.Entry.EntryComponent), typeof(Drrobo.Platforms.MacCatalyst.Renderers.EntryRendererMac));
				#endif

				#if IOS
					handlers.AddCompatibilityRenderer(typeof(Drrobo.Modules.Shared.Components.Entry.EntryComponent), typeof(Drrobo.Platforms.iOS.Renderers.EntryRendererIOS));
				#endif

				#if ANDROID
					handlers.AddCompatibilityRenderer(typeof(Drrobo.Modules.Shared.Components.Entry.EntryComponent), typeof(Drrobo.Platforms.Android.Renderers.EntryRendererAndroid));
				#endif
			});

		#if DEBUG
				builder.Logging.AddDebug();
		#endif

        return builder.Build();
	}
}