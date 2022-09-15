using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.ViewModels
{
    public partial class InputViewModel : ObservableObject
    {
        private readonly Input _input = null!;
        private readonly DeviceStatusViewModel _deviceStatusList;
        public IEnumerable<DeviceStatus> DeviceStatusSelection => _deviceStatusList.DeviceStatusList;

        [ObservableProperty]
        private string _deviceName = null!;

        [ObservableProperty]
        private string _lastUser = null!;

        [ObservableProperty]
        private string _deviceStatus = null!;

        public InputViewModel()
        {
            _input = new();
            _deviceStatusList = new();
            SubmitCommand = new AsyncRelayCommand(Submit);
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

            bool isSubmitSuccess = await _input.AddInput(_input);

            if(!isSubmitSuccess)
            {
                MessageBox.Show("Error submitting.");
            }
        }

        [RelayCommand]
        private void Cancel()
        {

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
