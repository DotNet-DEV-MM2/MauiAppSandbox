namespace MauiAppSandbox.Views;

public partial class ClosetItemDetailsPage : ContentPage
{
	public ClosetItemDetailsPage(ClosetItemDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}