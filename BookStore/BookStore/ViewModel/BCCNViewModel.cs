
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
using System.Windows.Data;
using System.Windows.Input;
//using Excel = Microsoft.Office.Interop.Excel;
namespace BookStore.ViewModel
{
    public class BCCNViewModel : BaseViewModel
    {
        public BCCNViewModel()
        {
            Date = DateTime.Now;
            ListReport = new ObservableCollection<BAOCAOCONGNO>(DataProvider.Ins.DB.BAOCAOCONGNOes);
            load();
            if (BaoCaoCongNoSource.Count != 0)
                Selected = BaoCaoCongNoSource[0];
            else
                Selected = new ChiTietBaoCaoCongNo();
            CloseWindowCommand = new RelayCommand<BCCNWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });

            ButtonSaveClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Save(p); });
            ButtonExportClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {/* Export(); */});

        }
        public class ChiTietBaoCaoCongNo
        {
            public int maKH { get; set; }
            public decimal? noDau { get; set; }
            public decimal? noMoi { get; set; }
            public decimal? daThu { get; set; }
            public decimal? noCuoi { get; set; }
        }

        void load()
        {
            BaoCaoCongNoSource = new List<ChiTietBaoCaoCongNo>();
            var chiTietBCCN = new ObservableCollection<CT_BCCN>(DataProvider.Ins.DB.CT_BCCN);
            var BCCN = new ObservableCollection<BAOCAOCONGNO>(DataProvider.Ins.DB.BAOCAOCONGNOes);
            var hoaDon = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            var phieuThuTien = new ObservableCollection<PHIEUTHUTIEN>(DataProvider.Ins.DB.PHIEUTHUTIENs);
            var chiTietPNS = new ObservableCollection<PHIEUNHAPSACH>(DataProvider.Ins.DB.PHIEUNHAPSACHes);
            var ctThangTruoc = chiTietBCCN.Where(x => x.MaBaoCaoCongNo == BCCN.Last().MaBaoCaoCongNo);

            var maSachThangTruoc = ctThangTruoc.Select(x => x.MaKhachHang);

            foreach (var item in DataProvider.Ins.DB.KHACHHANGs)
            {
                if (maSachThangTruoc.Contains(item.MaKhachHang))
                {
                    BaoCaoCongNoSource.Add(new ChiTietBaoCaoCongNo() { maKH = item.MaKhachHang, noMoi = hoaDon.Where(x => (x.NgayLapHoaDon > BCCN.Last().Thang) && x.MaKhachHang == item.MaKhachHang).Sum(x => x.ConLai), daThu = phieuThuTien.Where(x => ((x.NgayThuTien > BCCN.Last().Thang) && x.MaKhachHang == item.MaKhachHang)).Sum(x => x.SoTienThu), noDau = ctThangTruoc.Where(x => x.MaKhachHang == item.MaKhachHang).First().NoCuoi, noCuoi = item.SoNo });
                }
                else
                {
                    BaoCaoCongNoSource.Add(new ChiTietBaoCaoCongNo() { maKH = item.MaKhachHang, noMoi = hoaDon.Where(x => (x.NgayLapHoaDon > BCCN.Last().Thang) && x.MaKhachHang == item.MaKhachHang).Sum(x => x.ConLai), daThu = phieuThuTien.Where(x => ((x.NgayThuTien > BCCN.Last().Thang) && x.MaKhachHang == item.MaKhachHang)).Sum(x => x.SoTienThu), noDau = 0, noCuoi = item.SoNo });
                }
            }
        }
        void Save(Button p)
        {
            DataProvider.Ins.DB.BAOCAOCONGNOes.Add(new BAOCAOCONGNO() { Thang = Date });
            DataProvider.Ins.DB.SaveChanges();
            var BCCN = new ObservableCollection<BAOCAOCONGNO>(DataProvider.Ins.DB.BAOCAOCONGNOes);
            foreach (var item in BaoCaoCongNoSource)
            {

                DataProvider.Ins.DB.CT_BCCN.Add(new CT_BCCN() { MaBaoCaoCongNo = BCCN.Last().MaBaoCaoCongNo, MaKhachHang = item.maKH, NoDau = item.noDau, NoCuoi = item.noCuoi, NoMoi = item.noMoi, DaThu = item.daThu });

            }
            DataProvider.Ins.DB.SaveChanges();
            if (MessageBox.Show("Lập báo cáo thành công", "Thông báo", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Grid grid1 = p.Parent as Grid;
                Border border1 = grid1.Parent as Border;
                Grid grid2 = border1.Parent as Grid;
                Border border2 = grid2.Parent as Border;
                BCCNWindow window = border2.Parent as BCCNWindow;
                window.Close();
            }
        }
      /*  void Export()
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
                            myRange.Value2 = "Mã khách hàng";
                            break;
                        }
                    case 1:
                        {
                            myRange.Value2 = "Nợ đầu";
                            break;
                        }
                    case 2:
                        {
                            myRange.Value2 = "Nợ mới";
                            break;
                        }
                    case 3:
                        {
                            myRange.Value2 = "Nợ đầu";
                            break;
                        }
                    case 4:
                        {
                            myRange.Value2 = "Nợ cuối";
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
                            for (int j = 0; j < BaoCaoCongNoSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoCongNoSource[j].maKH.ToString();
                            }
                            break;
                        }
                    case 1:
                        {
                            for (int j = 0; j < BaoCaoCongNoSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoCongNoSource[j].noDau.ToString();
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int j = 0; j < BaoCaoCongNoSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoCongNoSource[j].noMoi.ToString();
                            }
                            break;
                        }
                    case 3:
                        {
                            for (int j = 0; j < BaoCaoCongNoSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoCongNoSource[j].daThu.ToString();
                            }
                            break;
                        }
                    case 4:
                        {
                            for (int j = 0; j < BaoCaoCongNoSource.Count; j++)
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                                myRange.Value2 = BaoCaoCongNoSource[j].noCuoi.ToString();
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
        private ChiTietBaoCaoCongNo _Selected;
        private List<ChiTietBaoCaoCongNo> _BaoCaoCongNoSource;
        private ObservableCollection<BAOCAOCONGNO> _ListReport;
        public DateTime? Date { get => _Date; set => _Date = value; }
        public ChiTietBaoCaoCongNo Selected { get => _Selected; set { _Selected = value; OnPropertyChanged(); } }
        public List<ChiTietBaoCaoCongNo> BaoCaoCongNoSource { get => _BaoCaoCongNoSource; set { _BaoCaoCongNoSource = value; OnPropertyChanged(); } }
        public ObservableCollection<BAOCAOCONGNO> ListReport { get => _ListReport; set { _ListReport = value; OnPropertyChanged(); } }

    }
}
