
﻿using BookStore.Model;
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
            if (BaoCaoCongNoSource[0] != null)
                Selected = BaoCaoCongNoSource[0];
            CloseWindowCommand = new RelayCommand<BCCNWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });
            ButtonSaveClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Save(); });
            ButtonExportClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { /*Export();*/ });
        }
        public class ChiTietBaoCaoCongNo
        {
            public int maKH { get; set; }
            public decimal? noDau { get; set; }
            public decimal? phatSinh { get; set; }
            public decimal? noCuoi { get; set; }
        }

        void load()
        {
            BaoCaoCongNoSource = new List<ChiTietBaoCaoCongNo>();
            var chiTietBCCN = new ObservableCollection<CT_BCCN>(DataProvider.Ins.DB.CT_BCCN);
            var BCCN = new ObservableCollection<BAOCAOCONGNO>(DataProvider.Ins.DB.BAOCAOCONGNOes);
            var ctThangTruoc = chiTietBCCN.Where(x => x.MaBaoCaoCongNo == BCCN.Last().MaBaoCaoCongNo);
            var maSachThangTruoc = ctThangTruoc.Select(x => x.MaKhachHang);
            foreach (var item in DataProvider.Ins.DB.KHACHHANGs)
            {
                if (maSachThangTruoc.Contains(item.MaKhachHang))
                {
                    BaoCaoCongNoSource.Add(new ChiTietBaoCaoCongNo() { maKH = item.MaKhachHang, phatSinh = 0, noDau = ctThangTruoc.Where(x => x.MaKhachHang == item.MaKhachHang).First().NoCuoi, noCuoi = item.SoNo });
                }
                else
                {
                    BaoCaoCongNoSource.Add(new ChiTietBaoCaoCongNo() { maKH = item.MaKhachHang, phatSinh = 0, noDau = 0, noCuoi = item.SoNo });
                }
            }
        }
        void Save()
        {
            DataProvider.Ins.DB.BAOCAOCONGNOes.Add(new BAOCAOCONGNO() { Thang = Date });
            DataProvider.Ins.DB.SaveChanges();
            var BCCN = new ObservableCollection<BAOCAOCONGNO>(DataProvider.Ins.DB.BAOCAOCONGNOes);
            foreach (var item in BaoCaoCongNoSource)
            {
                //DataProvider.Ins.DB.CT_BCCN.Add(new CT_BCCN() { MaBaoCaoCongNo = BCCN.Last().MaBaoCaoCongNo,
                //    MaKhachHang = item.maKH, NoDau = item.noDau, NoCuoi = item.noCuoi, PhatSinh = item.phatSinh });
            }
            DataProvider.Ins.DB.SaveChanges();
        }
      /*  void Export()
        {

            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < 4; j++)
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
                            myRange.Value2 = "Phát sinh";
                            break;
                        }
                    case 3:
                        {
                            myRange.Value2 = "Nợ cuối";
                            break;
                        }
                    default:
                        break;
                }
            }
            for (int i = 0; i < 4; i++)
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
                                myRange.Value2 = BaoCaoCongNoSource[j].phatSinh.ToString();
                            }
                            break;
                        }
                    case 3:
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
