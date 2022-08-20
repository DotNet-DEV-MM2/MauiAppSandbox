using MauiAppSandbox.Services;
using MauiAppSandbox.Views;

namespace MauiAppSandbox.ViewModels
{
    public partial class ClosetItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ClosetItem> ClosetItems { get; } = new();

        // if getting data from http service
        //ClosetItemHTTPService closetItemService;

        // if getting data from local sqlite db
        ClosetItemSQLiteRepository _closetItemRepo;

        IConnectivity connectivity;
        IGeolocation geolocation;

        // if getting data from http service
        /*public ClosetItemsViewModel(ClosetItemHTTPService closetItemService, 
            IConnectivity connectivity, 
            IGeolocation geolocation)*/

        // if getting data from local sqlite db
        public ClosetItemsViewModel(ClosetItemSQLiteRepository closetItemRepo,
            IConnectivity connectivity,
            IGeolocation geolocation)

        {
            Title = "ClosetItem Finder";

            // if getting data from http service
            // this.closetItemService = closetItemService;

            // if getting data from local sqlite db
            _closetItemRepo = closetItemRepo;

            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetClosetItemsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                // if getting data from http service
                /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }*/

                IsBusy = true;

                // if using static data
                //var closetItems = await closetItemService.GetClosetItems();

                // if using local sqlite db
                var closetItems = await _closetItemRepo.GetAllClosetItems();
                if (ClosetItems.Count != 0)
                    ClosetItems.Clear();

                foreach (var closetItem in closetItems)
                    ClosetItems.Add(closetItem);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get closetItems: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        [RelayCommand]
        async Task GoToDetails(ClosetItem closetItem)
        {
            if (closetItem == null)
                return;

            await Shell.Current.GoToAsync(nameof(ClosetItemDetailsPage), true, new Dictionary<string, object>
        {
            {"ClosetItem", closetItem }
        });
        }

        [RelayCommand]
        async Task InsertClosetItem()
        {
            if (IsBusy)
                return;

            try
            {
                await Shell.Current.GoToAsync(nameof(AddClosetItemPage));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to open Details Page: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
