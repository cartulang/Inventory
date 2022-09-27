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

namespace Inventory.ViewModels
{
    public partial class WithdrawOrReturnDeviceViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly DeviceDto _deviceDto = null!;
        private readonly DeviceStore _deviceStore = null!;
        private readonly DeviceTransactionStore _deviceTransactionStore = null!;

        [ObservableProperty]
        private List<string> _operations = new()
        { "Withdraw", "Return" };

        [ObservableProperty]
        private int _selectedOperation = 0;


        [ObservableProperty]
        private string _deviceName = null!;

        [ObservableProperty]
        private int _quantity = 1;

        public IAsyncRelayCommand WithdrawOrReturnCommand { get; }

        public WithdrawOrReturnDeviceViewModel(NavigationStore navigationStore, DeviceDto deviceDto)
        {
            _navigationStore = navigationStore;
            _deviceDto = deviceDto;
            _deviceStore = new();
            _deviceTransactionStore = new();
            _deviceName = deviceDto.DeviceName;
            WithdrawOrReturnCommand = new AsyncRelayCommand(WithdrawOrReturnDevice, CanSubmit);
        }

        private async Task WithdrawOrReturnDevice()
        {
            bool isSuccess;

            if(_selectedOperation == 0)
            {
                isSuccess = await _deviceStore.WithdrawDevice(_deviceDto, _quantity, UserStore.UserName);
            } 
            
            else
            { 
                isSuccess = await _deviceStore.ReturnDevice(_deviceDto, _quantity, UserStore.UserName);
            }

            if (isSuccess)
            {
                Cancel();
            }
        }


        [RelayCommand]
        private void Cancel()
        {
            _navigationStore.CurrentViewModel = new DeviceListViewModel(_navigationStore);
        }

        private bool CanSubmit()
        {
            return _quantity > 0;
        }
    }
}
