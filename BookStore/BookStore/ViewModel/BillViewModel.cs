using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BookStore.Model;
using System.Windows.Controls;
using System.Windows.Input;
using BookStore.Tools;
using System.Windows;

namespace BookStore.ViewModel
{

    public class BillViewModel : BaseViewModel
    {
        public BillViewModel()
        {
            PayDate = DateTime.Now;
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);

            CloseWindowCommand = new RelayCommand<BillWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });
            NameCustomerSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { });
            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveInvoice(); });
            CancelButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { CancelInvoice(); });
        }

        void SaveInvoice()
        {
            bool IsComplete = false;
            if (SelectedCustomer != null)
            {
                foreach (var item in DataProvider.Ins.DB.KHACHHANGs)
                {
                    if (item.MaKhachHang == SelectedCustomer.MaKhachHang)
                    {
                        if (PaidAmount <= 0)
                            MessageBox.Show("Vui lòng nhập số tiền trả !");
                        else if (item.SoNo < PaidAmount)
                            MessageBox.Show("Số tiền trả không vượt quá số tiền nợ !");
                        else
                        {
                            item.SoNo = item.SoNo - PaidAmount;
                            IsComplete = true;
                        }
                        break;
                    }
                }
                if (IsComplete)
                { 
                    var phieuThuTien = new PHIEUTHUTIEN() { MaKhachHang = SelectedCustomer.MaKhachHang, NgayThuTien = PayDate, SoTienThu = PaidAmount };
                    DataProvider.Ins.DB.PHIEUTHUTIENs.Add(phieuThuTien);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Lưu thành công");
                    SelectedCustomer = null;
                    PaidAmount = 0;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng !");
            }
        }

        void CancelInvoice()
        {
            this.CleanUpData();
        }
        public override void CleanUpData()
        {
            base.CleanUpData();
            SelectedCustomer = null;
            PaidAmount = 0;
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand NameCustomerSelectionChangedCommand { get; set; }
        public ICommand SaveButtonClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private ObservableCollection<KHACHHANG> _ListCustomer;
        private KHACHHANG _SelectedCustomer;
        private DateTime? _PayDate;
        private Int64 _PaidAmount;
        private Int64 _LeftAMount;
        private int _MaKhachHang;

        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }
        public KHACHHANG SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; OnPropertyChanged(); } }
        public DateTime? PayDate { get => _PayDate; set => _PayDate = value; }
        public long PaidAmount { get => _PaidAmount; set { _PaidAmount = value; OnPropertyChanged(); } }
        public long LeftAMount { get => _LeftAMount; set { _LeftAMount = value; OnPropertyChanged(); } }
        public int MaKhachHang { get => _MaKhachHang; set { _MaKhachHang = value; OnPropertyChanged(); } }

    }
}
