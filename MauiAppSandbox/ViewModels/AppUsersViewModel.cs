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
            if (IsBusy)
                return;

            try
            {
                var appUsers = await _appUserRepository.GetAll();
                if (AppUsers.Count != 0)
                AppUsers.Clear();

                foreach (var appUser in appUsers)
                    AppUsers.Add(appUser);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get app users: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [ObservableProperty]
        bool isRefreshing;


    }
}