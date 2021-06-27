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

namespace BookStore.ViewModel
{
    public class AddUserViewModel : BaseViewModel
    {
        public AddUserViewModel()
        {
            User = new NGUOIDUNG();
            GroupUser = new List<int>();
            foreach (var item in DataProvider.Ins.DB.NHOMNGUOIDUNGs)
            {
                GroupUser.Add(item.MaNhom);
            }
            ButtonAddClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Add(); });
            CloseWindowCommand = new RelayCommand<AddUserWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });
        }

        public void Add()
        {
            if(User.MaNguoiDung == null || User.MatKhau == null || User.TenDangNhap == null || User.TenNguoiDung == null || User.MaNhom == null)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin !");
            }
            else
            {
                bool isExist = false;
                foreach (var item in DataProvider.Ins.DB.NGUOIDUNGs)
                {
                    if(item.TenDangNhap == User.TenDangNhap)
                    {                       
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                    MessageBox.Show("Tên đăng nhập đã tồn tại !");
                else
                { 
                    DataProvider.Ins.DB.NGUOIDUNGs.Add(User);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm người dùng thành công !");
                    User = new NGUOIDUNG();
                    GroupUser = new List<int>();
                    foreach (var item in DataProvider.Ins.DB.NHOMNGUOIDUNGs)
                    {
                        GroupUser.Add(item.MaNhom);
                    }
                }

            }
        }
        public override void CleanUpData()
        {
            base.CleanUpData();
            User = new NGUOIDUNG();
            GroupUser = new List<int>();
            foreach (var item in DataProvider.Ins.DB.NHOMNGUOIDUNGs)
            {
                GroupUser.Add(item.MaNhom);
            }
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand ButtonAddClickCommand { get; set; }

      
        private NGUOIDUNG _User;
        private List<int> _GroupUser;

       
        public NGUOIDUNG User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public List<int> GroupUser { get => _GroupUser; set { _GroupUser = value; OnPropertyChanged(); } }
    }
}
