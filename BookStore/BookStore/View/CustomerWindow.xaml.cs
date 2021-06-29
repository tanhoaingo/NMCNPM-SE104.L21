using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore.View
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public KHACHHANG khachHang { get; set; }
        public CustomerWindow(KHACHHANG kh)
        {
            khachHang = kh;
            InitializeComponent();
        }

        private void AddCustomerWD_Loaded(object sender, RoutedEventArgs e)
        {
            ten.Text = khachHang.HoTenKhachHang;
            diachi.Text = khachHang.DiaChi;
            email.Text = khachHang.Email;
            sdt.Text = khachHang.DienThoai;
            soNo.Text = khachHang.SoNo.ToString();
        }
    }
}
