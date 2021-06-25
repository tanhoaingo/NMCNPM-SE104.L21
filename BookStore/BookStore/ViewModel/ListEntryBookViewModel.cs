using BookStore.Model;
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
    public class ListEntryBookViewModel : BaseViewModel
    {
        public ListEntryBookViewModel()
        {
            ListEntryBook = new ObservableCollection<PHIEUNHAPSACH>(DataProvider.Ins.DB.PHIEUNHAPSACHes);
            SelectedEntryBook = null;
            SelectionChangedCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) => { loadDetail(); });
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { loadEdit(); });
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
            BookEntryWindow bookEntryWindow = new BookEntryWindow();
            bookEntryWindow.ShowDialog();
        }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }

        private ObservableCollection<PHIEUNHAPSACH> _ListEntryBook;
        private ObservableCollection<CT_PNS> _Detail;
        private PHIEUNHAPSACH _SelectedEntryBook;

        public ObservableCollection<PHIEUNHAPSACH> ListEntryBook { get => _ListEntryBook; set => _ListEntryBook = value; }
        public ObservableCollection<CT_PNS> Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        public PHIEUNHAPSACH SelectedEntryBook { get => _SelectedEntryBook; set { _SelectedEntryBook = value; OnPropertyChanged(); } }
    }
}
