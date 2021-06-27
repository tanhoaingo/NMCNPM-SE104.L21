using BookStore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace BookStore.ViewModel
{
    class NewMainViewModel :BaseViewModel
    {

        public NewMainViewModel()
        {
            BtnInvoiceCommand = new RelayCommand<Window>((p) => { return true; }, (p) => 
             {
                 Btn_hoaDon(p);
             });

            BookEntryCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_PhieuNhap(p);
            });

            AuthorCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_TacGia(p);
            });
            SearchBookCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_NhapSach(p);
            });

            TypeBookCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_LoaiSach(p);
            });


        }
        private void Btn_PhieuNhap(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/BookEntryPage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Phiếu nhập sách";
        }
        private void Btn_hoaDon(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/InvoicePage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Hóa đơn bán sách";
        }
        private void Btn_NhapSach(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/BookPage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Tìm kiếm sách";
        }
        private void Btn_TacGia(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/AuthorPage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Danh sách dữ liệu tác giả";
        }
        private void Btn_LoaiSach(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/BookCategory.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Danh sách dữ liệu loại sách";
        }


        //IECommand
        public ICommand BtnInvoiceCommand { get; set; } 
        public ICommand BookEntryCommand { get; set; } 
        public ICommand SearchBookCommand { get; set; } 
        public ICommand AuthorCommand { get; set; } 
        public ICommand TypeBookCommand { get; set; } 
        public ICommand mainSource { get; set; } 
        

        private void navigator()
        {
            foreach(Window w in Application.Current.Windows)
            {
               
            }
        }
    }
}
