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
            ListBooks = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.LuongTon > 0));
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


            ListBooksCollectionView = CollectionViewSource.GetDefaultView(ListBooks);
            ListBooksCollectionView.Filter = FillerListBooks;

            OptionsSearchSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { });
            AddBookButtonClickCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { AddNewBook(p); });
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
            decimal tmp = 0;
            if (intput == null || intput == "")
            {
                return 0;
            }
            
            decimal.TryParse(intput,out tmp);
            return tmp;
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
                            return dAUSACH.TenSach.IndexOf(v.SACH.DAUSACH.TenSach, StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                    }
                }
                else if (SelectedOption == "Tên sách") return dAUSACH.TenSach.IndexOf(ListBooksFiller, StringComparison.OrdinalIgnoreCase) >= 0;
                else if (SelectedOption == "Thể loại") return GetTypesString(dAUSACH.THELOAIs).IndexOf(ListBooksFiller, StringComparison.OrdinalIgnoreCase) >= 0;
                else if (SelectedOption == "Tác giả") return GetAuthorsString(dAUSACH.TACGIAs).IndexOf(ListBooksFiller, StringComparison.OrdinalIgnoreCase) >= 0;
                else
                {
                    return dAUSACH.TenSach.IndexOf(ListBooksFiller, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        GetAuthorsString(dAUSACH.TACGIAs).IndexOf(ListBooksFiller, StringComparison.OrdinalIgnoreCase) >= 0 ||
                         GetTypesString(dAUSACH.THELOAIs).IndexOf(ListBooksFiller, StringComparison.OrdinalIgnoreCase) >= 0 ||
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
                    return dAUSACH.TenSach.IndexOf(v.SACH.DAUSACH.TenSach, StringComparison.OrdinalIgnoreCase) >= 0;
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
            tmp.ShowDialog();

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



        public ICollectionView ListBooksCollectionView { get; set; }
        public ICommand OptionsSearchSelectionChangedCommand { get; set; }
        public ICommand AddBookButtonClickCommand { get; set; }

        private string _ListBooksFiller = string.Empty;
        private string _SelectedOption;
        private ObservableCollection<string> _ListOptionsSearch;
        private ObservableCollection<DAUSACH> _ListBooks;
        private ObservableCollection<string> _temp;

        public string ListBooksFiller { get => _ListBooksFiller; set{ _ListBooksFiller = value; OnPropertyChanged(nameof(ListBooksFiller)); ListBooksCollectionView.Refresh(); } }
        public string SelectedOption { get => _SelectedOption; set { _SelectedOption = value; OnPropertyChanged(); } }
        public ObservableCollection<string> ListOptionsSearch { get => _ListOptionsSearch; set { _ListOptionsSearch = value; OnPropertyChanged(); } }
        public ObservableCollection<DAUSACH> ListBooks { get => _ListBooks; set => _ListBooks = value; }
        public ObservableCollection<string> Temp { get => _temp; set => _temp = value; }
    }
}
