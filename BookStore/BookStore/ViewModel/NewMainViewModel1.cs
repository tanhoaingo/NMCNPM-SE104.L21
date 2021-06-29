﻿using BookStore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace BookStore.ViewModel
{
    class NewMainViewModel1 : BaseViewModel
    {

        public NewMainViewModel1()
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
            RuleCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_QuyDinh(p);
            });
            ListBillCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_DanhsachHoaDon(p);
            });
            ListBookEntryCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_DanhSachPhieuNhap(p);
            });
            ReportCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Btn_Report(p);
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
         private void Btn_QuyDinh(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/RulePage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Thay đổi quy định";
        }
          private void Btn_DanhSachPhieuNhap(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/ListBookEntryPage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Thay đổi quy định";
        }
          private void Btn_DanhsachHoaDon(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/ListInvoiceBillPage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Thay đổi quy định";
        }
        private void Btn_Report(Window p)
        {
            var tmpWd = p as NewMainWindow;
            tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/ProfitPage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Báo cáo tháng";
        }


        //IECommand
        public ICommand BtnInvoiceCommand { get; set; }
        public ICommand BookEntryCommand { get; set; }
        public ICommand SearchBookCommand { get; set; }
        public ICommand AuthorCommand { get; set; }
        public ICommand TypeBookCommand { get; set; }
        public ICommand RuleCommand { get; set; }
        public ICommand ListBillCommand { get; set; }
        public ICommand ListBookEntryCommand { get; set; }
        public ICommand mainSource { get; set; }
        public ICommand ReportCommand { get; set; }


        private void navigator()
        {
            foreach (Window w in Application.Current.Windows)
            {

            }
        }
    }
}