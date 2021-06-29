using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace BookStore.ViewModel
{
    public class ListCustomerViewModel: BaseViewModel
    {
        public ListCustomerViewModel()
        {
            LoadListCustomer();
            
            CreateBillButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CreateBill(); });
            AddCustomerButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CreateCustomer(); });
            EditCustomerButtonClickCommand = new RelayCommand<object>((p) => { return EditCustomerNeed(); }, (p) => { EditCustomer(); });
            DeleteCustomerButtonClickCommand = new RelayCommand<object>((p) => { return DeleteCustomerNeed(); }, (p) => { DeleteCustomer(); });
        }

        private void DeleteCustomer()
        {
            var tmp = DataProvider.Ins.DB.HOADONs.Where(x => x.MaKhachHang == SelectedItem.MaKhachHang);
            if(tmp.Count() > 0)
            {
                MessageBoxResult tmpRel = MessageBox.Show("Không thể xóa vì khách hàng đã thực hiện hóa đơn, bạn có muốn chuyển khách hàng vào lưu trữ \n" +
                    "(Những khách hàng lưu trữ sẽ không còn khả thi nhưng vẫn hiển thị đối với hóa đơn đã lập)", "Thông báo",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(tmpRel == MessageBoxResult.OK)
                {
                    var tmpCus = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == SelectedItem.MaKhachHang).FirstOrDefault();
                    tmpCus.TrangThai = 1;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListCustomer();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBoxResult tmpRel = MessageBox.Show("Xác nhận xóa khách hàng", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (tmpRel == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedItem);
                    SelectedItem = null;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListCustomer();
                }
                else
                {
                    return;
                }
            }
        }

        private bool DeleteCustomerNeed()
        {
            return SelectedItem != null;
        }

        private void LoadListCustomer()
        {
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        }

        private bool EditCustomerNeed()
        {
            return SelectedItem != null;
        }

        private void EditCustomer()
        {
            var tmpWD = new AddCustomerWindow();
            var tmpVM = (tmpWD.DataContext as AddCustomerViewModel);
            tmpVM.FLagItent = 1;
            tmpVM.EditID = SelectedItem.MaKhachHang;
            tmpVM.TenKhachHang = SelectedItem.HoTenKhachHang;
            tmpVM.SDT = SelectedItem.DienThoai;
            tmpVM.DiaChi = SelectedItem.DiaChi;
            tmpVM.Email = SelectedItem.Email;
            tmpVM.EditSDT = SelectedItem.DienThoai;
            tmpWD.ShowDialog();
            LoadListCustomer();
        }

        private void CreateCustomer()
        {
            var tmpWD = new AddCustomerWindow();
            (tmpWD.DataContext as AddCustomerViewModel).FLagItent = 0;
            tmpWD.ShowDialog();
            LoadListCustomer();
        }

        private void CreateBill()
        {
            var tmpWD = new BillWindow();
            (tmpWD.DataContext as BillViewModel).SelectedCustomer = SelectedItem;
            tmpWD.ShowDialog();
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            SelectedItem = null;
            ListCustomer = null;
        }

        public ICommand CreateBillButtonClickCommand { get; set; }
        public ICommand AddCustomerButtonClickCommand { get; set; }
        public ICommand EditCustomerButtonClickCommand { get; set; }
        public ICommand DeleteCustomerButtonClickCommand { get; set; }

        private KHACHHANG _SelectedItem;
        private ObservableCollection<KHACHHANG> _ListCustomer;

        public KHACHHANG SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }
    }
}
