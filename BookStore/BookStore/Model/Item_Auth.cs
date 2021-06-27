using BookStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Item_Auth : BaseViewModel
    {
        public Item_Auth()
        {

        }

        private String _ID;
        private String _Author;
        public String ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        public String Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }
    }
}