using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class AuthorViewModel : BaseViewModel
    {
        public AuthorViewModel()
        {
            Items = CreateData();
            LoadAuthor();

            AddDetailClickCommand = new RelayCommand<object>((p) =>
            { return checkADD(); }, (p) => { AddTolist(); UpdateDB(); });

            EditDetailClickCommand = new RelayCommand<object>((p) =>
            { return checkADD(); }, (p) => { EditTolist(); UpdateEditDB(); });
            DeleteDetailClickCommand = new RelayCommand<object>((p) =>
            { return checkDelete(); }, (p) => { });
        }

        public ICommand AddDetailClickCommand { get; set; }
        public ICommand EditDetailClickCommand { get; set; }
        public ICommand DeleteDetailClickCommand { get; set; }

        private bool checkDelete()
        {
            return false; // check dieu kien o day
        }
        private bool checkADD()
        {
            if (string.IsNullOrEmpty(AuthorName))
            {
                return false;
            }
            var c = DataProvider.Ins.DB.TACGIAs.Where(x => x.TenTacGia == AuthorName).Count();
            if(c>0)
            {
                return false;
            }
            return true;
        }
        private void AddTolist()
        {
            _Items.Add(new Item_Auth() { Author = AuthorName, ID = (_Items.Count+1).ToString() });
        } 
        private void EditTolist()
        {
            SelectedItem.Author = AuthorName;
        } 
        private void UpdateDB()
        {
            DataProvider.Ins.DB.TACGIAs.Add(new TACGIA() { TenTacGia = AuthorName });
            DataProvider.Ins.DB.SaveChanges();
           
        }

        private void UpdateEditDB()
        {
            var author = DataProvider.Ins.DB.TACGIAs.Where(x => x.MaTacGia.ToString() == SelectedItem.ID).SingleOrDefault();
            author.TenTacGia = AuthorName;
            DataProvider.Ins.DB.SaveChanges();
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
        private String _authorName;
        public ObservableCollection<Item_Auth> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }
        public Item_Auth SelectedItem { get => _selectedItem; set { 
                _selectedItem = value; OnPropertyChanged();
                if (_selectedItem != null)
                {
                    AuthorName = _selectedItem.Author;
                }
            } }
        public string AuthorName { get => _authorName; set { _authorName = value; OnPropertyChanged(); } }
    }
}
