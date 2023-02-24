using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;

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
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureMauiHandlers((handlers) => { NewHandlers(handlers); });

		#if DEBUG
			builder.Logging.AddDebug();
		#endif

		return builder.Build();
	}

	private static void NewHandlers(IMauiHandlersCollection handlers)
	{
		#if MACCATALYST
			handlers.AddCompatibilityRenderer(typeof(Drrobo.Modules.Shared.Components.Entry.EntryComponent), typeof(Platforms.MacCatalyst.Renderers.EntryRendererMac));
		#endif

		#if IOS
			handlers.AddCompatibilityRenderer(typeof(Drrobo.Modules.Shared.Components.Entry.EntryComponent), typeof(Platforms.iOS.Renderers.EntryRendererIOS));
		#endif

		#if ANDROID
			handlers.AddCompatibilityRenderer(typeof(Drrobo.Modules.Shared.Components.Entry.EntryComponent), typeof(Platforms.Android.Renderers.EntryRendererAndroid));
		#endif
	}
}