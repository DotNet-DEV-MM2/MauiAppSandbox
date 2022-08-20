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

       // public ObservableCollection<Category> CatItems { get;  } = new();

        public ObservableCollection<Category> SizeItems { get; } = new();

        public ObservableCollection<Category> SeasonItems { get; } = new();

        public ObservableCollection<Category>TypeItems { get; } = new();

        [ObservableProperty]
        private Category selectedSeason;

        [ObservableProperty]
        private string seasonText;

        [ObservableProperty]
        private Category selectedSize;

        [ObservableProperty]
        private string sizeText;

        [ObservableProperty]
        Category selectedType;

        [ObservableProperty]
        private string typeText;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string pictureUri;

        public AddClosetItemViewModel(ClosetItemSQLiteRepository closetItemRepo,
             CategorySQLiteRepository categoryRepo,
            IConnectivity connectivity)
        {
            _closetItemRepo = closetItemRepo;
            _categoryRepo = categoryRepo;

            this.connectivity = connectivity;
        }

        [RelayCommand]
        public async void Init()
        {
            var catItems = await _categoryRepo.GetAllCategories();
            var catCount = catItems.Count;
            await Shell.Current.DisplayAlert("OnNavigatedTo: How many cats", catCount.ToString(), "OK");

            foreach(var catItem in catItems)
            {
                if (catItem.CategoryType == "Season")
                {
                    SeasonItems.Add(catItem);
                }
            }

            foreach (var catItem in catItems)
            {
                if (catItem.CategoryType == "Type")
                {
                    TypeItems.Add(catItem);
                }
            }

            foreach (var catItem in catItems)
            {
                if (catItem.CategoryType == "Size")
                {
                    SizeItems.Add(catItem);
                }
            }

            Title = "Add Your Item";

        }

        [RelayCommand]
        private async Task SaveClosetItem()
        {
            /*catItems = await _categoryRepo.GetAllCategories();
            var cat1Count = catItems.Count;
            await Shell.Current.DisplayAlert("SaveClosetITem: How many cats", cat1Count.ToString(), "OK");*/
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
                Name = Name,
                Description = Description,
                Size = SelectedSize.CategoryTitle,
                Season = SelectedSeason.CategoryTitle,
                Type = SelectedType.CategoryTitle,
                PictureUri = "dress_red_1.png",
            };



            await Shell.Current.DisplayAlert("Add this item?", closetitem.Name + " " + closetitem.Description + " " + closetitem.Size + " " + closetitem.Season + " " + closetitem.Type + " " + closetitem.PictureUri, "OK");
            await _closetItemRepo.InsertClosetItem(closetitem);
            await Shell.Current.GoToAsync("..");

        }

    }
}
