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
    public class ListInvoiceViewModel : BaseViewModel
    {
        public ListInvoiceViewModel()
        {
            ListInvoice = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SelectedInvoice = null;
            SelectionChangedCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) => { loadDetail(); });
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { loadEdit(); });
        }

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
            InvoiceWindow invoiceWindow = new InvoiceWindow();
            invoiceWindow.ShowDialog();
        }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }

        private ObservableCollection<HOADON> _ListInvoice;
        private ObservableCollection<CT_HD> _Detail;
        private HOADON _SelectedInvoice;

        public ObservableCollection<HOADON> ListInvoice { get => _ListInvoice; set => _ListInvoice = value; }
        public ObservableCollection<CT_HD> Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        public HOADON SelectedInvoice { get => _SelectedInvoice; set { _SelectedInvoice = value; OnPropertyChanged(); } }
    }
}
