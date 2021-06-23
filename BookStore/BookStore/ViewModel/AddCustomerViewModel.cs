using BookStore.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class AddCustomerViewModel : BaseViewModel
    {

        public AddCustomerViewModel()
        {
            FlagIsSaved = false;

            ConfirmButtonClickCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { ConfirmAddCustomer(p); });
            NameTextChangedCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(p); });
            PhoneTextChangedCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(p); });
            EmailTextChangedCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(p); });
            AddressTextChangedCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(p); });
            CancelButtonClickCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { CancelAddCustomer(p); });
        }

        private void CancelAddCustomer(AddCustomerWindow p)
        {
            if (FlagIsSaved || (string.IsNullOrEmpty(TenKhachHang) && string.IsNullOrEmpty(SDT) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(DiaChi)))
            {
                p.Close();
            }
            else
            {
                var result = MessageBox.Show("Bạn có muốn lưu thay đổi? ", "Thông báo", MessageBoxButton.YesNoCancel, MessageBoxImage.Error, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmAddCustomer(p);
                    p.Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    p.Close();
                }
                else
                {
                    return;
                }
            }
        }

        private void HiddenErrorAddCustomer(AddCustomerWindow p)
        {
            p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ConfirmAddCustomer(AddCustomerWindow p)
        {
            if (TenKhachHang == null || TenKhachHang == "")
            {
                p.ErrorAddCustomer.Text = "Vui lòng nhập tên khách hàng!";
                p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            if (SDT == null || SDT == "")
            {
                p.ErrorAddCustomer.Text = "Vui lòng nhập số điện thoại!";
                p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            var tmp = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.HoTenKhachHang == TenKhachHang && x.DienThoai == SDT);
            if (tmp == null || tmp.Count() != 0)
            {
                p.ErrorAddCustomer.Text = "Khách hàng đã tồn tại!";
                p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            var kh = new KHACHHANG() { HoTenKhachHang = TenKhachHang, DienThoai = SDT, DiaChi = DiaChi, Email = Email, SoNo = 0 };
            DataProvider.Ins.DB.KHACHHANGs.Add(kh);
            DataProvider.Ins.DB.SaveChanges();
            p.ErrorAddCustomer.Text = "Thêm khách hàng thành công!";
            p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
            _FlagIsSaved = true;
        }

        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand NameTextChangedCommand { get; set; }
        public ICommand PhoneTextChangedCommand { get; set; }
        public ICommand EmailTextChangedCommand { get; set; }
        public ICommand AddressTextChangedCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private string _TenKhachHang;
        private string _SDT;
        private string _Email;
        private string _DiaChi;
        private bool _FlagIsSaved;

        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        public bool FlagIsSaved { get => _FlagIsSaved; set => _FlagIsSaved = value; }
    }
}
