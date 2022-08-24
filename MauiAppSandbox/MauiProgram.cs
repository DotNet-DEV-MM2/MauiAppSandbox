using CommunityToolkit.Maui;
using MauiAppSandbox.Services;

namespace MauiAppSandbox;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
        => MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(
                fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews() 
            .Build();


    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        string dbPath = FileAccessHelper.GetLocalFilePath("MauiAppSandboxDb.db3");
        mauiAppBuilder.Services.AddSingleton<CategoryService>(s => ActivatorUtilities.CreateInstance<CategoryService>(s, dbPath));
        mauiAppBuilder.Services.AddSingleton<ClosetItemService>(s => ActivatorUtilities.CreateInstance<ClosetItemService>(s, dbPath));


        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CategoriesViewModel>();
        mauiAppBuilder.Services.AddSingleton<ClosetItemsViewModel>();
        mauiAppBuilder.Services.AddSingleton<AdminViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CategoriesPage>();
        mauiAppBuilder.Services.AddSingleton<ClosetItemsPage>();
        mauiAppBuilder.Services.AddSingleton<AdminPage>();

        return mauiAppBuilder;
    }
}