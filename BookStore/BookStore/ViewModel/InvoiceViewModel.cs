using BookStore.Model;
using BookStore.Pages;
using BookStore.Tools;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace BookStore.ViewModel
{
    public class InvoiceViewModel : BaseViewModel
    {
        public InvoiceViewModel()
        {
          
            _cardVisible = Visibility.Hidden;
            ListBook = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.LuongTon > 0));
            Items = CreateData();
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);


            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveInvoice(); });
            AddingNewItemCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { });
            AddCustomerClick = new RelayCommand<Page>((p) => { return true; }, (p) => { CreateNewCustomer(p); });
            NameCustomerSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { });
            BookSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateListPriceOfBook(); UpdateBookInfor(); });
            PriceSelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateIntoMoneyValue(); });
            AmountTextChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { UpdateIntoMoneyValue(); });
            AddDetailClickCommand = new RelayCommand<object>((p) => { return AddDetailButtonNeed(); }, (p) => { AddDetail(); UpdateResultAMount(); });
            EditDetailClickCommand = new RelayCommand<object>((p) => { return EditDetailButtonNeed(); }, (p) => { EditDetail(); UpdateResultAMount(); });
            DeleteDetailClickCommand = new RelayCommand<object>((p) => { return DeleteDetailButtonNeed(); }, (p) => { DeleteDetail(); UpdateResultAMount(); });
            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => CloseInvoice(p));
            SearchButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => SearchBook());
            ExitButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => ExitWindow(p));
            PaidAmountTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { UpdateResultAMount(); });
        }
        //toolbox coi ddaua m tool box laf cai lon` gi`cai de minh keo tha
        //thì m phải bỏ trong sự kiện
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

            Amount = (SelectedBook.LuongTon - 20).ToString();
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
            if (!SaveInvoiceNeed())
            {
                MessageBox.Show("Vui lòng hoàn thành hóa đơn!");
                return;
            }

            if (SaveInvoiceNeed() && LeftAmount > 20000)
            {
                //cái này tính tổng nợ chứ không phải nợ của mỗi hóa đơn này ok
                MessageBox.Show("Số tiền nợ không được vượt quá 20000");
                return;
            }

            var HoaDon = new HOADON() { MaKhachHang = SelectedCustomer.MaKhachHang, MaNguoiLap = 5, NgayLapHoaDon = InvoiceDate, SoTienTra = PaidAmount, ConLai = LeftAmount, TongTien = SumAmount };
            DataProvider.Ins.DB.HOADONs.Add(HoaDon);
            DataProvider.Ins.DB.SaveChanges();

            foreach (var v in Items)
            {

                var CTHD = new CT_HD() { DonGiaBan = SelectedPriceOfBook.DonGiaNhap, MaHoaDon = HoaDon.MaHoaDon, SoLuong = int.Parse(v.Amount), MaSach = SelectedPriceOfBook.MaSach };
                DataProvider.Ins.DB.CT_HD.Add(CTHD);
                DataProvider.Ins.DB.SaveChanges();
                v.IDinDataBase = CTHD.MaCT_HD;
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
            if (SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        private bool EditDetailButtonNeed()
        {
            if (SelectedItem == null || SelectedBook == null || string.IsNullOrEmpty(Amount) || SelectedPriceOfBook == null)
            {
                return false;
            }
            return true;
        }

        private void DeleteDetail()
        {
            Items.Remove(SelectedItem);
        }

        private void EditDetail()
        {
            SelectedItem.DauSach = SelectedBook;
            SelectedItem.Amount = Amount;
            SelectedItem.CTPNS = SelectedPriceOfBook;
            SelectedItem.IntoMoney = IntoMoney;
        }

        private void UpdateResultAMount()
        {
            SumAmount = 0;
            //PaidAmount = 0; sao lại set paid amount = 0 ủa 
            LeftAmount = 0;
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

        private void AddDetail()
        {
            if (int.Parse(Amount) > (SelectedBook.LuongTon - 20))
            {
                MessageBox.Show("Số lượng tồn không đủ!");
            }
            else
            {
                var CT_HD = new Item_CT_HD() { DauSach = SelectedBook, ID = Items.Count() + 1, Amount = Amount, CTPNS = SelectedPriceOfBook, IntoMoney = IntoMoney };
                Items.Add(CT_HD);
            }
        }

        private bool AddDetailButtonNeed()
        {
            if (SelectedBook == null || string.IsNullOrEmpty(Amount) || SelectedPriceOfBook == null)
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
            IntoMoney = Rules.Instance.ConvertStringAmountToInt64(Amount) * Rules.Instance.ConvertDecimal_nullToInt64(SelectedPriceOfBook.DonGiaNhap);
        }

        private void UpdateListPriceOfBook()
        {
            if (ListPriceOfBook == null)
            {
                ListPriceOfBook = new ObservableCollection<CT_PNS>();
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
                vv.DonGiaNhap = vv.DonGiaNhap * 105 / 100;
                ListPriceOfBook.Add(vv);
            }
        }

        private void CreateNewCustomer(Page p)
        {
            /*   (controls as Grid).Effect = new BlurEffect();
              // Splash.Visibility = Visibility.Visible;

               var dlg = new Window();

               dlg.ShowDialog();

               // Splash.Visibility = Visibility.Collapsed;
               (controls as Grid).Effect = null;

   */

            var tmpPg = p as InvoicePage;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddCustomerWindow();
            tmp.ShowDialog();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }
        private void SetCard(KHACHHANG value)
        {
            if(value!=null)
            {
                CardPhone = value.DienThoai; 
                CardVisible = Visibility.Visible;
                CardName = value.HoTenKhachHang;
            }
            else
            {
                CardVisible = Visibility.Hidden;
            }
        }

        private static ObservableCollection<Item_CT_HD> CreateData()
        {
            return new ObservableCollection<Item_CT_HD> { };
        }

        public ICommand SaveButtonClickCommand { get; set; }
        public ICommand Grid { get; set; }
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
        private Int64 _LeftAmount;
        private Item_CT_HD _SelectedItem;
        private DateTime? _InvoiceDate;
        private string _BookTypes;
        private string _cardName;
        private string _cardPhone;
        private Visibility _cardVisible;
        private Grid _grid;
        private string _StaffName;

        public ObservableCollection<Item_CT_HD> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }

        public ObservableCollection<DAUSACH> ListBook { get => _ListBook; set { _ListBook = value; } }

        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }

        public KHACHHANG SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; SetCard(value); OnPropertyChanged(); } }

        public KHACHHANG SelectedCustomer_2 { get => _SelectedCustomer_2; set { _SelectedCustomer_2 = value; OnPropertyChanged(); } }

        public DAUSACH SelectedBook { get => _SelectedBook; set { _SelectedBook = value; OnPropertyChanged(); } }

        public ObservableCollection<CT_PNS> ListPriceOfBook { get => _ListPriceOfBook; set { _ListPriceOfBook = value; OnPropertyChanged(); } }

        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }

        public CT_PNS SelectedPriceOfBook { get => _SelectedPriceOfBook; set { _SelectedPriceOfBook = value; OnPropertyChanged(); } }

        public Int64 IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }

        public long SumAmount { get => _SumAmount; set { _SumAmount = value; OnPropertyChanged(); } }

        public long PaidAmount { get => _PaidAmount; set { _PaidAmount = value; OnPropertyChanged(); } }

        public long LeftAMount { get => _LeftAMount; set { _LeftAMount = value; OnPropertyChanged(); } }
        
        public string CardName { get => _cardName; set { _cardName = value; OnPropertyChanged(); } }
        public string CardPhone { get => _cardPhone; set { _cardPhone = value; OnPropertyChanged(); } }
        public Visibility CardVisible { get => _cardVisible; set { _cardVisible = value; OnPropertyChanged(); } }
        public Grid GRID { get => _grid; set { _grid = value; OnPropertyChanged(); } }

        public Item_CT_HD SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedBook = SelectedItem.DauSach;
                    SelectedPriceOfBook = SelectedItem.CTPNS;
                    Amount = SelectedItem.Amount;
                    IntoMoney = SelectedItem.IntoMoney;
                }
            }
        }

        public DateTime? InvoiceDate { get => _InvoiceDate; set { _InvoiceDate = value; OnPropertyChanged(); } }

        public string BookTypes { get => _BookTypes; set { _BookTypes = value; OnPropertyChanged(); } }

        public string StaffName { get => _StaffName; set { _StaffName = value; OnPropertyChanged(); } }
    }
}
