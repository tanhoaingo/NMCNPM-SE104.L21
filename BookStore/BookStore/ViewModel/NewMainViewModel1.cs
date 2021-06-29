using BookStore.Model;
using BookStore.Pages;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace BookStore.ViewModel
{
    class NewMainViewModel1 : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public NewMainViewModel1()
        {

            LogoutCommand = new RelayCommand<OfficialMainWindow>(p => true, p => {
                Isloaded = false;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                var loginVM = loginWindow.DataContext as LoginViewModel;
                loginVM.IsLogin = false;
                loginWindow.ShowDialog();
                if (loginVM.IsLogin)
                {
                    p.Show();
                    string foreFocus = "#FFFFFF";
                    string backFocus = "#6485FF";
                    var tmpP = new InvoicePage();
                    p.btnHoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    p.btnHoaDon.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);
                    p.MainFrame.Content = tmpP;
                    p.MainTitle.Text = "Hóa đơn bán sách";
                    (tmpP.DataContext as InvoiceViewModel).Staff = User.Ins.nguoiDung;

                    if (User.Ins.nguoiDung.NHOMNGUOIDUNG.TenNhom == "Nhân Viên")
                    {
                        p.btnQuyDinh.IsEnabled = false;
                        p.btnDShoaDon.IsEnabled = false;

                    }
                }
                else
                {

                    p.Close();
                }
            });

            LoadedWindowCommand = new RelayCommand<OfficialMainWindow>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                {
                    return;
                }
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    string foreFocus = "#FFFFFF";
                    string backFocus = "#6485FF";
                    var tmpP = new InvoicePage();
                    p.btnHoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    p.btnHoaDon.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);
                    p.MainFrame.Content = tmpP;
                    p.MainTitle.Text = "Hóa đơn bán sách";
                    (tmpP.DataContext as InvoiceViewModel).Staff = User.Ins.nguoiDung;

                    if(User.Ins.nguoiDung.NHOMNGUOIDUNG.TenNhom == "Nhân Viên")
                    {
                        p.btnQuyDinh.IsEnabled = false;
                        p.btnDShoaDon.IsEnabled = false;
                        
                    }
                }
                else
                {

                    p.Close();
                }
            }
                );
            MouseLeaveCommand = new RelayCommand<Button>(p => true, p => MouseLeave(p));
            GetUidCommand = new RelayCommand<Button>(p => true, p => { _uid = p.Uid; Hover(p); });
            BtnCommand = new RelayCommand<OfficialMainWindow>(p => true, p => Btn_Click(p));
        }

        void MouseLeave(Button btn)
        {
            if (btn.IsFocused)
            {
                return;
            }
            string foreground = "#6485FF";
            string background = "#FFFFFF";
            btn.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            btn.Background = (Brush)new BrushConverter().ConvertFrom(background);
        }
        public void Btn_Click(OfficialMainWindow window)
        {
            int index = int.Parse(Uid);


            string foreground = "#6485FF";
            string background = "#FFFFFF";
            string foreFocus = "#FFFFFF";
            string backFocus = "#6485FF";

            window.btnHoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnPhieuNhap.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnDauSach.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnTacGia.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnTheLoai.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnDSKhachHang.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.BtnDsPhieuNhap.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnDShoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnDoanhThu.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnQuyDinh.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnBaoCaoTon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnDSHoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);
            window.btnDSPhieuThu.Foreground = (Brush)new BrushConverter().ConvertFrom(foreground);

            window.btnHoaDon.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnPhieuNhap.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnDauSach.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnTacGia.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnTheLoai.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnDSKhachHang.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.BtnDsPhieuNhap.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnDShoaDon.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnDoanhThu.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnQuyDinh.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnBaoCaoTon.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnDSHoaDon.Background = (Brush)new BrushConverter().ConvertFrom(background);
            window.btnDSPhieuThu.Background = (Brush)new BrushConverter().ConvertFrom(background);

            var tmpWd = window as OfficialMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;

            tmpWd.MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            switch (index)
            {
                case 0:
                    var tmpP = new InvoicePage();
                    window.btnHoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnHoaDon.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);
                    (tmpP.DataContext as InvoiceViewModel).LoadListBook();
                    (tmpP.DataContext as InvoiceViewModel).Staff = User.Ins.nguoiDung;
                    tmpWd.MainFrame.Content = tmpP;
                    tmpWd.MainTitle.Text = "Hóa đơn bán sách";
                    break;

                case 1:
                    var tmp2P = new BookEntryPage();
                    window.btnPhieuNhap.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnPhieuNhap.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);
                    tmpWd.MainFrame.Content = tmp2P;
                    tmpWd.MainTitle.Text = "Phiếu nhập sách";
                    (tmp2P.DataContext as BookEntryViewModel).Staff = User.Ins.nguoiDung;
                    break;
                case 2:

                    window.btnDauSach.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnDauSach.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);


                    
                    tmpWd.MainFrame.Navigate(new Uri("../Pages/BookPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Tìm kiếm sách";
                    break;

                case 3:

                    window.btnTacGia.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnTacGia.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/AuthorPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Danh sách dữ liệu tác giả";
                    break;
                case 4:

                    window.btnTheLoai.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnTheLoai.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/BookCategory.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Danh sách dữ liệu loại sách";
                    break;
                case 5:

                    window.btnDSKhachHang.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnDSKhachHang.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/ListCustomerPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Danh sách khách hàng";
                    break;
                case 6:
                    window.BtnDsPhieuNhap.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.BtnDsPhieuNhap.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

            tmpWd.MainFrame.Navigate(new Uri("../Pages/ListBookEntryPage.xaml", UriKind.Relative));

            tmpWd.MainTitle.Text = "Danh sách phiếu nhập sách";
                    break;

                case 7:

                    window.btnDShoaDon.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);
                    window.btnDShoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/ListUserPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Danh sách Người dùng";
                    break;
                case 8:

                    window.btnDoanhThu.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnDoanhThu.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/ProfitPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Báo cáo Doanh thu";
                    break;
                case 9:

                    window.btnQuyDinh.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnQuyDinh.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/RulePage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Thay đổi quy định";
                    break;

                case 10:

                    window.btnBaoCaoTon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnBaoCaoTon.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/ReportPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Báo cáo tồn và công nợ";
                    break;


                case 15:

                    window.btnDSHoaDon.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnDSHoaDon.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/ListInvoiceBillPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Danh sách hóa đơn";
                    break;


                case 16:

                    window.btnDSPhieuThu.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
                    window.btnDSPhieuThu.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);

                    tmpWd.MainFrame.Navigate(new Uri("../Pages/ListInvoiceBillPage.xaml", UriKind.Relative));
                    tmpWd.MainTitle.Text = "Danh sách phiếu thu";
                    break;
                default:
                    break;


            }

        }

        void Hover(Button btn)
        {
            string foreFocus = "#FFFFFF";
            string backFocus = "#6485FF";
            btn.Foreground = (Brush)new BrushConverter().ConvertFrom(foreFocus);
            btn.Background = (Brush)new BrushConverter().ConvertFrom(backFocus);
        }
        
         
     


       
        public ICommand mainSource { get; set; }
        public ICommand ReportCommand { get; set; }

        public ICommand MouseLeaveCommand { get; set; }
        public ICommand BtnCommand { get; set; }

        public ICommand GetUidCommand { get; set; }
        private string _uid;
        public string Uid { get => _uid; set => _uid = value; }

       
    }
}