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
    class CategoryPageViewModel : BaseViewModel
    {
        public CategoryPageViewModel()
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
            if (string.IsNullOrEmpty(CategoryName))
            {
                return false;
            }
            var c = DataProvider.Ins.DB.THELOAIs.Where(x => x.TenTheLoai == CategoryName).Count();
            if (c > 0)
            {
                return false;
            }
            return true;
        }
        private void AddTolist()
        {
            _Items.Add(new Item_category() { Category = CategoryName, ID = (_Items.Count + 1).ToString() });
        }
        private void EditTolist()
        {
            SelectedItem.Category = CategoryName;
        }
        private void UpdateDB()
        {
            DataProvider.Ins.DB.THELOAIs.Add(new THELOAI() { TenTheLoai = CategoryName });
            DataProvider.Ins.DB.SaveChanges();

        }

        private void UpdateEditDB()
        {
            var author = DataProvider.Ins.DB.THELOAIs.Where(x => x.MaTheLoai.ToString() == SelectedItem.ID).SingleOrDefault();
            author.TenTheLoai = CategoryName;
            DataProvider.Ins.DB.SaveChanges();
        }

        private static ObservableCollection<Item_category> CreateData()
        {
            return new ObservableCollection<Item_category> { };
        }

        private void LoadAuthor()
        {
            int i = 1;
            foreach (var v in DataProvider.Ins.DB.THELOAIs)
            {
                _Items.Add(new Item_category() { Category = v.TenTheLoai, ID = i.ToString() });

                i++;
            }
        }
        private ObservableCollection<Item_category> _Items;
        private Item_category _selectedItem;
        private String _categoryName;
        public ObservableCollection<Item_category> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }
        public Item_category SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value; OnPropertyChanged();
                if (_selectedItem != null)
                {
                    CategoryName = _selectedItem.Category;
                }
            }
        }
        public string CategoryName { get => _categoryName; set { _categoryName = value; OnPropertyChanged(); } }
    }
}

