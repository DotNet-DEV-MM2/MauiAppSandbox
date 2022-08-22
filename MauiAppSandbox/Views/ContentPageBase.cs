using MauiAppSandbox.ViewModels.Base;

namespace MauiAppSandbox.Views
{
    public abstract class ContentPageBase : ContentPage
    {

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IViewModelBase ivmb && !ivmb.IsInitialized)
            {
                ivmb.IsInitialized = true;
                await ivmb.InitializeAsync();
            }
        }
    }
}
