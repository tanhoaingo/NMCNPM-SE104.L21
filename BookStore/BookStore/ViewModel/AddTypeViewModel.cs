using BookStore.Model;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BookStore.ViewModel
{
    public class AddTypeViewModel: BaseViewModel
    {
        public AddTypeViewModel()
        { 
            FlagIsSaved = false;

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { this.CleanUpData(); });
            TypeNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { TypeNameTextChanged(p); });
            ConfirmButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ConfirmAddType(p); });
            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelButtonClick(p); });
        }

        private void CancelButtonClick(Window p)
        {
            if (FlagIsSaved || string.IsNullOrEmpty(TypeName))
            {
                p.Close();
            }
            else
            {
                var result = MessageBox.Show("Bạn có muốn lưu thay đổi? ", "Thông báo", MessageBoxButton.YesNoCancel, MessageBoxImage.Error, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmAddType(p);
                    p.Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    p.Close();
                }
                else
                {
                    return;
                }
            }
        }

        private void ConfirmAddType(Window p)
        {
            var tmpwd = p as AddTypeWindow;
            DataProvider.Ins.DB.THELOAIs.Add(new THELOAI() { TenTheLoai = TypeName });
            DataProvider.Ins.DB.SaveChanges();
            FlagIsSaved = true;

            #region UI changes
            tmpwd.ErrorAddType.Text = "Thêm thể loại thành công!";
            tmpwd.ErrorAddType.Foreground = Brushes.Green;
            tmpwd.ErrorAddType.Visibility = Visibility.Visible;
            #endregion
        }

        private void TypeNameTextChanged(Window p)
        {
            var tmpwd = p as AddTypeWindow;

            #region UI changes
            tmpwd.ErrorAddType.Visibility = Visibility.Hidden;
            #endregion
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            TypeName = string.Empty;
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand TypeNameTextChangedCommand { get; set; }
        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private string _TypeName;
        private bool _FlagIsSaved;

        public string TypeName { get => _TypeName; set => _TypeName = value; }
        public bool FlagIsSaved { get => _FlagIsSaved; set => _FlagIsSaved = value; }
    }
}
