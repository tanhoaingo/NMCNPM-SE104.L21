using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    class ReportViewModel : BaseViewModel
    {
        public ReportViewModel() 
        {
            MonthItemSource = new string[] {
                "Tất cả", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"
            };
            YearItemSource = new string[]
            {
                "Tất cả", DateTime.Now.Year.ToString(),(DateTime.Now.Year-1).ToString(),(DateTime.Now.Year-2).ToString(),(DateTime.Now.Year-3).ToString(),(DateTime.Now.Year-4).ToString()
            };
            SelectedMonth = "Tất cả";
            SelectedYear = "Tất cả";
            SelectedMonthBCT = "Tất cả";
            SelectedMonthBCCN = "Tất cả";

            loadStatistic("Tất cả", "Tất cả");
            loadBaoCaoTon("Tất cả");
            loadBaoCaoCongNo("Tất cả");

            SelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => loadStatistic(SelectedMonth, SelectedYear));
            BCTSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => loadBaoCaoTon(SelectedMonthBCT));
            BCCNSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => loadBaoCaoCongNo(SelectedMonthBCCN));
        }

        

        public void loadStatistic(String month, String year)
        {

            var theLoai = new ObservableCollection<THELOAI>(DataProvider.Ins.DB.THELOAIs);
            var hoaDon = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            var chiTietHD = new ObservableCollection<CT_HD>(DataProvider.Ins.DB.CT_HD);
            var dauSach = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);
            var sach = new ObservableCollection<SACH>(DataProvider.Ins.DB.SACHes);

            List<Statistic> statistic = new List<Statistic>();

            for (int i = 0; i < theLoai.Count; i++)
            {
                var maTheLoai = theLoai[i].MaTheLoai;
                var maDauSach = theLoai[i].DAUSACHes.Select(x => x.MaDauSach);
                var maSach = sach.Where(x => maDauSach.Contains(x.MaDauSach)).Select(x => x.MaSach);
                var maHoaDon = hoaDon.Where(x => (month == "Tất cả" && year == "Tất cả") ? true : (month == "Tất cả" ? x.NgayLapHoaDon.Value.Year.ToString() == year : (year == "Tất cả" ? x.NgayLapHoaDon.Value.Month.ToString() == month : x.NgayLapHoaDon.Value.Month.ToString() == month && x.NgayLapHoaDon.Value.Year.ToString() == year))).Select(x => x.MaHoaDon);
                int? amount = chiTietHD.Where(x => maSach.Contains(x.MaSach) && maHoaDon.Contains(x.MaHoaDon)).Sum(x => x.SoLuong);
                decimal? money = chiTietHD.Where(x => maSach.Contains(x.MaSach) && maHoaDon.Contains(x.MaHoaDon)).Sum(x => x.SoLuong * x.DonGiaBan);

                statistic.Add(new Statistic()
                {
                    Name = theLoai[i].TenTheLoai,
                    Amount = amount,
                    Money = money
                });
            }
            itemSource = new List<Statistic>(statistic);
        }


        public void loadBaoCaoTon(string month)
        {
            var chiTiet = new ObservableCollection<CT_BCT>(DataProvider.Ins.DB.CT_BCT);
            var baoCaoTon = new ObservableCollection<BAOCAOTON>(DataProvider.Ins.DB.BAOCAOTONs);
            var sach = new ObservableCollection<SACH>(DataProvider.Ins.DB.SACHes);
            var dauSach = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);

            List<ChiTietBaoCaoTon> list = new List<ChiTietBaoCaoTon>();
            var maBCT = baoCaoTon.Where(x => (month == "Tất cả" ? true : x.Thang.Value.Month.ToString() == month)).Select(x => x.MaBaoCaoTon);
            /*foreach (var item in maBCT)
            {
                var ct_BCT = chiTiet.Where(x => x.MaBaoCaoTon == item);
                foreach (var ct in ct_BCT)
                {
                    var maDauSach = sach.Where(x => x.MaSach == ct.MaSach).Select(x => x.MaDauSach).First();
                    var tenSach = dauSach.Where(x => x.MaDauSach == maDauSach).Select(x => x.TenSach).First();
                    list.Add(new ChiTietBaoCaoTon()
                    {
                        Sach = tenSach,
                        tonDau = ct.TonDau,
                        tonCuoi = ct.TonCuoi,
                        phatSinh = ct.PhatSinh
                    });
                }
            }*/
            BaoCaoTonSource = list;
        }
        public void loadBaoCaoCongNo(string month)
        {
            var chiTiet = new ObservableCollection<CT_BCCN>(DataProvider.Ins.DB.CT_BCCN);
            var baoCaoCongNo = new ObservableCollection<BAOCAOCONGNO>(DataProvider.Ins.DB.BAOCAOCONGNOes);
            var khacHang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);


            List<ChiTietBaoCaoCongNo> list = new List<ChiTietBaoCaoCongNo>();
            var maBCCN = baoCaoCongNo.Where(x => (month == "Tất cả" ? true : x.Thang.Value.Month.ToString() == month)).Select(x => x.MaBaoCaoCongNo);
            foreach (var item in maBCCN)
            {
                var ct_BCCN = chiTiet.Where(x => x.MaBaoCaoCongNo == item);
                foreach (var ct in ct_BCCN)
                {
                    var tenKH = khacHang.Where(x => x.MaKhachHang == ct.MaKhachHang).Select(x => x.HoTenKhachHang).First();
                    list.Add(new ChiTietBaoCaoCongNo()
                    {
                        maKH = ct.MaKhachHang,
                        khachHang = tenKH,
                        noDau = ct.NoDau,
                        noCuoi = ct.NoCuoi,
                        phatSinh = ct.PhatSinh
                    });
                }
            }
            BaoCaoCongNoSource = list;
        }

        public class Statistic
        {
            public string Name { get; set; }
            public int? Amount { get; set; }
            public decimal? Money { get; set; }
        }
        public class ChiTietBaoCaoTon
        {
            public string Sach { get; set; }
            public int? tonDau { get; set; }
            public int? phatSinh { get; set; }
            public int? tonCuoi { get; set; }
        }
        public class ChiTietBaoCaoCongNo
        {
            public int maKH { get; set; }
            public string khachHang { get; set; }
            public decimal? noDau { get; set; }
            public decimal? phatSinh { get; set; }
            public decimal? noCuoi { get; set; }
        }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand BCTSelectionChangedCommand { get; set; }
        public ICommand BCCNSelectionChangedCommand { get; set; }

        private List<Statistic> _itemSource;
        private string _SelectedYear;
        private string[] _YearItemSource;
        private string _SelectedMonth;
        private string[] _MonthItemSource;
        private string _SelectedMonthBCT;
        private string _SelectedMonthBCCN;
        private List<ChiTietBaoCaoTon> _BaoCaoTonSource;
        private List<ChiTietBaoCaoCongNo> _BaoCaoCongNoSource;

        public List<Statistic> itemSource { get => _itemSource; set { _itemSource = value; OnPropertyChanged(); } }
        public string SelectedYear { get => _SelectedYear; set { _SelectedYear = value; OnPropertyChanged(); } }
        public string[] YearItemSource { get => _YearItemSource; set => _YearItemSource = value; }
        public string SelectedMonth { get => _SelectedMonth; set { _SelectedMonth = value; OnPropertyChanged(); } }
        public string[] MonthItemSource { get => _MonthItemSource; set => _MonthItemSource = value; }
        public string SelectedMonthBCT { get => _SelectedMonthBCT; set { _SelectedMonthBCT = value; OnPropertyChanged(); } }
        public string SelectedMonthBCCN { get => _SelectedMonthBCCN; set { _SelectedMonthBCCN = value; OnPropertyChanged(); } }
        public List<ChiTietBaoCaoTon> BaoCaoTonSource { get => _BaoCaoTonSource; set { _BaoCaoTonSource = value; OnPropertyChanged(); } }
        public List<ChiTietBaoCaoCongNo> BaoCaoCongNoSource { get => _BaoCaoCongNoSource; set { _BaoCaoCongNoSource = value; OnPropertyChanged(); } }
    }
}
