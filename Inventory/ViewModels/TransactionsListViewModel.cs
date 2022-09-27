using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Dtos;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    partial class TransactionsListViewModel : ViewModelBase
    {
        private readonly BackgroundWorker bgWork = new();
        private readonly NavigationStore _navigationStore = null!;
        private readonly DeviceTransactionStore _deviceTransactionStore = null!;
        private IEnumerable<DeviceTransactionDto> _allTransactions = null!;

        [ObservableProperty]
        private ObservableCollection<DeviceTransactionDto> _transactions = null!;

        public TransactionsListViewModel(NavigationStore navigationStore)
        {
            _deviceTransactionStore = new();
            _transactions = new();
            _navigationStore = navigationStore;

            bgWork.DoWork += FetchTransactions;
            bgWork.RunWorkerCompleted += DoneFetching;
            bgWork.RunWorkerAsync();
        }

        private void DoneFetching(object? sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var transaction in _allTransactions)
            {
                _transactions.Add(transaction);
            }
        }

        private void FetchTransactions(object? sender, DoWorkEventArgs e)
        {
            _allTransactions = Task.Run(_deviceTransactionStore.GetAllTransactions).Result;
        }

        [RelayCommand]
        private void ToInputView()
        {
            _navigationStore.CurrentViewModel = new AddDeviceViewModel(_navigationStore);
        }

        [RelayCommand]
        private void ToHomeView()
        {
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
        }
    }
}
