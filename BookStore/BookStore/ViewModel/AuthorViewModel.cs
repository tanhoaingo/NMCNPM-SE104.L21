using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModel
{
    public class AuthorViewModel : BaseViewModel
    {
        public AuthorViewModel()
        {
            Items = CreateData();
            LoadAuthor();
        }

        private static ObservableCollection<Item_Auth> CreateData()
        {
            return new ObservableCollection<Item_Auth> { };
        }

        private void LoadAuthor()
        {
            int i = 1;
            foreach(var v in DataProvider.Ins.DB.TACGIAs)
            {
                _Items.Add(new Item_Auth() { Author=v.TenTacGia , ID =i.ToString() });
                i++;
            }
        }
        private ObservableCollection<Item_Auth> _Items;
        private Item_Auth _selectedItem;
        public ObservableCollection<Item_Auth> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }
        public Item_Auth SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }
        
    }
}
