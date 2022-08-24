using MauiAppSandbox.Services;
using MauiAppSandbox.Views;

namespace MauiAppSandbox.ViewModels
{
    public partial class ClosetItemsViewModel : ViewModelBase
    {
        public ObservableCollection<ClosetItem> ClosetItems { get; } = new();

        ClosetItemService _closetService;
/*
        IConnectivity connectivity;
        IGeolocation geolocation;*/

        public ClosetItemsViewModel(ClosetItemService closetService)

        {

            _closetService = closetService;

           /* this.connectivity = connectivity;
            this.geolocation = geolocation;*/
        }


        [RelayCommand]
        public override async Task InitializeAsync()
        {
            await GetClosetItems();
        }

        [RelayCommand]
        async Task GetClosetItems()
        {
            var items = await _closetService.GetAllClosetItems();
            if (ClosetItems.Count != 0)
                ClosetItems.Clear();

            foreach (var item in items)
                ClosetItems.Add(item);
        }

        [ObservableProperty]
        bool isRefreshing;
    }
}
