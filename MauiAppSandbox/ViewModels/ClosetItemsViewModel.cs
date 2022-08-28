
using MauiAppSandbox.Interfaces;

namespace MauiAppSandbox.ViewModels
{
    public partial class ClosetItemsViewModel : ViewModelBase
    {
        public ObservableCollection<ClosetItem> ClosetItems { get; } = new();

        IClosetItemRepository _closetRepository;
/*
        IConnectivity connectivity;
        IGeolocation geolocation;*/

        public ClosetItemsViewModel(IClosetItemRepository closetRepository)

        {

            _closetRepository = closetRepository;

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
            var items = await _closetRepository.GetAll();
            if (ClosetItems.Count != 0)
                ClosetItems.Clear();

            foreach (var item in items)
                ClosetItems.Add(item);
        }

        [ObservableProperty]
        bool isRefreshing;
    }
}
