using MauiAppSandbox.Services;

namespace MauiAppSandbox.ViewModels
{
    public partial class CategoriesViewModel : ViewModelBase
    {
        public ObservableCollection<Category> Categories { get; } = new();

        private CategoryService _categoryService;
      
        public CategoriesViewModel(CategoryService categoryService)

        {
            _categoryService = categoryService;   

        }

        [RelayCommand]
        public override async Task InitializeAsync()
        {
            await GetCategories();
        }

        [RelayCommand]
        async Task GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            if (Categories.Count != 0)
                Categories.Clear();

            foreach (var category in categories)
                Categories.Add(category);
        }

        [ObservableProperty]
        bool isRefreshing;

       
    }
}
