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
    public class AddAuthorViewModel:BaseViewModel
    {
        public AddAuthorViewModel()
        {
            AuthorNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AuthorNameTextChanged(p); });
            ConfirmButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ConfirmButtonClick(p); });
            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelButtonClick(p); });
        }

        private void CancelButtonClick(Window p)
        {
            p.Close();
        }

        private void ConfirmButtonClick(Window p)
        {
            var tmpwd = p as AddAuthorWindow;
            DataProvider.Ins.DB.TACGIAs.Add(new TACGIA() { TenTacGia = AuthorName });
            DataProvider.Ins.DB.SaveChanges();
            tmpwd.ErrorAddAuthor.Text = "Thêm tác giả thành công!";
            tmpwd.ErrorAddAuthor.Foreground = Brushes.Green;
            tmpwd.ErrorAddAuthor.Visibility = Visibility.Visible;
        }

        private void AuthorNameTextChanged(Window p)
        {
            var tmpwd = p as AddAuthorWindow;
            tmpwd.ErrorAddAuthor.Visibility = Visibility.Hidden;
        }

        public ICommand AuthorNameTextChangedCommand { get; set; }
        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private string _AuthorName;

        public string AuthorName { get => _AuthorName; set => _AuthorName = value; }
    }
}
