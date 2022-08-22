using MauiAppSandbox.Services;
using MauiAppSandbox.Views;
using CommunityToolkit.Maui;

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
                })
            .UseMauiCommunityToolkit();
        string dbPath = FileAccessHelper.GetLocalFilePath("closet.db3");
        Console.WriteLine("DbPath is: " + dbPath);
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
        builder.Services.AddSingleton<ClosetItemsPage>();

        // categories
        builder.Services.AddSingleton<CategoriesViewModel>();
        builder.Services.AddSingleton<CategoriesPage>();
        // admin
        builder.Services.AddSingleton<AdminPage>();
        builder.Services.AddSingleton<AdminViewModel>();

        return builder.Build();
    }
}