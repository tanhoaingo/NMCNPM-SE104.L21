using BookStore.Model;
using BookStore.Pages;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

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

        private ObservableCollection<PHIEUNHAPSACH> _ListEntryBook;
        private ObservableCollection<CT_PNS> _Detail;
        private PHIEUNHAPSACH _SelectedEntryBook;

        public ObservableCollection<PHIEUNHAPSACH> ListEntryBook { get => _ListEntryBook; set { _ListEntryBook = value; OnPropertyChanged(); } }
        public ObservableCollection<CT_PNS> Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        public PHIEUNHAPSACH SelectedEntryBook { get => _SelectedEntryBook; set { _SelectedEntryBook = value; OnPropertyChanged(); } }
    }
}
