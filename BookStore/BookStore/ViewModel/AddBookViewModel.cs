using BookStore.Model;
using BookStore.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BookStore.ViewModel
{
    public class AddBookViewModel: BaseViewModel
    {

        public AddBookViewModel()
        {
            FlagListNow = 0;

            LoadListTypes();
            LoadListAuthor();
            
                
            ConfirmButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ConfirmAddBook(p); });
            BookNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p);HiddenSomething(p); });
            TypeTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p); TextListBookTypesChanged(); });
            AuthorTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p);TextListBookAuthorsChanged(); });
            ShowListBookTypesCommand= new RelayCommand<Window>((p) => { return true; }, (p) => { ShowListBookTypes(p);  });
            ShowListBookAuthorsCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ShowListBookAuthors(p); });
            ItemTypeChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemTypeChecked(p); });
            ItemAuthorChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemAuthorChecked(p); });
            ItemTypeUnChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemTypeChecked(p); });
            ItemAuthorUnChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemAuthorChecked(p); });
            AddListClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddList(p); });
            AddPictureCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddPicture(p); });
        }

        private void AddPicture(Window p)
        {
            var tmpWD = p as AddBookWindow;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BookImage = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void HiddenSomething(Window p)
        {
            FlagListNow = 0;
            var tmp = p as AddBookWindow;
            tmp.exListBookTypes.Visibility = Visibility.Hidden;
            tmp.exListBookAuthors.Visibility = Visibility.Hidden;
            tmp.AddButton.Visibility = Visibility.Hidden;
            
        }

        private void AddList(Window p)
        {
            var tmpWD = p as AddBookWindow;
            if (FlagListNow == 2)
            {
                var tmp = new AddAuthorWindow();
                tmp.AuthorText.Focus();
                tmp.ShowDialog();
                LoadListAuthor();
                var tmpindex = ListAuthors.IndexOf(ListAuthors.Where(x => x.Name == (tmp.DataContext as AddAuthorViewModel).AuthorName).FirstOrDefault());
                tmpWD.exListBookAuthors.SelectedIndex = tmpindex;
                ItemAuthorChecked(tmpWD);
            }
            else if (FlagListNow == 1)
            {
                var tmp = new AddTypeWindow();
                tmp.TypeText.Focus();
                tmp.ShowDialog();
                LoadListTypes();
                var tmpindex = ListTypes.IndexOf(ListTypes.Where(x => x.Type == (tmp.DataContext as AddTypeViewModel).TypeName).FirstOrDefault());
                tmpWD.exListBookTypes.SelectedIndex = tmpindex;
                ItemTypeChecked(tmpWD);
            }
            else
            {
                return;
            }
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
            FlagListNow = 2;
            var tmp = p as AddBookWindow;
            foreach (var v in ListAuthors)
            {
                if (v.IsChecked == true)
                {
                   
                }
            }
            tmp.exListBookTypes.Visibility = Visibility.Hidden;
            tmp.exListBookAuthors.Visibility = Visibility.Visible;
            tmp.AddButton.Visibility = Visibility.Visible;
        }

        private void LoadListAuthor()
        {
            var tmpListAuthors = new ObservableCollection<Item_ListCheckedBoxAuthor>();
            foreach(var v in DataProvider.Ins.DB.TACGIAs)
            {
                tmpListAuthors.Add(new Item_ListCheckedBoxAuthor() { Name = v.TenTacGia, IsChecked = false });
            }
            ListAuthors = new ObservableCollection<Item_ListCheckedBoxAuthor>(tmpListAuthors.OrderBy(x => x.Name).ToList());
        }

        private void ShowListBookTypes(Window p)
        {
            FlagListNow = 1;
            var tmp = p as AddBookWindow;
            tmp.exListBookAuthors.Visibility = Visibility.Hidden;
            tmp.exListBookTypes.Visibility = Visibility.Visible;
            tmp.AddButton.Visibility = Visibility.Visible;
        }

        private void LoadListTypes()
        {
            var tmpListTypes = new ObservableCollection<Item_ListCheckedBoxType>();
            foreach (var v in DataProvider.Ins.DB.THELOAIs)
            {
                tmpListTypes.Add(new Item_ListCheckedBoxType() { Type = v.TenTheLoai, IsChecked = false });
            }
            ListTypes = new ObservableCollection<Item_ListCheckedBoxType> ( tmpListTypes.OrderBy(x => x.Type).ToList() );
        }

        private void HiddenErrorTextBlock(Window p)
        {
            var tmp = p as AddBookWindow;
            tmp.ErrorAddBook.Visibility = Visibility.Hidden;
        }

        private void ConfirmAddBook(Window p)
        {
            var tmpWD = p as AddBookWindow;
            if (string.IsNullOrEmpty(BookName)||string.IsNullOrEmpty(Type)||string.IsNullOrEmpty(Author)||BookImage == null)
            {
                tmpWD.ErrorAddBook.Visibility = Visibility.Visible;
                return;
            }

            var tmpBook = new DAUSACH() { TenSach = BookName, LuongTon = 0 };
            foreach (var v in tmpWD.exListBookAuthors.SelectedItems)
            {
                var tmpName = (v as Item_ListCheckedBoxAuthor).Name;
                var tmpAuthor = DataProvider.Ins.DB.TACGIAs.FirstOrDefault(x => x.TenTacGia == tmpName);
                tmpBook.TACGIAs.Add(tmpAuthor);
            }
            foreach(var v in tmpWD.exListBookTypes.SelectedItems)
            {
                var tmpName = (v as Item_ListCheckedBoxType).Type;
                var tmpType = DataProvider.Ins.DB.THELOAIs.FirstOrDefault(x => x.TenTheLoai == tmpName);
                tmpBook.THELOAIs.Add(tmpType);
            }

            tmpBook.HinhAnhSach = BitmapSourceToByteArray(BookImage);

            DataProvider.Ins.DB.DAUSACHes.Add(tmpBook);
            DataProvider.Ins.DB.SaveChanges();
        }

        private byte[] BitmapSourceToByteArray(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
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
        public ICommand AddPictureCommand { get; set; }


        private string _BookName;
        private string _Type;
        private string _Author;
        private ObservableCollection<Item_ListCheckedBoxType> _ListTypes;
        private ObservableCollection<Item_ListCheckedBoxAuthor> _ListAuthors;
        private List<string> _ListSelectedType;
        private List<string> _ListSelectedAuthor;
        private int _FlagListNow;
        private BitmapImage _BookImage;

        public string BookName { get => _BookName; set { _BookName = value; OnPropertyChanged(); } }
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public string Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }
        public ObservableCollection<Item_ListCheckedBoxType> ListTypes { get => _ListTypes; set { _ListTypes = value; OnPropertyChanged(); } }

        public ObservableCollection<Item_ListCheckedBoxAuthor> ListAuthors { get => _ListAuthors; set { _ListAuthors = value; OnPropertyChanged(); } }
        public List<string> ListSelectedType { get => _ListSelectedType; set => _ListSelectedType = value; }
        public List<string> ListSelectedAuthor { get => _ListSelectedAuthor; set => _ListSelectedAuthor = value; }
        public int FlagListNow { get => _FlagListNow; set => _FlagListNow = value; }
        public BitmapImage BookImage { get => _BookImage; set { _BookImage = value; OnPropertyChanged(); } }
    }
}
