using BookStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Item_ListCheckedBoxType: BaseViewModel
    {
        private string _Type;
        private bool _IsChecked;

        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public bool IsChecked { get => _IsChecked; set { _IsChecked = value; OnPropertyChanged(); } }
    }
}
