using BookStore.Model;
using BookStore.Pages;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace BookStore.ViewModel
{
    public class ListBookEntryViewModel : BaseViewModel
    {
        public ListBookEntryViewModel()
        {
            LoadData();
            SelectedEntryBook = null;
            SelectionChangedCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) => { loadDetail(); });
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { loadEdit(); });
            SeeDetailCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { SeeDetail(p); });
        }

        private void LoadData()
        {
            ListEntryBook = new ObservableCollection<PHIEUNHAPSACH>(DataProvider.Ins.DB.PHIEUNHAPSACHes);
        }

        public void loadDetail()
        {
            Detail = new ObservableCollection<CT_PNS>();
            foreach (var item in DataProvider.Ins.DB.CT_PNS)
            {
                if (item.MaPhieuNhapSach == SelectedEntryBook.MaPhieuNhapSach)
                {
                    Detail.Add(item);
                }
            }
        }
       
        
        private void SeeDetail(Page p)
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Chọn chi tiết hóa đơn trước");
                return;
            }
          
            Cdata = _selectedBook.SACH.DAUSACH;
            foreach( var s in DataProvider.Ins.DB.SACHes)
            {
                if (s.MaDauSach == Cdata.MaDauSach)
                {
                    Sach = s;
                    break;
                }
            }
            
            var tmpPg = p as ListBookEntryPage;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new BookDetailWindow();
            var tmpVM = tmp.DataContext as BookDetailViewModel;

            tmpVM.EditBook = Cdata;
            tmpVM.LoadData();
            tmp.ShowDialog();


            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }

        public void loadEdit()
        {
            if(SelectedEntryBook == null)
            {
                return;
            }    
            BookEntryWindow tmpWD = new BookEntryWindow();
            var tmpVM = tmpWD.DataContext as BookEntryViewModel;
            var tmpPg = tmpWD.PEPage as BookEntryPage;


            tmpVM.FlagIntent = 1;
            tmpVM.Editor = SelectedEntryBook;
            tmpVM.LoadData();
            tmpPg.AddDetailButton.IsEnabled = false;
            tmpPg.EditDetailButton.IsEnabled = false;
            tmpPg.BookNameComboBox.IsEnabled = false;
            tmpPg.BookTypeTextBox.IsEnabled = false;
            tmpPg.BookAuthorTextBox.IsEnabled = false;
            tmpPg.AmountTextBox.IsEnabled = false;
            tmpPg.InputPriceTextBox.IsEnabled = false;
            tmpPg.IntoMoneyTextBox.IsEnabled = false;
            tmpWD.ShowDialog();
            loadDetail();
            LoadData();
            
        }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }
        public ICommand SeeDetailCommand { get; set; }

        private ObservableCollection<PHIEUNHAPSACH> _ListEntryBook;
        private ObservableCollection<CT_PNS> _Detail;
        private PHIEUNHAPSACH _SelectedEntryBook;
        private CT_PNS _selectedBook;

        public ObservableCollection<PHIEUNHAPSACH> ListEntryBook { get => _ListEntryBook; set { _ListEntryBook = value; OnPropertyChanged(); } }
        public ObservableCollection<CT_PNS> Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        public PHIEUNHAPSACH SelectedEntryBook { get => _SelectedEntryBook; set { _SelectedEntryBook = value; OnPropertyChanged(); } }
        public CT_PNS SelectedBook { get => _selectedBook; set { _selectedBook = value; OnPropertyChanged(); } }

        private DAUSACH _cData;
        public DAUSACH Cdata { get => _cData; set  { _cData = value; OnPropertyChanged(); } }
        private SACH _sach;
        public SACH Sach { get => _sach; set { _sach = value; OnPropertyChanged(); } }
    }
}
