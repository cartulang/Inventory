using CommunityToolkit.Mvvm.Input;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly UserStore _userStore = null!;

        public HomeViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _userStore = new();
        }

        [RelayCommand]
        private void ToTransactions()
        {
            _navigationStore.CurrentViewModel = new TransactionsListViewModel(_navigationStore);
        }

        [RelayCommand]
        private void ToDeviceList()
        {
            _navigationStore.CurrentViewModel = new DeviceListViewModel(_navigationStore);
        }

        [RelayCommand]
        private void Logout()
        {
           _userStore.Logout();
           _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore);
        }
    }
}
