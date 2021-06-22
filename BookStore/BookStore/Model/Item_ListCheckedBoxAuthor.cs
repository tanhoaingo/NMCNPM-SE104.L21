using BookStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Item_ListCheckedBoxAuthor: BaseViewModel
    {
        private string _Name;
        private bool _IsChecked;

        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        public bool IsChecked { get => _IsChecked; set { _IsChecked = value; OnPropertyChanged(); } }
    }
}
