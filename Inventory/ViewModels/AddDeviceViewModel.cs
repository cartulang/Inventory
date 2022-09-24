using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Rpc;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.ViewModels
{
    partial class AddDeviceViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly DeviceStore _deviceStore = null!;
        private readonly DeviceTransactionStore _deviceTransactionStore = null!;

        [ObservableProperty]
        private List<string> _deviceStatusSelection = new()
        { "Working", "Defective", "In-Use" };

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _user = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _deviceName = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int _deviceStatus;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int _quantity = 1;

        public AddDeviceViewModel(NavigationStore navigationStore)
        {
            _deviceStore = new();
            _deviceTransactionStore = new();
            _navigationStore = navigationStore;
            SubmitCommand = new AsyncRelayCommand(Submit, CanSubmit);
        }


        public IAsyncRelayCommand SubmitCommand { get; }
        private async Task Submit()
        {

            if (!ValidateInputs())
            {
                return;
            }

            var device = new Device()
            {
   /*             LastUser = _user,
                DeviceName = _deviceName.Trim(),
                Status = _deviceStatusSelection[_deviceStatus],
                Quantity = _quantity*/
            };

            var isSuccess = await _deviceTransactionStore.CreateTransaction("Deposit", device) && await _deviceStore.AddDevice(device);

            if (isSuccess)
            {
                Cancel();
                return;
            }
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrEmpty(_user) && !string.IsNullOrEmpty(_deviceName) && !string.IsNullOrEmpty(_deviceStatusSelection[_deviceStatus]) && !(_quantity < 1);
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationStore.CurrentViewModel = new DeviceListViewModel(_navigationStore);
        }

        private bool ValidateInputs()
        {
            if (String.IsNullOrEmpty(_deviceName))
            {
                MessageBox.Show("All input fields are required.");
                return false;
            }

            if (_quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.");
                return false;
            }

            return true;
        }
    }
}
