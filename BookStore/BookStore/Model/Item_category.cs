using BookStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Item_category : BaseViewModel
    {
        public Item_category()
        {

        }

        private String _ID;
        private String _category;
        public String ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        public String Category { get => _category; set { _category = value; OnPropertyChanged(); } }
    }
}