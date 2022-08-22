namespace MauiAppSandbox.Views;

public partial class CategoriesPage : ContentPageBase
{
	public CategoriesPage(CategoriesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}