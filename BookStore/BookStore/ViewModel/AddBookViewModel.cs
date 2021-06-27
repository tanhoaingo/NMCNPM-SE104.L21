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
            ListSelectedAuthor = new List<string>();
            ListSelectedType = new List<string>();
            FlagIsSaved = false;

            LoadListTypes();
            LoadListAuthor();
            
                
            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { this.CleanUpData(); });
            ConfirmButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ConfirmAddBook(p); });
            BookNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { BookNameChanged(p); });
            TypeTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p); TextListBookTypesChanged(); });
            AuthorTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { HiddenErrorTextBlock(p); TextListBookAuthorsChanged(); });
            ShowListBookTypesCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ShowListBookTypes(p); });
            ShowListBookAuthorsCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ShowListBookAuthors(p); });
            ItemTypeChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemTypeChecked(p); });
            ItemAuthorChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemAuthorChecked(p); });
            ItemTypeUnChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemTypeChecked(p); });
            ItemAuthorUnChekedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ItemAuthorChecked(p); });
            AddListClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddToList(p); });
            AddPictureCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddPicture(p); });

            BookImage = new BitmapImage(new Uri(@"/BookStore;component/Source/Image/bookInsert.jpg", UriKind.Relative));

            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelAddBook(p); });
        }

        private void BookNameChanged(Window p)
        {
            HiddenErrorTextBlock(p); 
            HiddenSomething(p);
        }

        private void CancelAddBook(Window p)
        {
            if (FlagIsSaved || (string.IsNullOrEmpty(BookName) && BookImage == null && (ListSelectedAuthor.Count() == 0) && (ListSelectedType.Count() == 0)))
            {
                p.Close();
            }
            else
            {
                var result = MessageBox.Show("Bạn có muốn lưu thay đổi? ", "Thông báo", MessageBoxButton.YesNoCancel, MessageBoxImage.Error, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmAddBook(p);
                    p.Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    p.Close();
                }
                else
                {
                    return;
                }
            }    
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

        private void AddToList(Window p)
        {
            var tmpWD = p as AddBookWindow;
            if (FlagListNow == 2)
            {
                var tmp = new AddAuthorWindow();
                tmp.AuthorText.Focus();
                tmp.ShowDialog();
                LoadListAuthor();
                ReUpdateSelectedAuthor(p);
                var tmpindex = ListAuthors.IndexOf(ListAuthors.Where(x => x.Name == (tmp.DataContext as AddAuthorViewModel).AuthorName).FirstOrDefault());
                if (tmpindex != -1)
                {
                    tmpWD.exListBookAuthors.SelectedItems.Add(tmpWD.exListBookAuthors.Items.GetItemAt(tmpindex));
                }
                ItemAuthorChecked(tmpWD);
                
            }
            else if (FlagListNow == 1)
            {
                var tmp = new AddTypeWindow();
                tmp.TypeText.Focus();
                tmp.ShowDialog();
                LoadListTypes();
                ReUpdateSelectedType(p);
                var tmpindex = ListTypes.IndexOf(ListTypes.Where(x => x.Type == (tmp.DataContext as AddTypeViewModel).TypeName).FirstOrDefault());
                if(tmpindex !=-1)
                {
                    tmpWD.exListBookTypes.SelectedItems.Add(tmpWD.exListBookTypes.Items.GetItemAt(tmpindex));
                }
                ItemTypeChecked(tmpWD);
            }
            else
            {
                return;
            }
        }

        private void ReUpdateSelectedType(Window p)
        {
            var tmpWD = p as AddBookWindow;
            foreach (var v in ListSelectedType)
            {
                var tmpindex = ListTypes.IndexOf(ListTypes.Where(x => x.Type == v).FirstOrDefault());
                tmpWD.exListBookTypes.SelectedItems.Add(tmpWD.exListBookTypes.Items.GetItemAt(tmpindex));
            }
        }

        private void ReUpdateSelectedAuthor(Window p)
        {
            var tmpWD = p as AddBookWindow;
            foreach (var v in ListSelectedAuthor)
            {
                var tmpindex = ListAuthors.IndexOf(ListAuthors.Where(x => x.Name == v).FirstOrDefault());
                tmpWD.exListBookAuthors.SelectedItems.Add(tmpWD.exListBookAuthors.Items.GetItemAt(tmpindex));
            }
        }

        private void ItemAuthorChecked(Window p)
        {
            ListSelectedAuthor = new List<string>();
            var tmp = p as AddBookWindow;
            Author = "";
            foreach (var v in tmp.exListBookAuthors.SelectedItems)
            {
                Author += (Author == "") ? "" : ", ";
                Author += (v as Item_ListCheckedBoxAuthor).Name;
                ListSelectedAuthor.Add((v as Item_ListCheckedBoxAuthor).Name);
            }
        }

        private void ItemTypeChecked(Window p)
        {
            ListSelectedType = new List<string>();
            var tmp = p as AddBookWindow;
            Type = "";
            foreach (var v in tmp.exListBookTypes.SelectedItems)
            {
                Type += (Type == "") ? "" : ", ";
                Type += (v as Item_ListCheckedBoxType).Type;
                ListSelectedType.Add((v as Item_ListCheckedBoxType).Type);
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

            if(!IsBookExist(tmpBook))
            {
                tmpBook.HinhAnhSach = BitmapSourceToByteArray(BookImage);

                DataProvider.Ins.DB.DAUSACHes.Add(tmpBook);
                DataProvider.Ins.DB.SaveChanges();

                tmpWD.ErrorAddBook.Text = "Thêm sách thành công!";
                tmpWD.ErrorAddBook.Foreground = Brushes.Green;
                tmpWD.ErrorAddBook.Visibility = Visibility.Visible;
                FlagIsSaved = true;
            }
            else
            {
                tmpWD.ErrorAddBook.Text = "Sách đã tồn tại!";
                tmpWD.ErrorAddBook.Foreground = Brushes.Red;
                tmpWD.ErrorAddBook.Visibility = Visibility.Visible;
            }

            
        }

        private bool IsBookExist(DAUSACH tmpBook)
        {
            foreach (var v in DataProvider.Ins.DB.DAUSACHes)
            {
                var tmpCount = 0;
                if (v.TenSach == tmpBook.TenSach)
                {
                    tmpCount++;
                }
                if (Enumerable.SequenceEqual(v.TACGIAs.OrderBy(t => t.TenTacGia), tmpBook.TACGIAs.OrderBy(t => t.TenTacGia)))
                {
                    tmpCount++;
                }
                if (Enumerable.SequenceEqual(v.THELOAIs.OrderBy(t => t.TenTheLoai), tmpBook.THELOAIs.OrderBy(t => t.TenTheLoai)))
                {
                    tmpCount++;
                }
                if(tmpCount == 3)
                {
                    return true;
                }
            }
            return false;
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

        public override void CleanUpData()
        {
            base.CleanUpData();
            BookName = string.Empty;
            Type = string.Empty;
            Author = string.Empty;
            ListTypes = new ObservableCollection<Item_ListCheckedBoxType>();
            ListAuthors = new ObservableCollection<Item_ListCheckedBoxAuthor>();
            ListSelectedAuthor = new List<string>();
            ListSelectedType = new List<string>();
            FlagListNow = 0;
            BookImage = null;
            FlagIsSaved = false;
        }

        public ICommand CloseWindowCommand { get; set; }
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
        public ICommand CancelButtonClickCommand { get; set; }


        private string _BookName;
        private string _Type;
        private string _Author;
        private ObservableCollection<Item_ListCheckedBoxType> _ListTypes;
        private ObservableCollection<Item_ListCheckedBoxAuthor> _ListAuthors;
        private List<string> _ListSelectedType;
        private List<string> _ListSelectedAuthor;
        private int _FlagListNow;
        private BitmapImage _BookImage;
        private bool _FlagIsSaved;

        public string BookName { get => _BookName; set { _BookName = value; OnPropertyChanged(); } }
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public string Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }
        public ObservableCollection<Item_ListCheckedBoxType> ListTypes { get => _ListTypes; set { _ListTypes = value; OnPropertyChanged(); } }

        public ObservableCollection<Item_ListCheckedBoxAuthor> ListAuthors { get => _ListAuthors; set { _ListAuthors = value; OnPropertyChanged(); } }
        public List<string> ListSelectedType { get => _ListSelectedType; set => _ListSelectedType = value; }
        public List<string> ListSelectedAuthor { get => _ListSelectedAuthor; set => _ListSelectedAuthor = value; }
        public int FlagListNow { get => _FlagListNow; set => _FlagListNow = value; }
        public BitmapImage BookImage { get => _BookImage; set { _BookImage = value; OnPropertyChanged(); } }

        public bool FlagIsSaved { get => _FlagIsSaved; set => _FlagIsSaved = value; }
    }
}
