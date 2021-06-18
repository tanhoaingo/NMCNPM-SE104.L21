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
    
    public class ThuTienViewModel : BaseViewModel
    {
        private ObservableCollection<KHACHHANG> _ListCustomer;
        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }
        private KHACHHANG _SelectedCustomer;
        public KHACHHANG SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; OnPropertyChanged(); } }
        private DateTime? _payDate;
        public DateTime? payDate { get => _payDate; set => _payDate = value; }
        private Int64 _PaidAmount;
        public long PaidAmount { get => _PaidAmount; set { _PaidAmount = value; OnPropertyChanged(); } }
        private Int64 _LeftAMount;
        public long LeftAMount { get => _LeftAMount; set { _LeftAMount = value; OnPropertyChanged(); } }
        private int _maKhachHang;
        public int maKhachHang { get => _maKhachHang; set { _maKhachHang = value; OnPropertyChanged(); } }
        
        
        public ThuTienViewModel()
        {
            payDate = DateTime.Now;
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            NameCustomerSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { });
            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveInvoice(); });
            CancelButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { CancelInvoice(); });
        }

        public ICommand NameCustomerSelectionChangedCommand { get; set; }
        public ICommand SaveButtonClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        void SaveInvoice() {
            bool IsComplete = false;
            if(SelectedCustomer != null)
            {
                foreach (var item in DataProvider.Ins.DB.KHACHHANGs)
                {
                   
                    if (item.MaKhachHang == SelectedCustomer.MaKhachHang)
                    {
                        if (PaidAmount <= 0 || PaidAmount == null)
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
                    
                    var phieuThuTien = new PHIEUTHUTIEN() { MaKhachHang = SelectedCustomer.MaKhachHang, NgayThuTien = payDate, SoTienThu = PaidAmount };
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

        void CancelInvoice() {
            SelectedCustomer = null;
            PaidAmount = 0;
        }
    }
}
