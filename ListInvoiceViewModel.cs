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
    public class ListInvoiceViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _ListInvoice;
        public ObservableCollection<HOADON> ListInvoice { get => _ListInvoice; set => _ListInvoice = value; }
        private ObservableCollection<CT_HD> _Detail;
        public ObservableCollection<CT_HD> Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        private HOADON _SelectedInvoice;
        public HOADON SelectedInvoice { get => _SelectedInvoice; set { _SelectedInvoice = value; OnPropertyChanged(); } }

        public ListInvoiceViewModel()
        {
            ListInvoice = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SelectedInvoice = null;
            SelectionChangedCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) => { loadDetail(); });
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { loadEdit(); });
        }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }
        public void loadDetail()
        {

            Detail = new ObservableCollection<CT_HD>();
            foreach (var item in DataProvider.Ins.DB.CT_HD)
            {
                if (item.MaHoaDon == SelectedInvoice.MaHoaDon)
                {
                    Detail.Add(item);
                }
            }
        }
        public void loadEdit()
        {
            
        }
    }
}
