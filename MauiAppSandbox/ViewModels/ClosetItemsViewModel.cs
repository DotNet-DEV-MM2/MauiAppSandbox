using MauiAppSandbox.Services;
using MauiAppSandbox.Views;

namespace MauiAppSandbox.ViewModels
{
    public partial class ClosetItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ClosetItem> ClosetItems { get; } = new();
        ClosetItemService closetItemService;
        IConnectivity connectivity;
        IGeolocation geolocation;
        public ClosetItemsViewModel(ClosetItemService closetItemService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "ClosetItem Finder";
            this.closetItemService = closetItemService;
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
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                var closetItems = await closetItemService.GetClosetItems();

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
        async Task GetClosestClosetItem()
        {
            if (IsBusy || ClosetItems.Count == 0)
                return;

            try
            {
                // Get cached location, else get real location.
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                // Find closest monkey to us
                var first = ClosetItems.OrderBy(m => location.CalculateDistance(
                    new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await Shell.Current.DisplayAlert("", first.Name + " " +
                    first.Location, "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
