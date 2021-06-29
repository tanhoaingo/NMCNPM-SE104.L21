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
            LoadWindowCommand = new RelayCommand<AddCustomerWindow>((p) => { return true; }, (p) => { LoadWindow(p); });
        }


        private void LoadWindow(AddCustomerWindow p)
        {
            p.TextTitle.Text = "Sửa thông tin khách hàng";
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
            if (FLagItent == 0)
            {
                var tmp = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DienThoai == SDT);
                if (tmp == null || tmp.Count() != 0)
                {
                    p.ErrorAddCustomer.Text = "Số điện thoại đã tồn tại!";
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
            else if (FLagItent == 1)
            {
                if(SDT != EditSDT)
                {
                    var tmp = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DienThoai == SDT);
                    if (tmp == null || tmp.Count() != 0)
                    {
                        p.ErrorAddCustomer.Text = "Số điện thoại thuộc về khách hàng khác!";
                        p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
                        return;
                    }
                }
                var tmpkh = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == EditID).FirstOrDefault();
                tmpkh.HoTenKhachHang = TenKhachHang;
                tmpkh.DiaChi = DiaChi;
                tmpkh.DienThoai = SDT;
                tmpkh.Email = Email;
                p.ErrorAddCustomer.Text = "Sửa thông tin khác hàng thành công!";
                p.ErrorAddCustomer.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand NameTextChangedCommand { get; set; }
        public ICommand PhoneTextChangedCommand { get; set; }
        public ICommand EmailTextChangedCommand { get; set; }
        public ICommand AddressTextChangedCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }

        private string _TenKhachHang;
        private string _SDT;
        private string _Email;
        private string _DiaChi;
        private bool _FlagIsSaved;
        private int _FLagItent;
        private int _EditID;
        private string _EditSDT;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        public bool FlagIsSaved { get => _FlagIsSaved; set => _FlagIsSaved = value; }
        public int FLagItent { get => _FLagItent; set => _FLagItent = value; }
        public int EditID { get => _EditID; set => _EditID = value; }
        public string EditSDT { get => _EditSDT; set => _EditSDT = value; }
    }
}
