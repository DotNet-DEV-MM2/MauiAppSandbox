using MauiAppSandbox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.ViewModels
{
    public partial class AddClosetItemViewModel : BaseViewModel
    {
        ClosetItemSQLiteRepository _closetItemRepo;
        CategorySQLiteRepository _categoryRepo;

        IConnectivity connectivity;

        [ObservableProperty]
        List<Category> catItems;

        [ObservableProperty]
        List<Category> sizeItems;

        [ObservableProperty]
        List<Category> seasonItems;

        [ObservableProperty]
        List<Category> typeItems;

        [ObservableProperty]
        Category selectedSeason;

        [ObservableProperty]
        string seasonText;

        /*[ObservableProperty]
        Category selectedSize
        {
            get => selectedSize;
            set
            {
                SetProperty(ref selectedSize, value);
                sizeText = "Size is " + selectedSize.CategoryName;
            }
        }*/

        [ObservableProperty]
        string sizeText;

        /*[ObservableProperty]
        Category selectedType
        {
            get => selectedType;
            set
            {
                SetProperty(ref selectedType, value);
                typeText = "Type is " + selectedType.CategoryName;
            }
        }*/

        [ObservableProperty]
        string typeText;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string pictureUri;

        [RelayCommand]
        async void AddClosetItemCommand()
        {
            Console.WriteLine("AddClosetItemCommand");
            await SaveNewClosetItem();
        }

        public AddClosetItemViewModel(ClosetItemSQLiteRepository closetItemRepo,
             CategorySQLiteRepository categoryRepo,
            IConnectivity connectivity)
        {
            _closetItemRepo = closetItemRepo;
            _categoryRepo = categoryRepo;

            this.connectivity = connectivity;
        }

        public async void OnNavigatedTo()
        {
            catItems = await _categoryRepo.GetAllCategories();
            seasonItems = catItems.Where(i => i.CategoryType == "Season").ToList();
            typeItems = catItems.Where(i => i.CategoryType == "Type").ToList();
            sizeItems = catItems.Where(i => i.CategoryType == "Size").ToList();
            Title = "Add Your Item";

        }


        private async Task SaveNewClosetItem()
        {
            /*var closetitem = new ClosetItem
            {
                Name = name,
                Description = description,
                Size = selectedSize.CategoryName,
                Season = selectedSeason.CategoryName,
                Type = selectedType.CategoryName,
                PictureUri = "dress_red_1.png",
            };*/

            var closetitem = new ClosetItem
            {
                Name = name,
                Description = description,
                Size = "Medium",
                Season = selectedSeason.CategoryName,
                Type = "Dress",
                PictureUri = "dress_red_1.png",
            };



            await Shell.Current.DisplayAlert("Add this item?", closetitem.Name + " " + closetitem.Description + " " + closetitem.Size + " " + closetitem.Season + " " + closetitem.Type + " " + closetitem.PictureUri, "OK");
            await _closetItemRepo.InsertClosetItem(closetitem);
            await Shell.Current.GoToAsync("..");

        }

    }
}
