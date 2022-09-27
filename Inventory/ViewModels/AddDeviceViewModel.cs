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
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _deviceName = null!;

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
                DeviceName = _deviceName,
            };

            bool isSuccess = await _deviceStore.AddDevice(device, _quantity, UserStore.UserName);

            if(isSuccess)
            {
                Cancel();
            }
        }

        private bool CanSubmit()
        {
            return  !string.IsNullOrEmpty(_deviceName) && _quantity > 0;
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

            if (_quantity < 0)
            {
                MessageBox.Show("Quantity must be greater than 0.");
                return false;
            }

            return true;
        }
    }
}
