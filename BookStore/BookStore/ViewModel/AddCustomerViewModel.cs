using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        private void ConfirmButtonClick()
        {
            if(TenKhachHang == null || TenKhachHang == "" )
            {
                ErrorAddCustomer.Text = "Vui lòng nhập tên khách hàng!";
                return;
            }
            if(SDT==null||SDT=="")
            {
                ErrorAddCustomer.Text = "Vui lòng nhập số điện thoại!";
                return;
            }
            var tmp = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.HoTenKhachHang == TenKhachHang && x.DienThoai == SDT);
            if (tmp == null || tmp.Count() != 0)
            {
                ErrorAddCustomer.Text = "Khách hàng đã tồn tại!";
                return;
            }
            var kh = new KHACHHANG() { HoTenKhachHang = TenKhachHang, DienThoai = SDT, DiaChi = DiaChi, Email = Email, SoNo = 0 };
            DataProvider.Ins.DB.KHACHHANGs.Add(kh);
            DataProvider.Ins.DB.SaveChanges();
            ErrorAddCustomer.Text = "ID: " + kh.MaKhachHang.ToString();
        }

        private string _TenKhachHang;
        private string _SDT;
        private string _Email;
        private string _DiaChi;

        public ICommand LoadedCommand { get; set; }
        public ICommand ConfirmButtonClickCommand { get; set; }

        public TextBlock ErrorAddCustomer { get; set; }

        public string TenKhachHang { get => _TenKhachHang; set => _TenKhachHang = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
    }
}
