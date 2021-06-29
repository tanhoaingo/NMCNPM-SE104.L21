using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace BookStore.ViewModel
{
    public class ListCustomerViewModel: BaseViewModel
    {
        public ListCustomerViewModel()
        {
            LoadListCustomer();

            ListOptionsSearch = new ObservableCollection<string>();
            ListOptionsSearch.Add("Tất cả");
            ListOptionsSearch.Add("Tên");
            ListOptionsSearch.Add("Số điện thoại");
            SelectedOption = "Tất cả";


            ListCustomersCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListCustomer);
            ListCustomersCollectionView.Filter = FillerListBooks;

            OptionsSearchSelectionChangedCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { Refresh(p); });

            CreateBillButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CreateBill(); });
            AddCustomerButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CreateCustomer(); });
            EditCustomerButtonClickCommand = new RelayCommand<object>((p) => { return EditCustomerNeed(); }, (p) => { EditCustomer(); });
            DeleteCustomerButtonClickCommand = new RelayCommand<object>((p) => { return DeleteCustomerNeed(); }, (p) => { DeleteCustomer(); });
        }

        private void Refresh(Page p)
        {
            CollectionViewSource.GetDefaultView(ListCustomer).Refresh();
        }

        private void DeleteCustomer()
        {
            var tmp = DataProvider.Ins.DB.HOADONs.Where(x => x.MaKhachHang == SelectedItem.MaKhachHang);
            if(tmp.Count() > 0)
            {
                MessageBoxResult tmpRel = MessageBox.Show("Không thể xóa vì khách hàng đã thực hiện hóa đơn, bạn có muốn chuyển khách hàng vào lưu trữ \n" +
                    "(Những khách hàng lưu trữ sẽ không còn khả thi nhưng vẫn hiển thị đối với hóa đơn đã lập)", "Thông báo",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(tmpRel == MessageBoxResult.OK)
                {
                    var tmpCus = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == SelectedItem.MaKhachHang).FirstOrDefault();
                    tmpCus.TrangThai = 1;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListCustomer();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBoxResult tmpRel = MessageBox.Show("Xác nhận xóa khách hàng", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (tmpRel == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedItem);
                    SelectedItem = null;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListCustomer();
                }
                else
                {
                    return;
                }
            }
        }

        private bool DeleteCustomerNeed()
        {
            return SelectedItem != null;
        }

        private void LoadListCustomer()
        {
            ListCustomer = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        }

        private bool EditCustomerNeed()
        {
            return SelectedItem != null;
        }

        private void EditCustomer()
        {
            var tmpWD = new AddCustomerWindow();
            var tmpVM = (tmpWD.DataContext as AddCustomerViewModel);
            tmpVM.FLagItent = 1;
            tmpVM.EditID = SelectedItem.MaKhachHang;
            tmpVM.TenKhachHang = SelectedItem.HoTenKhachHang;
            tmpVM.SDT = SelectedItem.DienThoai;
            tmpVM.DiaChi = SelectedItem.DiaChi;
            tmpVM.Email = SelectedItem.Email;
            tmpVM.EditSDT = SelectedItem.DienThoai;
            tmpWD.ShowDialog();
            LoadListCustomer();
        }

        private void CreateCustomer()
        {
            var tmpWD = new AddCustomerWindow();
            (tmpWD.DataContext as AddCustomerViewModel).FLagItent = 0;
            tmpWD.ShowDialog();
            LoadListCustomer();
        }

        private void CreateBill()
        {
            var tmpWD = new BillWindow();
            (tmpWD.DataContext as BillViewModel).SelectedCustomer = SelectedItem;
            tmpWD.ShowDialog();
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            SelectedItem = null;
            ListCustomer = null;
        }



        public string NonUnicode(string text)
        {
            string s = text.ToLower();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ"};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y"};
            for (int i = 0; i < arr1.Length; i++)
            {
                s = s.Replace(arr1[i], arr2[i]);
            }
            return s;
        }

        private bool FillerListBooks(object obj)
        {

            if (String.IsNullOrEmpty(ListCustomersFiller))
            {
                return true;
            }

            if (obj is KHACHHANG kHACHHANG)
            {
                if (SelectedOption == "Số điện thoại")
                {
                    return NonUnicode(kHACHHANG.DienThoai).IndexOf(NonUnicode(ListCustomersFiller), StringComparison.OrdinalIgnoreCase) >= 0;
                }
                else if (SelectedOption == "Tên")
                {
                    return NonUnicode(kHACHHANG.HoTenKhachHang).IndexOf(NonUnicode(ListCustomersFiller), StringComparison.OrdinalIgnoreCase) >= 0;
                }
                else
                {
                    return NonUnicode(kHACHHANG.DienThoai).IndexOf(NonUnicode(ListCustomersFiller), StringComparison.OrdinalIgnoreCase) >= 0 ||
                        NonUnicode(kHACHHANG.HoTenKhachHang).IndexOf(NonUnicode(ListCustomersFiller), StringComparison.OrdinalIgnoreCase) >= 0;
                }

            }
            return false;
        }


        public ICollectionView ListCustomersCollectionView { get; set; }
        public ICommand OptionsSearchSelectionChangedCommand { get; set; }

        public ICommand CreateBillButtonClickCommand { get; set; }
        public ICommand AddCustomerButtonClickCommand { get; set; }
        public ICommand EditCustomerButtonClickCommand { get; set; }
        public ICommand DeleteCustomerButtonClickCommand { get; set; }


        private string _ListCustomersFiller = string.Empty;
        private string _SelectedOption;
        private ObservableCollection<string> _ListOptionsSearch;
        private KHACHHANG _SelectedItem;
        private ObservableCollection<KHACHHANG> _ListCustomer;


        public string ListCustomersFiller { get => _ListCustomersFiller; set { _ListCustomersFiller = value; OnPropertyChanged(nameof(_ListCustomersFiller)); ListCustomersCollectionView.Refresh(); } }
        public string SelectedOption { get => _SelectedOption; set { _SelectedOption = value; OnPropertyChanged(); } }
        public ObservableCollection<string> ListOptionsSearch { get => _ListOptionsSearch; set { _ListOptionsSearch = value; OnPropertyChanged(); } }
        public KHACHHANG SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
        public ObservableCollection<KHACHHANG> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }
    }
}
