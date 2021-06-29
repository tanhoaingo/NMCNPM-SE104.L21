using BookStore.Model;
using BookStore.Pages;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace BookStore.ViewModel
{
    public class ListBookViewModel : BaseViewModel
    {
        
        public ListBookViewModel()
        {

            LoadListBooks();
            #region Test
            /*Temp = new ObservableCollection<string>();
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");*/
            #endregion

            ListOptionsSearch = new ObservableCollection<string>();
            ListOptionsSearch.Add("Tất cả");
            ListOptionsSearch.Add("Tên sách");
            ListOptionsSearch.Add("Thể loại");
            ListOptionsSearch.Add("Tác giả");
            ListOptionsSearch.Add("Đơn giá");
            SelectedOption = "Tất cả";


            ListBooksCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListBooks);
            ListBooksCollectionView.Filter = FillerListBooks;

            OptionsSearchSelectionChangedCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { Refresh(p); });
            AddBookButtonClickCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { AddNewBook(p); });
            EditBookButtonClickCommand = new RelayCommand<Page>((p) => { return EditBookNeed(); }, (p) => { EditBook(p); });
            DeleteBookButtonClickCommand = new RelayCommand<Page>((p) => { return DeleteBookNeed(); }, (p) => { DeleteBook(p); });
            SearchTextChangedCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { Refresh(p); });

            SeeDetailCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { SeeDetail(p); });

            SelectBookCommand = new RelayCommand<Page>((p) => { return SelectedMBook != null; }, (p) => { AfterSelectedBook(p); });
            ExitSelectCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { CloseWindow(); });
        }

        private void CloseWindow()
        {
            LBWD.Close();
        }

        private void AfterSelectedBook(Page p)
        {
            LBWD.Close();

        }

        private void Refresh(Page p)
        {
            CollectionViewSource.GetDefaultView(ListBooks).Refresh();
        }
        private void test1()
        {
            MessageBox.Show("hong");
          
        }

        private bool DeleteBookNeed()
        {
            return SelectedMBook != null;
        }

        private void DeleteBook(Page p)
        {
            var tmp = DataProvider.Ins.DB.SACHes.Where(x => x.MaDauSach == SelectedMBook.MaDauSach);
            if (tmp.Count() > 0)
            {
                MessageBoxResult tmpRel = MessageBox.Show("Không thể xóa vì đã sử dụng dữ liệu đầu sách, bạn có muốn chuyển đầu sách vào lưu trữ \n" +
                    "(Những đầu sách lưu trữ sẽ không còn khả thi nhưng vẫn hiển thị đối với dữ liệu đã tạo)", "Thông báo",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (tmpRel == MessageBoxResult.OK)
                {
                    var tmpMBook = DataProvider.Ins.DB.DAUSACHes.Where(x => x.MaDauSach == SelectedMBook.MaDauSach).FirstOrDefault();
                    tmpMBook.TrangThai = 1;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListBooks();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBoxResult tmpRel = MessageBox.Show("Xác nhận xóa đầu sách", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (tmpRel == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.DAUSACHes.Remove(SelectedMBook);
                    SelectedMBook = null;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListBooks();
                }
                else
                {
                    return;
                }
            }
        }

        private bool EditBookNeed()
        {
            return SelectedMBook != null;
        }

        public void LoadListBooks()
        {
            if (FlagIntent == 0)
            {
                ListBooks = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.TrangThai == 0));
            }
            else
            {
                ListBooks = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.TrangThai == 0 && x.LuongTon > 0));
            }
        }

        public void LoadPage(BookPage p)
        {
            if(FlagIntent == 0)
            {
                p.IfShowListBook.Visibility = Visibility.Visible;
                p.IfSelectBook.Visibility = Visibility.Hidden;
                p.ListBookTitle.Text = "Danh sách đầu sách ";
            }
            else if(FlagIntent == 1)
            {
                p.IfShowListBook.Visibility = Visibility.Hidden;
                p.IfSelectBook.Visibility = Visibility.Visible;
                p.ListBookTitle.Text = "Tra cứu sách";
            }
        }

        private string GetAuthorsString(ICollection<TACGIA> tACGIAs)
        {
            string tmpauthor = "";
            foreach (var v in tACGIAs)
            {
                tmpauthor += (tmpauthor == "" ? "" : ", ") + v.TenTacGia;
            }
            return tmpauthor;
        }

        private string GetTypesString(ICollection<THELOAI> tHELOAIs)
        {
            string tmptype = "";
            foreach (var v in tHELOAIs)
            {
                tmptype += (tmptype == "" ? "" : ", ") + v.TenTheLoai;
            }
            return tmptype;
        }

        public decimal ConvertStringToDecimal(string intput)
        {
            decimal tmp = -100000;
            if (intput == null || intput == "")
            {
                return -100000;
            }

            if (decimal.TryParse(intput, out tmp))
            {
                return tmp;
            }
            return -1000000;
        }

        public string NonUnicode(string text)
        {
            string s = text.ToLower();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ"};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y"};
            for (int i = 0; i < arr1.Length; i++)
            {
                s = s.Replace(arr1[i], arr2[i]);
            }
            return s;
        }

        private bool FillerListBooks(object obj)
        {

            if (String.IsNullOrEmpty(ListBooksFiller))
            {
                return true;
            }

            if (obj is DAUSACH dAUSACH)
            {
                if (SelectedOption == "Đơn giá")
                {
                    var tmpInput = ConvertStringToDecimal(ListBooksFiller);
                    var tmpSach = DataProvider.Ins.DB.CT_PNS.Where(x => x.SACH.MaDauSach == dAUSACH.MaDauSach).ToList();
                    foreach (var v in tmpSach)
                    {
                        if ((v.DonGiaNhap*105/100 <= tmpInput + 20000) && (v.DonGiaNhap * 105 / 100 >= tmpInput - 20000))
                        {
                            return NonUnicode(dAUSACH.TenSach).IndexOf(NonUnicode(v.SACH.DAUSACH.TenSach), StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                    }
                }
                else if (SelectedOption == "Tên sách") return NonUnicode( dAUSACH.TenSach).IndexOf(NonUnicode(ListBooksFiller), StringComparison.OrdinalIgnoreCase) >= 0;
                else if (SelectedOption == "Thể loại") return NonUnicode(GetTypesString(dAUSACH.THELOAIs)).IndexOf(NonUnicode( ListBooksFiller), StringComparison.OrdinalIgnoreCase) >= 0;
                else if (SelectedOption == "Tác giả") return  NonUnicode(GetAuthorsString(dAUSACH.TACGIAs)).IndexOf(NonUnicode(ListBooksFiller) , StringComparison.OrdinalIgnoreCase) >= 0;
                else
                {
                    return NonUnicode(dAUSACH.TenSach).IndexOf(NonUnicode(ListBooksFiller), StringComparison.OrdinalIgnoreCase) >= 0 ||
                        NonUnicode(GetAuthorsString(dAUSACH.TACGIAs)).IndexOf(NonUnicode(ListBooksFiller), StringComparison.OrdinalIgnoreCase) >= 0 ||
                         NonUnicode(GetTypesString(dAUSACH.THELOAIs)).IndexOf(NonUnicode(ListBooksFiller), StringComparison.OrdinalIgnoreCase) >= 0 ||
                         FindByPrice(dAUSACH);
                }

            }
            return false;
        }

        private bool FindByPrice(DAUSACH dAUSACH)
        {
            var tmpInput = ConvertStringToDecimal(ListBooksFiller);
            var tmpSach = DataProvider.Ins.DB.CT_PNS.Where(x => x.SACH.MaDauSach == dAUSACH.MaDauSach).ToList();
            foreach (var v in tmpSach)
            {
                if ((v.DonGiaNhap * 105 / 100 <= tmpInput + 20000) && (v.DonGiaNhap * 105 / 100 >= tmpInput - 20000))
                {
                    return NonUnicode(dAUSACH.TenSach).IndexOf(NonUnicode(v.SACH.DAUSACH.TenSach), StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
            return false;
        }

        private void AddNewBook(Page p)
        {

            var tmpPg = p as BookPage ;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddBookWindow();
            var tmpVM = tmp.DataContext as AddBookViewModel;
            tmpVM.CleanUpData();
            tmpVM.FlagIntent = 0;
            tmp.ShowDialog();

            LoadListBooks();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }

        private void SeeDetail(Page p)
        {
            if(SelectedMBook == null)
            {
                MessageBox.Show("Chọn sách trước");
                return;
            }
            var tmpPg = p as BookPage;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new BookDetailWindow();
            var tmpVM = tmp.DataContext as BookDetailViewModel;
           

            foreach (var s in DataProvider.Ins.DB.SACHes)
            {
                if (s.MaDauSach == SelectedMBook.MaDauSach)
                {
                    Sach = s;
                    break;
                }
            }

            tmpVM.EditBook = SelectedMBook;
            tmpVM.LoadData();
            tmp.ShowDialog();

            LoadListBooks();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }
        private void  EditBook(Page p)
        {
            var tmpPg = p as BookPage;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddBookWindow();
            var tmpVM = tmp.DataContext as AddBookViewModel;
            tmpVM.CleanUpData();
            tmpVM.FlagIntent = 1;
            tmpVM.EditBook = SelectedMBook;
            tmp.ShowDialog();

            LoadListBooks();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }

        private void IsAllowedInput(TextCompositionEventArgs e)
        {

            if (SelectedOption == "Tên sách" || SelectedOption == "Thể loại" || SelectedOption == "Tác giả")
            {
               
            }
            else if (SelectedOption == "Đơn giá")
            {
                e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            SelectedMBook = null;
            LBWD = null;
            Temp = null;
        }


        public ICollectionView ListBooksCollectionView { get; set; }
        public ICommand OptionsSearchSelectionChangedCommand { get; set; }
        public ICommand AddBookButtonClickCommand { get; set; }
        public ICommand EditBookButtonClickCommand { get; set; }
        public ICommand DeleteBookButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }

        public ICommand SeeDetailCommand { get; set; }
         

        public ICommand SelectBookCommand { get; set; }
        public ICommand ExitSelectCommand { get; set; }


        private string _ListBooksFiller = string.Empty;
        private string _SelectedOption;
        private ObservableCollection<string> _ListOptionsSearch;
        private ObservableCollection<DAUSACH> _ListBooks;
        private ObservableCollection<string> _Temp;
        private int _FlagIntent;
        private DAUSACH _SelectedMBook;

        private SACH _sach;

        private ListBookWindow _LBWD;



        public string ListBooksFiller { get => _ListBooksFiller; set{ _ListBooksFiller = value; OnPropertyChanged(nameof(ListBooksFiller)); ListBooksCollectionView.Refresh(); } }
        public string SelectedOption { get => _SelectedOption; set { _SelectedOption = value; OnPropertyChanged(); } }
        public ObservableCollection<string> ListOptionsSearch { get => _ListOptionsSearch; set { _ListOptionsSearch = value; OnPropertyChanged(); } }

        public ObservableCollection<DAUSACH> ListBooks { get => _ListBooks; set { _ListBooks = value; OnPropertyChanged(); } }
        public ObservableCollection<string> Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }
        public int FlagIntent { get => _FlagIntent; set => _FlagIntent = value; }
        public DAUSACH SelectedMBook { get => _SelectedMBook; set => _SelectedMBook = value; }

        public SACH Sach { get => _sach; set => _sach = value; }

        public ListBookWindow LBWD { get => _LBWD; set => _LBWD = value; }

    }
}
