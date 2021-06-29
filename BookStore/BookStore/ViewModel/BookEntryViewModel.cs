using BookStore.Model;
using BookStore.Pages;
using BookStore.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace BookStore.ViewModel
{
    public class BookEntryViewModel : BaseViewModel
    {

        public BookEntryViewModel()
        {
            #region Temp Value
            Staff = DataProvider.Ins.DB.NGUOIDUNGs.First(x => x.MaNguoiDung == 1);
            DeletedCT_PN = new List<int>();
            #endregion

            ListBook = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);
            Items = new ObservableCollection<Item_CT_PNS>();
            EntryBookDate = DateTime.Now;
            Staff = User.Ins.nguoiDung;

            CloseWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { this.CleanUpData(); });
            InputPriceTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UpdateIntoMoney(); });
            AddDetailClickCommand = new RelayCommand<object>((p) => { return AddDetailNeed(); }, (p) => { AddDetail(); });
            BookNameSelectionChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UpdateBookInfor(); });
            SaveButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SaveBookEntry(); });
            ItemListDetailSelectionChangedCommand = new RelayCommand<object> ((p) => { return true; }, (p) => { LoadFromDetail(); });
            EditDetailClickCommand = new RelayCommand<object>((p) => { return EditDetailNeed(); }, (p) => { EditDetail(); });
            DeleteDetailClickCommand = new RelayCommand<object>((p) => { return DeleteDetailNeed(); }, (p) => { DeleteDetail(); });
            AmountTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UpdateIntoMoney(); });
            CancelButtonClickCommand = new RelayCommand<BookEntryPage>((p) => { return true; }, (p) => { Cancel(p); });
        }

        private void Cancel(BookEntryPage p)
        {
            SelectedBook = null;
            InputPrice = 0;
            Amount = 0;
            UpdateIntoMoney();
            Items = null;
            UpdateSumAmount();
        }

        private bool DeleteDetailNeed()
        {
            return SelectedDetail != null;
        }

        private void DeleteDetail()
        {
            DeletedCT_PN.Add(SelectedDetail.MaCTPNSInNeed);
            Items.Remove(SelectedDetail);
            SelectedDetail = null; 
            foreach(var v in Items)
            {
                v.ID = Items.IndexOf(v) + 1;
            }
            UpdateSumAmount();
        }

        private bool EditDetailNeed()
        {
            return AddDetailNeed() && SelectedDetail != null;
        }

        private void EditDetail()
        {
            SelectedDetail.Book = SelectedBook;
            SelectedDetail.Amount = Amount;
            SelectedDetail.InputPrice = InputPrice;
            SelectedDetail.IntoMoney = IntoMoney;
            UpdateSumAmount();
        }

        private void LoadFromDetail()
        {
            if (SelectedDetail == null)
            {
                return;
            }
            SelectedBook = SelectedDetail.Book;
            Authors = GetAuthorsString(SelectedDetail.Book.TACGIAs);
            Types = GetTypesString(SelectedDetail.Book.THELOAIs);
            Amount = SelectedDetail.Amount;
            InputPrice = InputPrice;
            IntoMoney = SelectedDetail.IntoMoney ?? 0;
        }

        private void SaveBookEntry()
        {
            if ((Items.Count() == 0 && FlagIntent == 0) || EntryBookDate == null)
            {
                MessageBox.Show("Vui lòng hoàn thành phiếu!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (FlagIntent == 0)
            {
                var tmpPN = new PHIEUNHAPSACH() { NgayNhap = EntryBookDate, MaNguoiLap = Staff.MaNguoiDung };
                DataProvider.Ins.DB.PHIEUNHAPSACHes.Add(tmpPN);
                DataProvider.Ins.DB.SaveChanges();

                foreach (var v in Items)
                {
                    var tmpBook = new SACH() { MaDauSach = v.Book.MaDauSach, LuongTon = Amount };
                    DataProvider.Ins.DB.SACHes.Add(tmpBook);
                    DataProvider.Ins.DB.SaveChanges();

                    var tmpMBook = DataProvider.Ins.DB.DAUSACHes.First(x => x.MaDauSach == tmpBook.MaDauSach);
                    tmpMBook.LuongTon += Amount;
                    
                    var tmpCT_PN = new CT_PNS() { MaPhieuNhapSach = tmpPN.MaPhieuNhapSach, DonGiaNhap = v.InputPrice, MaSach = tmpBook.MaSach, SoLuong = Amount };
                    DataProvider.Ins.DB.CT_PNS.Add(tmpCT_PN);
                    DataProvider.Ins.DB.SaveChanges();
                }
                MessageBox.Show("Lập phiếu nhập sách thành công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (FlagIntent == 1)
            {
                var tmpPN = DataProvider.Ins.DB.PHIEUNHAPSACHes.First(x => x.MaPhieuNhapSach == Editor.MaPhieuNhapSach);
                { 
                    foreach(var v in DeletedCT_PN)
                    {
                        var tmpCT_PN = DataProvider.Ins.DB.CT_PNS.Where(x => x.MaCT_PNS == v).FirstOrDefault();
                        var tmpSach = DataProvider.Ins.DB.SACHes.Where(x => x.MaSach == tmpCT_PN.MaSach).FirstOrDefault();
                        if (DataProvider.Ins.DB.CT_HD.Where(x => x.MaSach == tmpSach.MaSach).Count() > 0)
                        {
                            MessageBox.Show("Sách đã sử dụng, không thể xóa dữ liệu nhập sách này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        var tmpMSach = DataProvider.Ins.DB.DAUSACHes.Where(x => x.MaDauSach == tmpSach.MaDauSach).FirstOrDefault();
                        tmpMSach.LuongTon -= tmpSach.LuongTon;
                        DataProvider.Ins.DB.SACHes.Remove(tmpSach);
                        DataProvider.Ins.DB.CT_PNS.Remove(tmpCT_PN);
                    }
                }
                if(Items.Count==0)
                {
                    DataProvider.Ins.DB.PHIEUNHAPSACHes.Remove(DataProvider.Ins.DB.PHIEUNHAPSACHes.Where(x => x.MaPhieuNhapSach == Editor.MaPhieuNhapSach).FirstOrDefault());
                }
                DataProvider.Ins.DB.SaveChanges();
                DeletedCT_PN = null;
                MessageBox.Show("Sửa phiếu nhập sách thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }   

        private void UpdateBookInfor()
        {
            if(SelectedBook==null)
            {
                return;
            }
            Authors = GetAuthorsString(SelectedBook.TACGIAs);
            Types = GetTypesString(SelectedBook.THELOAIs);
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

        private string GetAuthorsString(ICollection<TACGIA> tACGIAs)
        {
            string tmpauthor = "";
            foreach (var v in tACGIAs)
            {
                tmpauthor += (tmpauthor == "" ? "" : ", ") + v.TenTacGia;
            }
            return tmpauthor;
        }


        public void LoadData()
        {
            if (FlagIntent == 1)
            {
                Items = new ObservableCollection<Item_CT_PNS> { };
                EntryBookDate = Editor.NgayNhap;
                Staff = DataProvider.Ins.DB.NGUOIDUNGs.First(x => x.MaNguoiDung == Editor.MaNguoiLap);
                foreach(var v in Editor.CT_PNS)
                {
                    var tmpMBook = DataProvider.Ins.DB.DAUSACHes.First(x => x.MaDauSach == v.SACH.MaDauSach);
                    Items.Add(new Item_CT_PNS()
                    {
                        ID = Items.Count + 1,
                        Book = tmpMBook,
                        Authors = GetAuthorsString(tmpMBook.TACGIAs),
                        Types = GetTypesString(tmpMBook.THELOAIs),
                        InputPrice = v.DonGiaNhap,
                        Amount = v.SoLuong??0,
                        IntoMoney = Rules.Instance.ConvertDecimal_nullToInt64(v.DonGiaNhap) * Rules.Instance.ConvertInt_nullToInt64(v.SoLuong),
                        MaSachInNeed = v.MaSach, 
                        MaCTPNSInNeed = v.MaCT_PNS
                    });
                }
                foreach(var v in Items)
                {
                    SumAmount += v.IntoMoney ?? 0;
                }
                return;
            }
        }

        private bool AddDetailNeed()
        {
            if (SelectedBook == null || Amount ==0 || InputPrice == 0 )
            {
                return false;

            }
            return true;
        }

        private void AddDetail()
        {
            var minRemain = 0;
            var nowRemain = SelectedBook.LuongTon ?? 0;
            if (DataProvider.Ins.DB.THAMSOes.ToList().Count() > 0)
            {
                minRemain = DataProvider.Ins.DB.THAMSOes.ToList().Last().LuongTonToiThieu ?? 0;
            }
            if(nowRemain > minRemain)
            {
                MessageBox.Show(string.Format("Lượng tồn của sách phải ít hơn {0} để nhập sách! \n\nLượng tồn hiện tại: {1}", minRemain, nowRemain), "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var minAmount = 0;
            if (DataProvider.Ins.DB.THAMSOes.ToList().Count() > 0)
            {
                minAmount = DataProvider.Ins.DB.THAMSOes.ToList().Last().LuongNhapToiThieu ?? 0;
            }
            if (Amount < minAmount)
            {
                MessageBox.Show(string.Format("Lượng nhập tối thiểu là: {0} ", minAmount), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var tmp = new Item_CT_PNS()
            {
                Book = SelectedBook,
                Amount = Amount,
                ID = Items.Count() + 1,
                Authors = Authors,
                Types = Types,
                InputPrice = InputPrice,
                IntoMoney = IntoMoney
            };
            Items.Add(tmp);

            UpdateSumAmount();
        }

        private void UpdateSumAmount()
        {
            SumAmount = 0;
            if(Items == null)
            {
                return;
            }
            foreach (var v in Items)
            {
                SumAmount += v.IntoMoney ?? 0;
            }
        }

        private void UpdateIntoMoney()
        {
            if (SelectedBook == null)
            {
                return;
            }
            IntoMoney = Amount * InputPrice;

        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            FlagIntent = 0;
            SelectedBook = null;
            Types = string.Empty;
            Authors = string.Empty;
            IntoMoney = 0;
            Amount = 0;
            InputPrice = 0;
            EntryBookDate = null;
            Items = null;
            //ListBook = null;
            Staff = null;
            Editor = null;
            SumAmount = 0;
            SelectedDetail = null;
            DeletedCT_PN = null;
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand InputPriceTextChangedCommand { get; set; }
        public ICommand AddDetailClickCommand { get; set; }
        public ICommand BookNameSelectionChangedCommand { get; set; }
        public ICommand SaveButtonClickCommand { get; set; }
        public ICommand ItemListDetailSelectionChangedCommand { get; set; }
        public ICommand EditDetailClickCommand { get; set; }
        public ICommand DeleteDetailClickCommand { get; set; }
        public ICommand AmountTextChangedCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private int _FlagIntent;
        private DAUSACH _SelectedBook;
        private string _Types;
        private string _Authors;
        private Decimal _IntoMoney;
        private int _Amount;
        private Decimal _InputPrice;
        private DateTime? _EntryBookDate;
        private ObservableCollection<Item_CT_PNS> _Items;
        private ObservableCollection<DAUSACH> _ListBook;
        private NGUOIDUNG _Staff;
        private PHIEUNHAPSACH _Editor;
        private decimal _SumAmount;
        private Item_CT_PNS _SelectedDetail;
        private List<int> _DeletedCT_PN;

        public int FlagIntent { get => _FlagIntent; set => _FlagIntent = value; }
        public DAUSACH SelectedBook { get => _SelectedBook; set { _SelectedBook = value; OnPropertyChanged(); } }
        public string Types { get => _Types; set { _Types = value; OnPropertyChanged(); } }
        public string Authors { get => _Authors; set { _Authors = value; OnPropertyChanged(); } }
        public Decimal IntoMoney  { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }
        public int Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
        public DateTime? EntryBookDate { get => _EntryBookDate; set { _EntryBookDate = value; OnPropertyChanged(); } }
        public ObservableCollection<DAUSACH> ListBook { get => _ListBook; set { _ListBook = value; OnPropertyChanged(); } }
        public ObservableCollection<Item_CT_PNS> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }
        public NGUOIDUNG Staff { get => _Staff; set { _Staff = value; OnPropertyChanged(); } }
        public PHIEUNHAPSACH Editor { get => _Editor; set => _Editor = value; }
        public decimal SumAmount { get => _SumAmount; set { _SumAmount = value; OnPropertyChanged(); } }

        public Item_CT_PNS SelectedDetail { get => _SelectedDetail; set { _SelectedDetail = value; OnPropertyChanged(); } }

        public List<int> DeletedCT_PN { get => _DeletedCT_PN; set => _DeletedCT_PN = value; }
        public decimal InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }
    }
}
