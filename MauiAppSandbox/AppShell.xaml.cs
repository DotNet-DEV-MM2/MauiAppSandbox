using MauiAppSandbox.Views;

namespace MauiAppSandbox;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Routing.RegisterRoute(nameof(ClosetItemDetailsPage), typeof(ClosetItemDetailsPage));
        //Routing.RegisterRoute(nameof(CategoriesView), typeof(CategoriesView));

        InitRoutes();
    }

    private void InitRoutes()
    {
        Routing.RegisterRoute(nameof(ClosetItemDetailsPage), typeof(ClosetItemDetailsPage));
        Routing.RegisterRoute(nameof(CategoriesView), typeof(CategoriesView));
    }

    private string selectedRoute;
    public string SelectedRoute
    {
        get { return selectedRoute; }
        set
        {
            selectedRoute = value;
            OnPropertyChanged();
        }
    }

    async void OnMenuItemChanged(System.Object sender, CheckedChangedEventArgs e)
    {
        if (!String.IsNullOrEmpty(selectedRoute))
            await Shell.Current.GoToAsync($"//{selectedRoute}");
    }
}
