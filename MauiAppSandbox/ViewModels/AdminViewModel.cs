namespace MauiAppSandbox.ViewModels
{
   
    public partial class AdminViewModel : ViewModelBase
    {
        CategorySQLiteRepository _categoryRepo;
        ClosetItemSQLiteRepository _closetItemRepo;

        List<Category> categoryList;
        List<ClosetItem> closetItemList;

        public AdminViewModel(CategorySQLiteRepository categoryRepo, ClosetItemSQLiteRepository closetItemRepo)
        {
            _categoryRepo = categoryRepo;
            _closetItemRepo = closetItemRepo;
        }

        [RelayCommand]
        async Task ReseedCategories()
        {
            //  seed the categories
            using var stream = await FileSystem.OpenAppPackageFileAsync("categorydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            categoryList = JsonSerializer.Deserialize<List<Category>>(contents);

            foreach(var category in categoryList)
            {
                await _categoryRepo.InsertCategory(category);
            }

            await Shell.Current.DisplayAlert("Added Categories", categoryList.Count + " categories added.", "OK");

        }

        [RelayCommand]
        async Task ReseedClosetItems()
        {
            //  seed the categories
            using var stream = await FileSystem.OpenAppPackageFileAsync("closetItemdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            closetItemList = JsonSerializer.Deserialize<List<ClosetItem>>(contents);

            foreach (var closetItem in closetItemList)
            {
                await _closetItemRepo.InsertClosetItem(closetItem);
            }

            await Shell.Current.DisplayAlert("Added Closet Items", closetItemList.Count + " closet items added.", "OK");
        }


    }
}
