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
    public class ListUserViewModel : BaseViewModel
    {
        public ListUserViewModel()
        {
            ListUser = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
            SelectedUser = ListUser[0];
            GroupUser = new List<int>();
            foreach (var item in DataProvider.Ins.DB.NHOMNGUOIDUNGs)
            {
                GroupUser.Add(item.MaNhom);
            }
            ButtonEditClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Edit(); });
            ButtonDeleteClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Delete(); });
            ButtonAddClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Add(); });
            CloseWindowCommand = new RelayCommand<ListUserWindow>((p) => { return true; }, (p) => { this.CleanUpData(); });
        }
        
        public void Edit()
        {
            if(SelectedUser.MatKhau == "" || SelectedUser.MatKhau == null)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu !");
            }
            else
            {
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Lưu thay đổi thành công");
            }    
        }
        public void Delete()
        {
            var nguoidung = SelectedUser;
            ListUser.Remove(nguoidung);
            DataProvider.Ins.DB.NGUOIDUNGs.Remove(nguoidung);
            SelectedUser = ListUser[0];
        }
        public void Add()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.ShowDialog();
            ListUser = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
            SelectedUser = ListUser[0];
            GroupUser = new List<int>();
            foreach (var item in DataProvider.Ins.DB.NHOMNGUOIDUNGs)
            {
                GroupUser.Add(item.MaNhom);
            }
        }
        public override void CleanUpData()
        {
            base.CleanUpData();
            ListUser = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
            SelectedUser = ListUser[0];
            GroupUser = new List<int>();
            foreach (var item in DataProvider.Ins.DB.NHOMNGUOIDUNGs)
            {
                GroupUser.Add(item.MaNhom);
            }
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand ButtonEditClickCommand { get; set; }
        public ICommand ButtonDeleteClickCommand { get; set; }
        public ICommand ButtonAddClickCommand { get; set; }

        private ObservableCollection<NGUOIDUNG> _ListUser;
        private NGUOIDUNG _SelectedUser;
        private List<int> _GroupUser;


        public ObservableCollection<NGUOIDUNG> ListUser { get => _ListUser; set { _ListUser = value; OnPropertyChanged(); } }
        public NGUOIDUNG SelectedUser { get => _SelectedUser; set { _SelectedUser = value; OnPropertyChanged(); } }
        public List<int> GroupUser { get => _GroupUser; set { _GroupUser = value; OnPropertyChanged(); } }
    }
}
