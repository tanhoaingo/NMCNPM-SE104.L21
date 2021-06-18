using BookStore.Model;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class AddCustomerViewModel : BaseViewModel
    {

        public AddCustomerViewModel()
        {
            LoadedCommand = new RelayCommand<TextBlock>((p) => { return true; }, (p) => { ErrorAddCustomer = (TextBlock)p; });
            ConfirmButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ConfirmButtonClick(); });
            NameTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(); });
            PhoneTextChangedCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(); });
            EmailTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(); });
            AddressTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HiddenErrorAddCustomer(); });

        }

        private void HiddenErrorAddCustomer()
        {
            ErrorAddCustomer.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ConfirmButtonClick()
        {
            if (TenKhachHang == null || TenKhachHang == "")
            {
                ErrorAddCustomer.Text = "Vui lòng nhập tên khách hàng!";
                ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            if (SDT == null || SDT == "")
            {
                ErrorAddCustomer.Text = "Vui lòng nhập số điện thoại!";
                ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            var tmp = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.HoTenKhachHang == TenKhachHang && x.DienThoai == SDT);
            if (tmp == null || tmp.Count() != 0)
            {
                ErrorAddCustomer.Text = "Khách hàng đã tồn tại!";
                ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            var kh = new KHACHHANG() { HoTenKhachHang = TenKhachHang, DienThoai = SDT, DiaChi = DiaChi, Email = Email, SoNo = 0 };
            DataProvider.Ins.DB.KHACHHANGs.Add(kh);
            DataProvider.Ins.DB.SaveChanges();
            ErrorAddCustomer.Text = "Thêm khách hàng thành công!";
            ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
        }


        public ICommand LoadedCommand { get; set; }
        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand NameTextChangedCommand { get; set; }
        public ICommand PhoneTextChangedCommand { get; set; }
        public ICommand EmailTextChangedCommand { get; set; }
        public ICommand AddressTextChangedCommand { get; set; }


        public TextBlock ErrorAddCustomer { get; set; }


        private string _TenKhachHang;
        private string _SDT;
        private string _Email;
        private string _DiaChi;


        public string TenKhachHang { get => _TenKhachHang; set => _TenKhachHang = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
    }
}
