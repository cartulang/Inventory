using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Dtos;
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
    public partial class RestockOrUnloadViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly DeviceStore _deviceStore = null!;
        private readonly DeviceTransactionStore _deviceTransactionStore = null!;
        private readonly DeviceDto _deviceDto;
        [ObservableProperty]
        private List<string> _operations = new()
        { "Restock", "Unload" };

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _user = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _deviceName = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int _operation;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int _quantity = 1;

        public RestockOrUnloadViewModel(NavigationStore navigationStore, DeviceDto deviceDto)
        {
            _deviceStore = new();
            _deviceTransactionStore = new();
            _deviceDto = deviceDto;
            _navigationStore = navigationStore;
            SubmitCommand = new AsyncRelayCommand(Submit, CanSubmit);

            _deviceName = deviceDto.DeviceName;
        }

        public IAsyncRelayCommand SubmitCommand { get; }
        private async Task Submit()
        {
            var isSuccess = false;
            if (!ValidateInputs())
            {
                return;
            }

            if(_operation == 0)
            {
                isSuccess = await _deviceStore.RestockDevice(_quantity, _deviceDto.Id, _user);
            } 
            
            else
            {
                isSuccess = await _deviceStore.UnloadDevice(_deviceDto.Id, _user);
            }

            if(isSuccess)
            {
                Cancel();
                return;
            }
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrEmpty(_user) && !string.IsNullOrEmpty(_deviceName) && !string.IsNullOrEmpty(_operations[_operation]) && !(_quantity < 1);
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
