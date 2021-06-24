using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BookStore.Model;
using System.Windows.Controls;
using System.Windows.Input;
using BookStore.Tools;
using System.Windows;
using BookStore.Pages;

namespace BookStore.ViewModel
{
    public class ListEntryBookVM : BaseViewModel
    {
        private ObservableCollection<PHIEUNHAPSACH> _ListEntryBook;
        public ObservableCollection<PHIEUNHAPSACH> ListEntryBook { get => _ListEntryBook; set => _ListEntryBook = value; }
        private ObservableCollection<CT_PNS> _Detail;
        public ObservableCollection<CT_PNS> Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        private PHIEUNHAPSACH _SelectedEntryBook;
        public PHIEUNHAPSACH SelectedEntryBook { get => _SelectedEntryBook; set { _SelectedEntryBook = value; OnPropertyChanged(); } }

        public ListEntryBookVM()
        {
            ListEntryBook = new ObservableCollection<PHIEUNHAPSACH>(DataProvider.Ins.DB.PHIEUNHAPSACHes);           
            SelectedEntryBook = null;
            SelectionChangedCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) => { loadDetail(); });
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { loadEdit(); });
        }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }
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
            ThayDoiWindow window = new ThayDoiWindow();
            window.ShowDialog();
        }
    }
}
