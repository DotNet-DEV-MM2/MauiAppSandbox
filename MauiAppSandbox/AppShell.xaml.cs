using MauiAppSandbox.Views;

namespace MauiAppSandbox;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ClosetItemDetailsPage), typeof(ClosetItemDetailsPage));
	}
}
