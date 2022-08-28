using MauiAppSandbox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.ViewModels
{
    public partial class AppUsersViewModel : ViewModelBase
    {
        public ObservableCollection<AppUser> AppUsers { get; } = new();

        private IAppUserRepository _appUserRepository;

        public AppUsersViewModel(IAppUserRepository appUserRepository)

        {
            _appUserRepository = appUserRepository;

        }

        [RelayCommand]
        public override async Task InitializeAsync()
        {
            await GetAppUsers();
        }

        [RelayCommand]
        async Task GetAppUsers()
        {
            var appUsers = await _appUserRepository.GetAll();
            if (AppUsers.Count != 0)
                AppUsers.Clear();

            foreach (var appUser in appUsers)
                AppUsers.Add(appUser);
        }

        [ObservableProperty]
        bool isRefreshing;


    }
}