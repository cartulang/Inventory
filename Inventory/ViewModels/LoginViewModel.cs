using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly UserStore _userStore = null!;

        [ObservableProperty]
        private string _username = null!;

        [ObservableProperty]
        private string _password = null!;

        public LoginViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _userStore = new();
            LoginCommand = new AsyncRelayCommand(Login);
        }

        public IAsyncRelayCommand LoginCommand { get; }

        private async Task Login()
        {
            var user = new User() 
            {
                Username = _username,
                Password = _password
            };

            var isLoginSuccess = await _userStore.Login(user);

            if(isLoginSuccess)
            {
                UserStore.IsLoggedIn = true;
                _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
            }
        }

       
    }
}
