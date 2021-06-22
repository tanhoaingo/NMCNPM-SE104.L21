using BookStore.Model;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BookStore.ViewModel
{
    public class AddBookViewModel: BaseViewModel
    {

        public AddBookViewModel()
        {
            LoadListTypes();
            LoadListAuthor();
            
                
            ConfirmButtonClickCommand = new RelayCommand<TextBlock>((p) => { return true; }, (p) => { ConfirmAddBook(p); });
            BookNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p); });
            TypeTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p); TextListBookTypesChanged(); });
            AuthorTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p);TextListBookAuthorsChanged(); });
            ShowListBookTypesCommand= new RelayCommand<Window>((p) => { return true; }, (p) => { ShowListBookTypes(p);  });
            ShowListBookAuthorsCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ShowListBookAuthors(p); });
            ItemTypeChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemTypeChecked(p); });
            ItemAuthorChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemAuthorChecked(p); });
            ItemTypeUnChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemTypeChecked(p); });
            ItemAuthorUnChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemAuthorChecked(p); });
            AddListClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddList(p); });
        }

        private void AddList(Window p)
        {
            throw new NotImplementedException();
        }

        private void ItemAuthorChecked(Window p)
        {
            if (ListSelectedAuthor == null)
            {
                ListSelectedAuthor = new List<string>();
            }
            var tmp = p as AddBookWindow;
            Author = "";
            foreach (var v in tmp.exListBookAuthors.SelectedItems)
            {
                Author += (Author == "") ? "" : ", ";
                Author += (v as Item_ListCheckedBoxAuthor).Name;
            }
        }

        private void ItemTypeChecked(Window p)
        {
            var tmp = p as AddBookWindow;
            if (ListSelectedType == null)
            {
                ListSelectedType = new List<string>();
            }
            Type = "";
            foreach (var v in tmp.exListBookTypes.SelectedItems)
            {
                Type += (Type == "") ? "" : ", ";
                Type += (v as Item_ListCheckedBoxType).Type;

                
            }

        }

        private void TextListBookAuthorsChanged()
        {
           //do something
        }

        private void TextListBookTypesChanged()
        {
            //do something
        }

        private void ShowListBookAuthors(Window p)
        {
            var tmp = p as AddBookWindow;
            tmp.exListBookTypes.Visibility = Visibility.Hidden;
            tmp.exListBookAuthors.Visibility = Visibility.Visible;
        }

        private void LoadListAuthor()
        {
            ListAuthors = new ObservableCollection<Item_ListCheckedBoxAuthor>();
            foreach(var v in DataProvider.Ins.DB.TACGIAs)
            {
                ListAuthors.Add(new Item_ListCheckedBoxAuthor() { Name = v.TenTacGia, IsChecked = false });
            }
        }

        private void ShowListBookTypes(Window p)
        {
            var tmp = p as AddBookWindow;
            tmp.exListBookAuthors.Visibility = Visibility.Hidden;
            tmp.exListBookTypes.Visibility = Visibility.Visible;
        }

        private void LoadListTypes()
        {
            ListTypes = new ObservableCollection<Item_ListCheckedBoxType>();
            foreach (var v in DataProvider.Ins.DB.THELOAIs)
            {
                ListTypes.Add(new Item_ListCheckedBoxType() { Type = v.TenTheLoai, IsChecked = false });
            }
        }

        private void HiddenErrorTextBlock(Window p)
        {
            var tmp = p as AddBookWindow;
            tmp.ErrorAddBook.Visibility = Visibility.Hidden;
        }

        private void ConfirmAddBook(TextBlock p)
        {
            if (string.IsNullOrEmpty(BookName)||string.IsNullOrEmpty(Type)||string.IsNullOrEmpty(Author))
            {
                p.Visibility = Visibility.Visible;
                return;
            }
        }

        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand BookNameTextChangedCommand { get; set; }
        public ICommand TypeTextChangedCommand { get; set; }
        public ICommand AuthorTextChangedCommand { get; set; }
        public ICommand ShowListBookTypesCommand { get; set; }
        public ICommand ShowListBookAuthorsCommand { get; set; }
        public ICommand ItemTypeChekedCommand { get; set; }
        public ICommand ItemAuthorChekedCommand { get; set; }
        public ICommand ItemTypeUnChekedCommand { get; set; }
        public ICommand ItemAuthorUnChekedCommand { get; set; }
        public ICommand AddListClickCommand { get; set; }


        private string _BookName;
        private string _Type;
        private string _Author;
        private ObservableCollection<Item_ListCheckedBoxType> _ListTypes;
        private ObservableCollection<Item_ListCheckedBoxAuthor> _ListAuthors;
        private List<string> _ListSelectedType;
        private List<string> _ListSelectedAuthor;

        public string BookName { get => _BookName; set { _BookName = value; OnPropertyChanged(); } }
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public string Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }
        public ObservableCollection<Item_ListCheckedBoxType> ListTypes { get => _ListTypes; set { _ListTypes = value; OnPropertyChanged(); } }

        public ObservableCollection<Item_ListCheckedBoxAuthor> ListAuthors { get => _ListAuthors; set { _ListAuthors = value; OnPropertyChanged(); } }
        public List<string> ListSelectedType { get => _ListSelectedType; set => _ListSelectedType = value; }
        public List<string> ListSelectedAuthor { get => _ListSelectedAuthor; set => _ListSelectedAuthor = value; }
    }
}
