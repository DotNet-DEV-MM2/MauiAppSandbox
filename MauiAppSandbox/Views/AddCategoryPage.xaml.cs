namespace MauiAppSandbox.Views;
public partial class AddCategoryPage : ContentPage
{
	public AddCategoryPage(AddCategoryViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}