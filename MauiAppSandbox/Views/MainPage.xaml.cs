namespace MauiAppSandbox.Views;

public partial class MainPage : ContentPage
{
	public MainPage(ClosetItemsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

