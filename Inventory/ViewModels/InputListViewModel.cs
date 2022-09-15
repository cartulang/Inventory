using CommunityToolkit.Mvvm.ComponentModel;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    partial class InputListViewModel : ObservableObject
    {
        private readonly Input? _input = null!;
        [ObservableProperty]
        private ObservableCollection<Input>? _inputs = null!;

        public InputListViewModel()
        {
            _input = new();
            _inputs = new();
           
            foreach(var input in _input.GetInventory())
            {
                _inputs.Add(input);
            }
        }
    }
}
