using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;

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
			.ConfigureMauiHandlers((handlers) => { NewHandlers(handlers); })
			.ConfigureLifecycleEvents(events => { NewLifeCycle(events); });


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

    private static void NewLifeCycle(ILifecycleBuilder events)
    {
		#if ANDROID33_0_OR_GREATER
			events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));
				static void MakeStatusBarTranslucent(Android.App.Activity activity)
				{
					activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

					activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

					activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);

					activity.Window.SetNavigationBarColor(Android.Graphics.Color.Black);
				}
		#endif
    }
}