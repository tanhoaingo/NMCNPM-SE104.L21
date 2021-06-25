using BookStore.Model;
using BookStore.Tools;
using System;
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
            ListBook = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);
            Items = CreateData();

            IntoMoneyNeedChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { changeIntoMoney(); });
            AddDetailClickCommand = new RelayCommand<object>((p) => { return AddDetailNeed(); }, (p) => { AddDetail(); });
            BookNameSelectionChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UpdateBookInfor(); });
            SaveButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SaveBookEntry(); });
        }

        private void SaveBookEntry()
        {
            if (Items.Count() == 0 || EntryBookDate == null)
            {
                MessageBox.Show("Vui lòng hoàn thành hóa đơn!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var tmpPN = new PHIEUNHAPSACH() { NgayNhap = EntryBookDate };
            DataProvider.Ins.DB.PHIEUNHAPSACHes.Add(tmpPN);

            foreach (var v in Items)
            {
                var tmpBook = new SACH() { MaDauSach = v.Book.MaDauSach, LuongTon = int.Parse(Amount) };
                DataProvider.Ins.DB.SACHes.Add(tmpBook);

                var tmpCT_PN = new CT_PNS() { MaPhieuNhapSach = tmpPN.MaPhieuNhapSach, DonGiaNhap = decimal.Parse(v.InputPrice), MaSach = tmpBook.MaSach, SoLuong = int.Parse(Amount) };
                DataProvider.Ins.DB.CT_PNS.Add(tmpCT_PN);
            }
            DataProvider.Ins.DB.SaveChanges();
        }

        private void UpdateBookInfor()
        {
            if(SelectedBook==null)
            {
                return;
            }
            string tmptype = "";
            string tmpauthor = "";
            foreach (var v in SelectedBook.TACGIAs)
            {
                tmpauthor += (tmpauthor == "" ? "" : ",") + v.TenTacGia;
            }
            Authors = tmpauthor;
            foreach (var v in SelectedBook.THELOAIs)
            {
                tmptype += (tmptype == "" ? "" : ",") + v.TenTheLoai;
            }
            Types = tmptype;
        }

        private static ObservableCollection<Item_CT_PNS> CreateData()
        {
            return new ObservableCollection<Item_CT_PNS> { };
        }

        private bool AddDetailNeed()
        {
            if (SelectedBook == null || string.IsNullOrEmpty(Amount) || string.IsNullOrEmpty(InputPrice))
            {
                return false;

            }
            return true;
        }

        private void AddDetail()
        {
            var tmp = new Item_CT_PNS() { Book = SelectedBook, Amount = Amount, ID = Items.Count() + 1, Authors = Authors, InputPrice = InputPrice, IntoMoney = IntoMoney };
            Items.Add(tmp);
        }

        private void changeIntoMoney()
        {
            if (SelectedBook == null)
            {
                return;
            }
            IntoMoney = Rules.Instance.ConvertStringAmountToInt64(Amount) * Rules.Instance.ConvertStringAmountToInt64(_InputPrice);

        }

        public ICommand IntoMoneyNeedChangedCommand { get; set; }
        public ICommand AddDetailClickCommand { get; set; }
        public ICommand BookNameSelectionChangedCommand { get; set; }
        public ICommand SaveButtonClickCommand { get; set; }

        private DAUSACH _SelectedBook;
        private string _Types;
        private string _Authors;
        private Int64 _IntoMoney;
        private string _Amount;
        private string _InputPrice;
        private DateTime? _EntryBookDate;
        private ObservableCollection<Item_CT_PNS> _Items;
        private ObservableCollection<DAUSACH> _ListBook;
        private NGUOIDUNG _Staff;

        public DAUSACH SelectedBook { get => _SelectedBook; set { _SelectedBook = value; OnPropertyChanged(); } }
        public string Types { get => _Types; set { _Types = value; OnPropertyChanged(); } }
        public string Authors { get => _Authors; set { _Authors = value; OnPropertyChanged(); } }
        public long IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
        public DateTime? EntryBookDate { get => _EntryBookDate; set { _EntryBookDate = value; OnPropertyChanged(); } }
        public string InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }
        public ObservableCollection<DAUSACH> ListBook { get => _ListBook; set { _ListBook = value; OnPropertyChanged(); } }
        public ObservableCollection<Item_CT_PNS> Items { get => _Items; set => _Items = value; }
        public NGUOIDUNG Staff { get => _Staff; set { _Staff = value; OnPropertyChanged(); } }
    }
}
