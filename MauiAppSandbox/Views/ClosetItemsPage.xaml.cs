namespace MauiAppSandbox.Views;

public partial class ClosetItemsPage : ContentPageBase
{
	public ClosetItemsPage(ClosetItemsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

