using MauiAppSandbox.Services;
using MauiAppSandbox.Views;

namespace MauiAppSandbox;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services.AddSingleton<ClosetItemService>();
        builder.Services.AddSingleton<ClosetItemsViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<ClosetItemDetailsViewModel>();
        builder.Services.AddTransient<ClosetItemDetailsPage>();

        return builder.Build();
	}
}
