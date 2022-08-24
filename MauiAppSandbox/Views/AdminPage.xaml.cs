namespace MauiAppSandbox.Views;

public partial class AdminPage : ContentPageBase
{
	public AdminPage(AdminViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}