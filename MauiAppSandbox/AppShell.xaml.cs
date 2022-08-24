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
        Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        Routing.RegisterRoute(nameof(ClosetItemsPage), typeof(ClosetItemsPage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
    }
}
