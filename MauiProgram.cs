using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Mopups.Hosting;

namespace Drrobo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMopups()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .UseMauiCompatibility()
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