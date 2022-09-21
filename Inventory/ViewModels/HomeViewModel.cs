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

        public HomeViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
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
    }
}
