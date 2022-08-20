namespace MauiAppSandbox.Views;

public partial class AddClosetItemPage : ContentPage
{
	public AddClosetItemPage(AddClosetItemViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}