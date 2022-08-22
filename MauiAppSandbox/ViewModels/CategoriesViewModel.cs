namespace MauiAppSandbox.ViewModels
{
    public partial class CategoriesViewModel : ViewModelBase
    {
        public ObservableCollection<Category> Categories { get; } = new();

        // if getting data from http service
        //CategoryHTTPService categoryService;

        // if getting data from local sqlite db
        CategorySQLiteRepository _categoryRepo;

        IConnectivity connectivity;

        // if getting data from http service
        /*public CategoriesViewModel(CategoryHTTPService categoryService, 
            IConnectivity connectivity)*/

        // if getting data from local sqlite db
        public CategoriesViewModel(CategorySQLiteRepository categoryRepo,
            IConnectivity connectivity)

        {

            // if getting data from http service
            // this.categoryService = categoryService;

            // if getting data from local sqlite db
            _categoryRepo = categoryRepo;

            this.connectivity = connectivity;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetCategoriesAsync()
        {
           /* if (IsBusy)
                return;*/

            try
            {
                // if getting data from http service
                /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }*/

               // IsBusy = true;

                // if using static data
                //var categories = await categoryService.GetCategories();

                // if using local sqlite db
                var categories = await _categoryRepo.GetAllCategories();
                if (Categories.Count != 0)
                    Categories.Clear();

                foreach (var category in categories)
                    Categories.Add(category);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get categories: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
           /* finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }*/

        }

       
    }
}
