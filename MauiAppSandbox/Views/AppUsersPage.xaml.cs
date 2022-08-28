namespace MauiAppSandbox.Views;

public partial class AppUsersPage : ContentPageBase
{
	public AppUsersPage(AppUsersViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}