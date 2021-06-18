using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookStore.Model;
using BookStore.Tools;

namespace BookStore.ViewModel
{



    public class InvoiceViewModel :BaseViewModel
    {
        

        public InvoiceViewModel()
        {
            ListBook = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.LuongTon > 0));
            Items = CreateData();
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            
            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveInvoice(); });
            AddingNewItemCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {  });
            AddCustomerClick = new RelayCommand<Object>((p) => { return true; }, (p) => { CreateNewCustomer(); });
            NameCustomerSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { });
            BookSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateListPriceOfBook(); });
            PriceSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateIntoMoneyValue(); });
            AmountTextChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateIntoMoneyValue(); });
            AddDetailClickCommand = new RelayCommand<object>((p) => { return AddDetailButtonNeed(); }, (p) => { AddDetail(); UpdateResultAMount(); });
            EditDetailClickCommand = new RelayCommand<object>((p) => { return EditDetailButtonNeed(); }, (p) => { EditDetail(); UpdateResultAMount();});
            DeleteDetailClickCommand = new RelayCommand<object>((p) => { return DeleteDetailButtonNeed(); }, (p) => { DeleteDetail(); UpdateResultAMount(); });
            CancelButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { /*do close create invoice*/ });

        }

        private void SaveInvoice()
        {
            var HoaDon = new HOADON() { MaKhachHang = SelectedCustomer.MaKhachHang, MaNguoiLap = 5, NgayLapHoaDon = InvoiceDate, SoTienTra = PaidAmount, ConLai = LeftAMount, TongTien = SumAmount };
            DataProvider.Ins.DB.HOADONs.Add(HoaDon);
            DataProvider.Ins.DB.SaveChanges();

            foreach(var v in Items)
            {

                var CTHD = new CT_HD() { DonGiaBan = SelectedPriceOfBook.DonGiaNhap, MaHoaDon = HoaDon.MaHoaDon, SoLuong = int.Parse(v.Amount), MaSach = SelectedPriceOfBook.MaSach };
            }
        }

        private bool DeleteDetailButtonNeed()
        {
            if(SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        private bool EditDetailButtonNeed()
        {
            if (SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        private void DeleteDetail()
        {
            //do delete selectedItem
        }

        private void EditDetail()
        {
            //do edit selectedItem
        }

        private void UpdateResultAMount()
        {
            SumAmount = 0;
            PaidAmount = 0;
            LeftAMount = 0;
            if (Items == null)
            {
                return;
            }
            foreach(var v in Items)
            {
                SumAmount += v.IntoMoney;
            }
            LeftAMount = SumAmount - PaidAmount;
        }

        private void AddDetail()
        {
            var CT_HD = new Item_CT_HD() { BookName = SelectedBook.TenSach, ID = ListBook.Count() + 1,Category = SelectedBook.THELOAI1.TenTheLoai, Amount = Amount,Price = Rules.Instance.ConvertDecimal_nullToInt64(SelectedPriceOfBook.DonGiaNhap), IntoMoney = IntoMoney };
            Items.Add(CT_HD);
        }

        private bool AddDetailButtonNeed()
        {
            if (SelectedBook == null || string.IsNullOrEmpty(Amount) || SelectedPriceOfBook == null )
            {
                return false;
            }
            return true;
        }

        private void UpdateIntoMoneyValue()
        {
            IntoMoney = Rules.Instance.ConvertStringAmountToInt64(Amount) * Rules.Instance.ConvertDecimal_nullToInt64(SelectedPriceOfBook.DonGiaNhap);
        }

        private void UpdateListPriceOfBook()
        {
            if(ListPriceOfBook ==null)
            {
                ListPriceOfBook = new ObservableCollection<CT_PNS>();
            }
            else
            {
                ListPriceOfBook.Clear();
            }
            var tmp = DataProvider.Ins.DB.CT_PNS.Where(x => x.SACH.DAUSACH.TenSach == SelectedBook.TenSach).ToList();
            if (tmp == null)
            { 
                return; 
            }
            foreach (var vv in tmp)
            {
                ListPriceOfBook.Add(vv);
            }
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


        private ObservableCollection<Item_CT_HD> _Items;
        private ObservableCollection<DAUSACH> _ListBook;
        private ObservableCollection<KHACHHANG> _ListCustomer;
        private KHACHHANG _SelectedCustomer;
        private KHACHHANG _SelectedCustomer_2;
        private DAUSACH _SelectedBook;
        private ObservableCollection<CT_PNS> _ListPriceOfBook;
        private string _Amount;
        private CT_PNS _SelectedPriceOfBook;
        private Int64 _IntoMoney;
        private Int64 _SumAmount;
        private Int64 _PaidAmount;
        private Int64 _LeftAMount;
        private Item_CT_HD _SelectedItem;
        private DateTime? _InvoiceDate;

        public ObservableCollection<Item_CT_HD> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }
        
        public ObservableCollection<DAUSACH> ListBook { get => _ListBook; set { _ListBook = value; } } 
        
        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }
        
        public KHACHHANG SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; OnPropertyChanged(); } }
        
        public KHACHHANG SelectedCustomer_2 { get => _SelectedCustomer_2; set { _SelectedCustomer_2 = value; OnPropertyChanged(); } }

        public DAUSACH SelectedBook { get => _SelectedBook; set { _SelectedBook = value; OnPropertyChanged(); } }

        public ObservableCollection<CT_PNS> ListPriceOfBook { get => _ListPriceOfBook; set { _ListPriceOfBook = value; OnPropertyChanged(); } }

        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }

        public CT_PNS SelectedPriceOfBook { get => _SelectedPriceOfBook; set { _SelectedPriceOfBook = value; OnPropertyChanged(); } }

        public Int64 IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }

        public long SumAmount { get => _SumAmount; set { _SumAmount = value; OnPropertyChanged(); } }

        public long PaidAmount { get => _PaidAmount; set { _PaidAmount = value; OnPropertyChanged(); } }

        public long LeftAMount { get => _LeftAMount; set { _LeftAMount = value; OnPropertyChanged(); } }

        public Item_CT_HD SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        public DateTime? InvoiceDate { get => _InvoiceDate; set => _InvoiceDate = value; }
    }
}
