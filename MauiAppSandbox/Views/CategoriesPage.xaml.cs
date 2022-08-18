namespace MauiAppSandbox.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}