using BookStore.Model;
using BookStore.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class InvoiceViewModel : BaseViewModel
    {


        public InvoiceViewModel()
        {
            ListBook = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.LuongTon > 20));
            Items = CreateData();
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);


            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveInvoice(); });
            AddingNewItemCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { });
            AddCustomerClick = new RelayCommand<Object>((p) => { return true; }, (p) => { CreateNewCustomer(); });
            NameCustomerSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateSoNoCustomer();  });
            BookSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateListPriceOfBook(); UpdateBookInfor(); });
            PriceSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateIntoMoneyValue(); UpdateAmountBook(); });
            AmountTextChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateIntoMoneyValue(); });
            AddDetailClickCommand = new RelayCommand<object>((p) => { return AddDetailButtonNeed(); }, (p) => { AddDetail(); UpdateResultAMount(); });
            EditDetailClickCommand = new RelayCommand<object>((p) => { return EditDetailButtonNeed(); }, (p) => { EditDetail(); UpdateResultAMount(); });
            DeleteDetailClickCommand = new RelayCommand<object>((p) => { return DeleteDetailButtonNeed(); }, (p) => { DeleteDetail(); UpdateResultAMount(); });
            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => CloseInvoice(p));
            SearchButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => SearchBook());
            ExitButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => ExitWindow(p));
            PaidAmountTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { UpdateResultAMount(); });
        }


        private void UpdateSoNoCustomer()
        {
            if (SelectedCustomer != null && FlagIntent==0 && SelectedCustomer.SoNo>0)
            {
                MessageBox.Show("Khách hàng đang nợ " + SelectedCustomer.SoNo + "! Vui lòng thanh toán nợ!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                SumAmount = Rules.Instance.ConvertDecimal_nullToInt64(SelectedCustomer.SoNo);
            }
        }

        private void UpdateAmountBook()
        {
            if (SelectedBook != null)
            {
                Amount = (DataProvider.Ins.DB.SACHes.First(x => x.DAUSACH.TenSach == SelectedBook.TenSach).LuongTon - 20).ToString();
            }
            else
            {
                return;
            }
            
        }

        private void UpdateBookInfor()
        {
            if (SelectedBook == null)
            {
                return;
            }
            string tmptype = "";
            foreach (var v in SelectedBook.THELOAIs)
            {
                tmptype += (tmptype == "" ? "" : ", ") + v.TenTheLoai;
            }
            BookTypes = tmptype;
        }

        private void ExitWindow(Window p)
        {
            p.Close();
        }

        private void SearchBook()
        {
            var tmp = new ListBookWindow { };
            tmp.ShowDialog();
            var res_tmp = tmp.DataContext as ListBookViewModel;
        }

        private void CloseInvoice(Window p)
        {
            p.Close();
        }



        private void SaveInvoice()
        {
            if (FlagIntent == 0)
            {
                if (!SaveInvoiceNeed())
                {
                    MessageBox.Show("Vui lòng thanh toán hóa đơn!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (LeftAmount > 20000)
                {
                    MessageBox.Show("Số tiền nợ không được vượt quá 20000!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var HoaDon = new HOADON() { MaKhachHang = SelectedCustomer.MaKhachHang, MaNguoiLap = 1, NgayLapHoaDon = InvoiceDate, SoTienTra = PaidAmount, ConLai = LeftAmount, TongTien = SumAmount };
                DataProvider.Ins.DB.HOADONs.Add(HoaDon);
                DataProvider.Ins.DB.SaveChanges();

                if (HoaDon.ConLai >= 0 && HoaDon.ConLai <= 20000)
                {
                    DataProvider.Ins.DB.KHACHHANGs.First(x => x.MaKhachHang == HoaDon.MaKhachHang).SoNo = HoaDon.ConLai;
                    DataProvider.Ins.DB.SaveChanges();
                }



                foreach (var v in Items)
                {

                    var tmpCTPNS = DataProvider.Ins.DB.CT_PNS.First(x => x.SACH.DAUSACH.TenSach == v.DauSach.TenSach);
                    var CTHD = new CT_HD() { DonGiaBan = v.OutputPrice, MaHoaDon = HoaDon.MaHoaDon, SoLuong = int.Parse(v.Amount), MaSach = tmpCTPNS.MaSach };
                    DataProvider.Ins.DB.CT_HD.Add(CTHD);
                    DataProvider.Ins.DB.SaveChanges();
                    v.IDinDataBase = CTHD.MaCT_HD;
                }
                MessageBox.Show("Lập hóa đơn thành công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                CleanUpData();
            }
            if (FlagIntent == 1)
            {
                if (Items.Count() == 0)
                {
                    DataProvider.Ins.DB.HOADONs.Remove(Editor);
                    DataProvider.Ins.DB.SaveChanges();
                }
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Sửa hóa đơn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                CleanUpData();
            }


        }

        private bool SaveInvoiceNeed()
        {
            if (SelectedCustomer == null || InvoiceDate == null || Items.Count == 0)
            {
                return false;
            }
            return true;
        }

        private bool DeleteDetailButtonNeed()
        {
            if (SelectedItem == null || SelectedCustomer == null || InvoiceDate == null)
            {
                return false;
            }
            return true;
        }

        private bool EditDetailButtonNeed()
        {
            if (SelectedItem == null || SelectedBook == null || string.IsNullOrEmpty(Amount) || SelectedPriceOfBook == null || SelectedCustomer == null || InvoiceDate == null || FlagIntent==1)
            {
                return false;
            }
            return true;
        }

        private void DeleteDetail()
        {
            if (FlagIntent == 0)
            {
                DataProvider.Ins.DB.SACHes.First(x => x.DAUSACH.TenSach == SelectedItem.DauSach.TenSach).LuongTon += int.Parse(SelectedItem.Amount);
                //DataProvider.Ins.DB.SaveChanges();
                Items.Remove(SelectedItem);
            }


            if (FlagIntent == 1)
            {
                SumAmount -= SelectedItem.IntoMoney;
                LeftAmount = (SumAmount - PaidAmount);
                if (SumAmount>0) Editor.TongTien = SumAmount;
                if (LeftAmount<=0)
                {
                    Editor.SoTienTra = Editor.TongTien;
                    SelectedCustomer.SoNo = 0;
                    Editor.ConLai = 0;

                }
                if (LeftAmount>0 && LeftAmount <=20000)
                {
                    SelectedCustomer.SoNo = LeftAmount;
                    Editor.ConLai = LeftAmount;
                }

                DataProvider.Ins.DB.SACHes.First(x => x.DAUSACH.TenSach == SelectedItem.DauSach.TenSach).LuongTon += int.Parse(SelectedItem.Amount);
                var tmpCTHD = DataProvider.Ins.DB.CT_HD.First(x => (x.MaSach == SelectedItem.MaSachInNeed) && (x.MaHoaDon == Editor.MaHoaDon));
                DataProvider.Ins.DB.CT_HD.Remove(tmpCTHD);
                Items.Remove(SelectedItem);
            }

        }

        private void EditDetail()
        {
            SelectedItem.DauSach = SelectedBook;
            SelectedItem.Amount = Amount;
            SelectedItem.OutputPrice = SelectedPriceOfBook;
            SelectedItem.IntoMoney = IntoMoney;
        }

        private void UpdateResultAMount()
        {
            if (FlagIntent==0)
            {
                SumAmount = Rules.Instance.ConvertDecimal_nullToInt64(SelectedCustomer.SoNo);
                //PaidAmount = 0;
                //LeftAmount = 0;
                if (Items == null)
                {
                    return;
                }
                foreach (var v in Items)
                {
                    SumAmount += v.IntoMoney;
                }
                LeftAmount = SumAmount - PaidAmount;
            }
        }

        private void AddDetail()
        {
            if (int.Parse(Amount) > (DataProvider.Ins.DB.SACHes.First(x => x.DAUSACH.TenSach == SelectedBook.TenSach).LuongTon - 20))
            {
                MessageBox.Show("Số lượng tồn không đủ!");
            }
            else
            {
                var CT_HD = new Item_CT_HD() { DauSach = SelectedBook, ID = Items.Count() + 1, Amount = Amount, BookTypes = GetTypesString(SelectedBook.THELOAIs), OutputPrice = SelectedPriceOfBook, IntoMoney = IntoMoney };
                Items.Add(CT_HD);
                DataProvider.Ins.DB.SACHes.First(x => x.DAUSACH.TenSach == SelectedBook.TenSach).LuongTon -= int.Parse(Amount);
                //DataProvider.Ins.DB.SaveChanges();
            }
        }

        private bool AddDetailButtonNeed()
        {
            if (SelectedBook == null || string.IsNullOrEmpty(Amount) || SelectedPriceOfBook == null || int.Parse(Amount) == 0 || SelectedCustomer==null || InvoiceDate==null || FlagIntent == 1)
            {
                return false;
            }
            return true;
        }

        private void UpdateIntoMoneyValue()
        {
            if (SelectedPriceOfBook == null)
            {
                IntoMoney = 0;
                return;
            }
            IntoMoney = Rules.Instance.ConvertStringAmountToInt64(Amount) * Rules.Instance.ConvertDecimal_nullToInt64(SelectedPriceOfBook);
        }

        private void UpdateListPriceOfBook()
        {
            if (ListPriceOfBook == null)
            {
                ListPriceOfBook = new ObservableCollection<decimal?>();
            }
            else
            {
                ListPriceOfBook.Clear();
            }
            if (SelectedBook == null)
            {
                return;
            }
            var tmp = DataProvider.Ins.DB.CT_PNS.Where(x => x.SACH.DAUSACH.TenSach == SelectedBook.TenSach).ToList();
            if (tmp == null)
            {
                return;
            }
            foreach (var vv in tmp)
            {
                OutputPrice = vv.DonGiaNhap * 105 / 100;
                ListPriceOfBook.Add(OutputPrice);
            }
            Amount = "";
        }

        private string GetTypesString(ICollection<THELOAI> tHELOAIs)
        {
            string tmptype = "";
            foreach (var v in tHELOAIs)
            {
                tmptype += (tmptype == "" ? "" : ", ") + v.TenTheLoai;
            }
            return tmptype;
        }

        public void LoadData()
        {
            if (FlagIntent == 1)
            {
                Items = new ObservableCollection<Item_CT_HD> { };
                InvoiceDate = Editor.NgayLapHoaDon;
                Staff = DataProvider.Ins.DB.NGUOIDUNGs.First(x => x.MaNguoiDung == Editor.MaNguoiLap);
                foreach (var v in Editor.CT_HD)
                {
                    var tmpMBook = DataProvider.Ins.DB.DAUSACHes.First(x => x.MaDauSach == v.SACH.MaDauSach);
                    Items.Add(new Item_CT_HD()
                    {
                        ID = Items.Count + 1,
                        DauSach = tmpMBook,
                        BookTypes = GetTypesString(tmpMBook.THELOAIs),
                        OutputPrice = v.DonGiaBan,
                        Amount = v.SoLuong.ToString(),
                        IntoMoney = Rules.Instance.ConvertDecimal_nullToInt64(v.DonGiaBan) * Rules.Instance.ConvertInt_nullToInt64(v.SoLuong),
                        MaSachInNeed = v.MaSach,
                        MaCTHDInNeed = v.MaCT_HD

                    });
                }
                foreach(var kh in ListCustomer)
                {
                    if (kh.MaKhachHang==Editor.MaKhachHang)
                    {
                        SelectedCustomer = kh;
                    }
                }
                SumAmount = Rules.Instance.ConvertDecimal_nullToInt64(Editor.TongTien);
                PaidAmount = Rules.Instance.ConvertDecimal_nullToInt64(Editor.SoTienTra);
                LeftAmount = Rules.Instance.ConvertDecimal_nullToInt64(Editor.ConLai);
                return;
            }
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            FlagIntent = 0;
            SelectedBook = null;
            BookTypes = string.Empty;
            IntoMoney = 0;
            Amount = string.Empty;
            OutputPrice = 0;
            InvoiceDate = null;
            Items = null;
            ListBook = null;
            ListCustomer = null;
            Staff = null;
            Editor = null;
            SumAmount = 0;
            LeftAmount = 0;
            PaidAmount = 0;
            SelectedCustomer = null;
            SelectedPriceOfBook = null;
        }

        private void CreateNewCustomer()
        {
            AddCustomerWindow tmp = new AddCustomerWindow();
            tmp.ShowDialog();

        }

        private static ObservableCollection<Item_CT_HD> CreateData()
        {
            return new ObservableCollection<Item_CT_HD> { };
        }

        public ICommand SaveButtonClickCommand { get; set; }
        public ICommand AddingNewItemCommand { get; set; }
        public ICommand AddCustomerClick { get; set; }
        public ICommand NameCustomerSelectionChangedCommand { get; set; }
        public ICommand BookSelectionChangedCommand { get; set; }
        public ICommand PriceSelectionChangedCommand { get; set; }
        public ICommand AmountTextChangedCommand { get; set; }
        public ICommand AddDetailClickCommand { get; set; }
        public ICommand EditDetailClickCommand { get; set; }
        public ICommand DeleteDetailClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }
        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand ExitButtonClickCommand { get; set; }
        public ICommand PaidAmountTextChangedCommand { get; set; }

        private int _FlagIntent;
        private HOADON _Editor;
        private NGUOIDUNG _Staff;
        private ObservableCollection<Item_CT_HD> _Items;
        private ObservableCollection<DAUSACH> _ListBook;
        private ObservableCollection<KHACHHANG> _ListCustomer;
        private KHACHHANG _SelectedCustomer;
        private KHACHHANG _SelectedCustomer_2;
        private DAUSACH _SelectedBook;
        private ObservableCollection<decimal?> _ListPriceOfBook;
        private string _Amount;
        private decimal? _SelectedPriceOfBook;
        private Int64 _IntoMoney;
        private Int64 _SumAmount;
        private Int64 _PaidAmount;
        private Int64 _LeftAmount;
        private Item_CT_HD _SelectedItem;
        private DateTime? _InvoiceDate;
        private string _BookTypes;
        private decimal? _OutputPrice;


        public decimal? OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }

        public int FlagIntent { get => _FlagIntent; set => _FlagIntent = value; }

        public HOADON Editor { get => _Editor; set => _Editor = value; }

        public NGUOIDUNG Staff { get => _Staff; set { _Staff = value; OnPropertyChanged(); } }

        public ObservableCollection<Item_CT_HD> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }

        public ObservableCollection<DAUSACH> ListBook { get => _ListBook; set { _ListBook = value; } }

        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }

        public KHACHHANG SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; OnPropertyChanged(); } }

        public KHACHHANG SelectedCustomer_2 { get => _SelectedCustomer_2; set { _SelectedCustomer_2 = value; OnPropertyChanged(); } }

        public DAUSACH SelectedBook { get => _SelectedBook; set { _SelectedBook = value; OnPropertyChanged(); } }

        public ObservableCollection<decimal?> ListPriceOfBook { get => _ListPriceOfBook; set { _ListPriceOfBook = value; OnPropertyChanged(); } }


        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }

        public decimal? SelectedPriceOfBook { get => _SelectedPriceOfBook; set { _SelectedPriceOfBook = value; OnPropertyChanged(); } }

        public Int64 IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }

        public long SumAmount { get => _SumAmount; set { _SumAmount = value; OnPropertyChanged(); } }

        public long PaidAmount { get => _PaidAmount; set { _PaidAmount = value; OnPropertyChanged(); } }

        public long LeftAmount { get => _LeftAmount; set { _LeftAmount = value; OnPropertyChanged(); } }

        public Item_CT_HD SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedBook = SelectedItem.DauSach;
                    SelectedPriceOfBook = SelectedItem.OutputPrice;
                    Amount = SelectedItem.Amount;
                    IntoMoney = SelectedItem.IntoMoney;
                }
            }
        }

        public DateTime? InvoiceDate { get => _InvoiceDate; set { _InvoiceDate = value; OnPropertyChanged(); } }

        public string BookTypes { get => _BookTypes; set { _BookTypes = value; OnPropertyChanged(); } }
    }
}
