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

        string dbPath = FileAccessHelper.GetLocalFilePath("closet.db3");
        builder.Services.AddSingleton<ClosetItemSQLiteRepository>(s => ActivatorUtilities.CreateInstance<ClosetItemSQLiteRepository>(s, dbPath));
        builder.Services.AddSingleton<CategorySQLiteRepository>(s => ActivatorUtilities.CreateInstance<CategorySQLiteRepository>(s, dbPath));

        // add services
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        //  register services for connection to data
        //  http
       /* builder.Services.AddSingleton<ClosetItemHTTPService>();
        builder.Services.AddSingleton<CategoryHTTPService>();*/

        // sqlite
       /* builder.Services.AddSingleton<ClosetItemSQLiteRepository>();
        builder.Services.AddSingleton<CategorySQLiteRepository>();*/

        // register views and viewmodels

        // closet
        builder.Services.AddSingleton<ClosetItemsViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<ClosetItemDetailsViewModel>();
        builder.Services.AddTransient<ClosetItemDetailsPage>();

        builder.Services.AddSingleton<AddClosetItemViewModel>();
        builder.Services.AddSingleton<AddClosetItemView>();

        // categories
        builder.Services.AddSingleton<CategoriesViewModel>();
        builder.Services.AddSingleton<CategoriesView>();



        return builder.Build();
	}
}
