namespace MauiAppSandbox.ViewModels
{
    [QueryProperty(nameof(ClosetItem), "ClosetItem")]
    public partial class ClosetItemDetailsViewModel : BaseViewModel
    {
        IMap map;
        public ClosetItemDetailsViewModel(IMap map)
        {
            this.map = map;
        }

        [ObservableProperty]
        ClosetItem closetItem;

        [RelayCommand]
        async Task OpenMap()
        {
            try
            {
                await map.OpenAsync(ClosetItem.Latitude, ClosetItem.Longitude, new MapLaunchOptions
                {
                    Name = ClosetItem.Name,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }
    }
}
