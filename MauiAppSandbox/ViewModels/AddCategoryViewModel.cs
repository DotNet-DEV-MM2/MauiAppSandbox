namespace MauiAppSandbox.ViewModels
{
    public partial class AddCategoryViewModel : BaseViewModel
    {
        CategorySQLiteRepository _categoryRepo;

        IConnectivity connectivity;

        [ObservableProperty]
        string color;

        [ObservableProperty]
        string categoryType;

        [ObservableProperty]
        string categoryName;

        [ObservableProperty]
        string categoryTitle;

        [ObservableProperty]
        string iconGlyph;

        [ObservableProperty]
        string iconFamily;

        [ObservableProperty]
        string pictureUri;

        public AddCategoryViewModel(CategorySQLiteRepository categoryRepo,
            IConnectivity connectivity)
        {
            _categoryRepo = categoryRepo;

            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task SaveCategory()
        {
            /*var category = new ClosetItem
            {
                Name = name,
                Description = description,
                Size = selectedSize.CategoryName,
                Season = selectedSeason.CategoryName,
                Type = selectedType.CategoryName,
                PictureUri = "dress_red_1.png",
            };*/

            var category = new Category
            {
                CategoryType = "Size",
                CategoryName = "Large",
                CategoryTitle = "Large",
                Color = "#B96CBD",
                IconGlyph = "noun-shirt-3013538.png",
                IconFamily = "FontAwesome-Regular",
                PictureUri = "noun-shirt-3013538.png",
            };



            await Shell.Current.DisplayAlert("Add this item?", category.CategoryType + " " + category.CategoryName + " " + category.CategoryTitle + " " + category.Color + " " + category.PictureUri + " " + category.IconFamily +" " + category.IconGlyph, "OK");
            await _categoryRepo.InsertCategory(category);
            await Shell.Current.GoToAsync("..");

        }

    }
}