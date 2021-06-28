using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class ListCustomerViewModel: BaseViewModel
    {
        public ListCustomerViewModel()
        {
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);

            CreateBillButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CreateBill(); });
        }

        private void CreateBill()
        {
            throw new NotImplementedException();
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            SelectedItem = null;
            ListCustomer = null;
        }

        public ICommand CreateBillButtonClickCommand { get; set; }

        private KHACHHANG _SelectedItem;
        private ObservableCollection<KHACHHANG> _ListCustomer;

        public KHACHHANG SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }
    }
}
