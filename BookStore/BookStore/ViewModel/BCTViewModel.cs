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
//using Excel = Microsoft.Office.Interop.Excel;
namespace BookStore.ViewModel
{
    public class BCTViewModel : BaseViewModel
    {
        public BCTViewModel()
        {
            Date = DateTime.Now;
            ListReport = new ObservableCollection<BAOCAOTON>(DataProvider.Ins.DB.BAOCAOTONs);
            load();
            if (BaoCaoTonSource[0] != null)
                Selected = BaoCaoTonSource[0];
            CloseWindowCommand = new RelayCommand<BCTWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });

            ButtonSaveClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Save(p); });
           // ButtonExportClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Export(); });

        }
        public class ChiTietBaoCaoTon
        {
            public int maDauSach { get; set; }
            public int? tonDau { get; set; }
            public int? nhapVao { get; set; }
            public int? banRa { get; set; }
            public int? tonCuoi { get; set; }
        }

        void load()
        {
            BaoCaoTonSource = new List<ChiTietBaoCaoTon>();
            var chiTietBCT = new ObservableCollection<CT_BCT>(DataProvider.Ins.DB.CT_BCT);
            var BCT = new ObservableCollection<BAOCAOTON>(DataProvider.Ins.DB.BAOCAOTONs);
            var hoaDon = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            var CTHD = new ObservableCollection<CT_HD>(DataProvider.Ins.DB.CT_HD);
            var phieuNhapSach = new ObservableCollection<PHIEUNHAPSACH>(DataProvider.Ins.DB.PHIEUNHAPSACHes);
            var chiTietPNS = new ObservableCollection<CT_PNS>(DataProvider.Ins.DB.CT_PNS);
            var sach = new ObservableCollection<SACH>(DataProvider.Ins.DB.SACHes);
            var ctThangTruoc = chiTietBCT.Where(x => x.MaBaoCaoTon == BCT.Last().MaBaoCaoTon);
            var maPhieuNhap = phieuNhapSach.Where(x => x.NgayNhap > BCT.Last().Thang).Select(x => x.MaPhieuNhapSach);
            var maSachThangTruoc = ctThangTruoc.Select(x => x.MaDauSach);
            var maHD = hoaDon.Where(x => x.NgayLapHoaDon > BCT.Last().Thang).Select(x => x.MaHoaDon);
            foreach (var item in DataProvider.Ins.DB.DAUSACHes)
            {
                var maSach = sach.Where(x => x.MaDauSach == item.MaDauSach).Select(x => x.MaSach);
                if (maSachThangTruoc.Contains(item.MaDauSach))
                {
                    BaoCaoTonSource.Add(new ChiTietBaoCaoTon() { maDauSach = item.MaDauSach, nhapVao = chiTietPNS.Where(x => (maSach.Contains(x.MaSach) && maPhieuNhap.Contains(x.MaPhieuNhapSach))).Sum(x => x.SoLuong), banRa = CTHD.Where(x => (maHD.Contains(x.MaHoaDon) && maSach.Contains(x.MaSach))).Sum(x => x.SoLuong), tonDau = ctThangTruoc.Where(x => x.MaDauSach == item.MaDauSach).First().TonCuoi, tonCuoi = item.LuongTon });
                }
                else
                {
                    BaoCaoTonSource.Add(new ChiTietBaoCaoTon() { maDauSach = item.MaDauSach, nhapVao = chiTietPNS.Where(x => (maSach.Contains(x.MaSach) && maPhieuNhap.Contains(x.MaPhieuNhapSach))).Sum(x => x.SoLuong), tonDau = 0, tonCuoi = item.LuongTon, banRa = CTHD.Where(x => (maHD.Contains(x.MaHoaDon) && maSach.Contains(x.MaSach))).Sum(x => x.SoLuong) });
                }
            }
        }
        void Save(Button p)
        {
            DataProvider.Ins.DB.BAOCAOTONs.Add(new BAOCAOTON() { Thang = Date });
            DataProvider.Ins.DB.SaveChanges();
            var BCT = new ObservableCollection<BAOCAOTON>(DataProvider.Ins.DB.BAOCAOTONs);
            foreach (var item in BaoCaoTonSource)
            {

                DataProvider.Ins.DB.CT_BCT.Add(new CT_BCT() { MaBaoCaoTon = BCT.Last().MaBaoCaoTon, MaDauSach = item.maDauSach, TonDau = item.tonDau, TonCuoi = item.tonCuoi, NhapVao = item.nhapVao, BanRa = item.banRa });

            }
            DataProvider.Ins.DB.SaveChanges();
            if (MessageBox.Show("Lập báo cáo thành công", "Thông báo", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Grid grid1 = p.Parent as Grid;
                Border border1 = grid1.Parent as Border;
                Grid grid2 = border1.Parent as Grid;
                Border border2 = grid2.Parent as Border;
                BCTWindow window = border2.Parent as BCTWindow;
                window.Close();
            }
        }
       /* void Export()
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < 5; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = sheet1.Cells[1, j + 1] as Excel.Range;
                switch (j)
                {
                    case 0:
                        {
                            myRange.Value2 = "Mã đầu sách";
                            break;
                        }
                    case 1:
                        {
                            myRange.Value2 = "Tồn đầu";
                            break;
                        }
                    case 2:
                        {
                            myRange.Value2 = "Nhập vào";
                            break;
                        }
                    case 3:
                        {
                            myRange.Value2 = "Bán ra";
                            break;
                        }
                    case 4:
                        {
                            myRange.Value2 = "Tồn cuối";
                            break;
                        }
                    default:
                        break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            for (int j = 0; j < BaoCaoTonSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoTonSource[j].maDauSach.ToString();
                            }
                            break;
                        }
                    case 1:
                        {
                            for (int j = 0; j < BaoCaoTonSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoTonSource[j].tonDau.ToString();
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int j = 0; j < BaoCaoTonSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoTonSource[j].nhapVao.ToString();
                            }
                            break;
                        }
                    case 3:
                        {
                            for (int j = 0; j < BaoCaoTonSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoTonSource[j].banRa.ToString();
                            }
                            break;
                        }
                    case 4:
                        {
                            for (int j = 0; j < BaoCaoTonSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoTonSource[j].tonCuoi.ToString();
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }*/
        public ICommand CloseWindowCommand { get; set; }
        public ICommand ButtonSaveClickCommand { get; set; }
        public ICommand ButtonExportClickCommand { get; set; }

        private DateTime? _Date;
        private ChiTietBaoCaoTon _Selected;
        private List<ChiTietBaoCaoTon> _BaoCaoTonSource;
        private ObservableCollection<BAOCAOTON> _ListReport;
        public DateTime? Date { get => _Date; set => _Date = value; }
        public ChiTietBaoCaoTon Selected { get => _Selected; set { _Selected = value; OnPropertyChanged(); } }
        public List<ChiTietBaoCaoTon> BaoCaoTonSource { get => _BaoCaoTonSource; set { _BaoCaoTonSource = value; OnPropertyChanged(); } }
        public ObservableCollection<BAOCAOTON> ListReport { get => _ListReport; set { _ListReport = value; OnPropertyChanged(); } }

    }
}