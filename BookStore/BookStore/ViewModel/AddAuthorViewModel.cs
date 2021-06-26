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
            FlagIsSaved = false;

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { this.CleanUpData(); });
            AuthorNameTextChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AuthorNameTextChanged(p); });
            ConfirmButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ConfirmAddAuthor(p); });
            CancelButtonClickCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelButtonClick(p); });
        }

        private void CancelButtonClick(Window p)
        {
            if (FlagIsSaved || string.IsNullOrEmpty(AuthorName)) 
            {
                p.Close();
            }
            else
            {
                var result = MessageBox.Show("Bạn có muốn lưu thay đổi? ", "Thông báo", MessageBoxButton.YesNoCancel, MessageBoxImage.Error, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmAddAuthor(p);
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

        private void ConfirmAddAuthor(Window p)
        {
            var tmpwd = p as AddAuthorWindow;
            DataProvider.Ins.DB.TACGIAs.Add(new TACGIA() { TenTacGia = AuthorName });
            DataProvider.Ins.DB.SaveChanges();
            FlagIsSaved = true;


            #region UI changes
            tmpwd.ErrorAddAuthor.Text = "Thêm tác giả thành công!";
            tmpwd.ErrorAddAuthor.Foreground = Brushes.Green;
            tmpwd.ErrorAddAuthor.Visibility = Visibility.Visible;
            #endregion
        }

        private void AuthorNameTextChanged(Window p)
        {
            var tmpwd = p as AddAuthorWindow;

            #region UI changes
            tmpwd.ErrorAddAuthor.Visibility = Visibility.Hidden;
            #endregion
        }

        public override void CleanUpData()
        {
            base.CleanUpData();
            AuthorName = string.Empty;
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand AuthorNameTextChangedCommand { get; set; }
        public ICommand ConfirmButtonClickCommand { get; set; }
        public ICommand CancelButtonClickCommand { get; set; }

        private string _AuthorName;
        private bool _FlagIsSaved;

        public string AuthorName { get => _AuthorName; set => _AuthorName = value; }
        public bool FlagIsSaved { get => _FlagIsSaved; set => _FlagIsSaved = value; }
    }
}
