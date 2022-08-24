using MauiAppSandbox.Services;

namespace MauiAppSandbox.ViewModels
{

    public partial class AdminViewModel : ViewModelBase
    {
        CategoryService _categoryService;
        ClosetItemService _closetItemService;

        List<Category> categoryList;
        List<ClosetItem> closetItemList;

        public AdminViewModel(CategoryService categoryService, ClosetItemService closetItemService)
        {
            _categoryService = categoryService;
            _closetItemService = closetItemService;
        }

        [RelayCommand]
        async Task ReseedCategories()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("categorydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            categoryList = JsonSerializer.Deserialize<List<Category>>(contents);

            Debug.WriteLine("Categories have been deserialized from JSON file and are in categoryList.  Now, insert them into Category table");

            foreach (var category in categoryList)
            {
                await _categoryService.InsertCategory(category);
                Debug.WriteLine("Category being inserted. " + category.CategoryType + ", " + category.CategoryName);
            }

            await Shell.Current.DisplayAlert("Added Categories", categoryList.Count + " categories added.", "OK");

        }

        [RelayCommand]
        async Task ReseedClosetItems()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("closetItemdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            closetItemList = JsonSerializer.Deserialize<List<ClosetItem>>(contents);

            foreach (var closetItem in closetItemList)
            {
                await _closetItemService.InsertClosetItem(closetItem);
            }

            await Shell.Current.DisplayAlert("Added Closet Items", closetItemList.Count + " closet items added.", "OK");
        }

        [RelayCommand]
        async Task DeleteClosetItems()
        {
            await _closetItemService.DeleteAll();
            await Shell.Current.DisplayAlert("Deleted Closet Items", "closet items deleted.", "OK");
        }

        [RelayCommand]
        async Task DeleteCategories()
        {
            await _categoryService.DeleteAll();
            await Shell.Current.DisplayAlert("Deleted Categories", "categories deleted", "OK");


        }
    }
}
