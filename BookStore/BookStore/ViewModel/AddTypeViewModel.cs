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
            TypeNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { TypeNameTextChanged(p); });
            ConfirmButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ConfirmButtonClick(p); });
            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelButtonClick(p); });
        }

        private void CancelButtonClick(Window p)
        {
            p.Close();
        }

        private void ConfirmButtonClick(Window p)
        {
            var tmpwd = p as AddTypeWindow;
            DataProvider.Ins.DB.THELOAIs.Add(new THELOAI() { TenTheLoai = TypeName });
            DataProvider.Ins.DB.SaveChanges();
            tmpwd.ErrorAddType.Text = "Thêm thể loại thành công!";
            tmpwd.ErrorAddType.Foreground = Brushes.Green;
            tmpwd.ErrorAddType.Visibility = Visibility.Visible;
        }

        private void TypeNameTextChanged(Window p)
        {
            var tmpwd = p as AddTypeWindow;
            tmpwd.ErrorAddType.Visibility = Visibility.Hidden;
        }

        public ICommand TypeNameTextChangedCommand { get; set; }
        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private string _TypeName;

        public string TypeName { get => _TypeName; set => _TypeName = value; }
    }
}
