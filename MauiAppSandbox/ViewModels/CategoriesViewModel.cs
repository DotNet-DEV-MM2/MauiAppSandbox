﻿using MauiAppSandbox.Interfaces;

namespace MauiAppSandbox.ViewModels
{
    public partial class CategoriesViewModel : ViewModelBase
    {
        public ObservableCollection<Category> Categories { get; } = new();

        private ICategoryRepository _categoryRepository;
      
        public CategoriesViewModel(ICategoryRepository categoryRepository)

        {
            _categoryRepository = categoryRepository;   

        }

        [RelayCommand]
        public override async Task InitializeAsync()
        {
            await GetCategories();
        }

        [RelayCommand]
        async Task GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            if (Categories.Count != 0)
                Categories.Clear();

            foreach (var category in categories)
                Categories.Add(category);
        }

        [ObservableProperty]
        bool isRefreshing;

       
    }
}
