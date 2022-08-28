using MauiAppSandbox.Interfaces;

namespace MauiAppSandbox.ViewModels
{

    public partial class AdminViewModel : ViewModelBase
    {
        private ICategoryRepository _categoryRepository;
        private IClosetItemRepository _closetItemRepository;
        private IAppUserRepository _appUserRepository;

        List<Category> categoryList;
        List<ClosetItem> closetItemList;
        List<AppUser> appUserList;

        public AdminViewModel(ICategoryRepository categoryRepository, 
            IClosetItemRepository closetItemRepository,
            IAppUserRepository appUserRepository)
        {
            _categoryRepository = categoryRepository;
            _closetItemRepository = closetItemRepository;
            _appUserRepository = appUserRepository;
        }


        // Categories Admin
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
                await _categoryRepository.SaveAsync(category);  
                Debug.WriteLine("Category being inserted. " + category.CategoryType + ", " + category.CategoryName);
            }

            await Shell.Current.DisplayAlert("Added Categories", categoryList.Count + " categories added.", "OK");
        }


        [RelayCommand]
        async Task DeleteCategories()
        {
            await _categoryRepository.DeleteAll();
            await Shell.Current.DisplayAlert("Deleted Categories", "categories deleted", "OK");
        }


        // Categories Admin
        [RelayCommand]
        async Task ReseedClosetItems()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("closetitemdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            closetItemList = JsonSerializer.Deserialize<List<ClosetItem>>(contents);

            foreach (var closetItem in closetItemList)
            {
                await _closetItemRepository.SaveAsync(closetItem);
            }

            await Shell.Current.DisplayAlert("Added Closet Items", closetItemList.Count + " closet items added.", "OK");
        }

        [RelayCommand]
        async Task DeleteClosetItems()
        {
            await _closetItemRepository.DeleteAll();
            await Shell.Current.DisplayAlert("Deleted Closet Items", "closet items deleted.", "OK");
        }

        // AppUser Admin
        [RelayCommand]
        async Task ReseedAppUsers()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("appuserdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            appUserList = JsonSerializer.Deserialize<List<AppUser>>(contents);

            foreach (var appUser in appUserList)
            {
                await _appUserRepository.SaveAsync(appUser);
            }

            await Shell.Current.DisplayAlert("Added App Users", appUserList.Count + " app users added.", "OK");
        }

        [RelayCommand]
        async Task DeleteAppUsers()
        {
            await _appUserRepository.DeleteAll();
            await Shell.Current.DisplayAlert("Deleted App Users", "app users deleted.", "OK");
        }

    }
}
