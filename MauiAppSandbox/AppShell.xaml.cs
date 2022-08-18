namespace MauiAppSandbox;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        GetRoutes();
    }

    private void GetRoutes()
    {
        Routing.RegisterRoute(nameof(ClosetItemDetailsPage), typeof(ClosetItemDetailsPage));
        Routing.RegisterRoute(nameof(AddClosetItemPage), typeof(AddClosetItemPage));
        Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
    }
}
