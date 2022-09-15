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
    partial class InputViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore = null!;
        private readonly Input _input = null!;
        private readonly DeviceStatusViewModel _deviceStatusList;
        public IEnumerable<DeviceStatus> DeviceStatusSelection => _deviceStatusList.DeviceStatusList;

        [ObservableProperty]
        private string _deviceName = null!;

        [ObservableProperty]
        private string _lastUser = null!;

        [ObservableProperty]
        private string _deviceStatus = null!;

        public InputViewModel(NavigationStore navigationStore)
        {
            _input = new();
            _deviceStatusList = new();
            SubmitCommand = new AsyncRelayCommand(Submit);
            _navigationStore = navigationStore;
        }

        public IAsyncRelayCommand SubmitCommand { get; }
        private async Task Submit()
        {

            if (!ValidateInputs())
            {
                MessageBox.Show("All inputs are required.");
                return;
            }
            var Date = DateOnly.FromDateTime(DateTime.Now).ToString("MM/dd/yyyy");
            var Time = TimeOnly.FromDateTime(DateTime.Now).ToString().ToUpper();
            _input.Id = DateTime.Now.ToString("MMddyyyyHHmmss");
            _input.DeviceName = _deviceName.Trim();
            _input.DeviceStatus = _deviceStatus;
            _input.LastUser = _lastUser.Trim();
            _input.Date = String.Format("{0}, {1}", Date, Time);
            /*await _input.AddInput(_input);*/
            bool isSubmitSuccess = await _input.AddInput(_input);

            if (!isSubmitSuccess)
            {
                MessageBox.Show("Error submitting.");
                return;
            }
            _deviceName = "";
            _lastUser = "";
            _deviceStatus = "";
            Cancel();
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationStore.CurrentViewModel = new InputListViewModel(_navigationStore);
        }

        private bool ValidateInputs()
        {
            if (String.IsNullOrEmpty(_deviceStatus)) return false;
            if (String.IsNullOrEmpty(_deviceName)) return false;
            if (String.IsNullOrEmpty(_lastUser)) return false;

            return true;
        }
    }
}
