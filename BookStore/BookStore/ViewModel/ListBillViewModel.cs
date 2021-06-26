using BookStore.Model;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace BookStore.ViewModel
{
    public class ListBillViewModel : BaseViewModel
    {
        public ListBillViewModel()
        {
            ListBill = new ObservableCollection<PHIEUTHUTIEN>(DataProvider.Ins.DB.PHIEUTHUTIENs);
            oldPays = new List<OldPay>();
            foreach (var item in ListBill)
            {
                OldPay tmp = new OldPay();
                tmp.maPTT = item.MaPhieuThuTien;
                tmp.soTienThu = item.SoTienThu;
                oldPays.Add(tmp);
            }
            SelectedBill = ListBill[0];
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { loadEdit(); });
            CloseWindowCommand = new RelayCommand<ListBillWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });
        }
        public class OldPay {
            public int? maPTT { get; set; }
            public decimal? soTienThu { get; set; }
        }
        public void loadEdit()
        {
            int i = 0;
            foreach (var item in DataProvider.Ins.DB.PHIEUTHUTIENs)
            {
                var thamSo = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes).First();
                if(thamSo.ChoPhepThuLonHonNo == true) {
                    foreach (var kh in DataProvider.Ins.DB.KHACHHANGs)
                    {
                        if(item.MaKhachHang == kh.MaKhachHang)
                        {
                            kh.SoNo = kh.SoNo - (item.SoTienThu - oldPays[i].soTienThu);
                            oldPays[i].soTienThu = item.SoTienThu;
                        }
                    }
                }
                else {
                    foreach (var kh in DataProvider.Ins.DB.KHACHHANGs)
                    {
                        if (item.MaKhachHang == kh.MaKhachHang)
                        {
                            if ((kh.SoNo - (item.SoTienThu - oldPays[i].soTienThu)) >= 0)
                            {
                                kh.SoNo = kh.SoNo - (item.SoTienThu - oldPays[i].soTienThu);
                                oldPays[i].soTienThu = item.SoTienThu;
                            }
                            else
                            {
                                ListBill[i].SoTienThu = item.SoTienThu = oldPays[i].soTienThu;
                                MessageBox.Show("Mã PTT: " + item.MaPhieuThuTien + " có số tiền thu vượt quá nợ !");
                            }
                        }
                    }
                }
                i++;
            }
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Lưu thay đổi thành công !");
        }
        public override void CleanUpData()
        {
            base.CleanUpData();
            SelectedBill = ListBill[0];
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }

        private ObservableCollection<PHIEUTHUTIEN> _ListBill;
        private List<OldPay> _oldPays;
        private PHIEUTHUTIEN _SelectedBill;


        public ObservableCollection<PHIEUTHUTIEN> ListBill { get => _ListBill; set { _ListBill = value; OnPropertyChanged(); } }
        public List<OldPay> oldPays { get => _oldPays; set => _oldPays = value; }
        public PHIEUTHUTIEN SelectedBill { get => _SelectedBill; set { _SelectedBill = value; OnPropertyChanged(); } }

}
}
