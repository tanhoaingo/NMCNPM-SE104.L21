using BookStore.Model;
using BookStore.Pages;
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
using System.Windows.Media.Effects;

namespace BookStore.ViewModel
{
    public class ListBookViewModel : BaseViewModel
    {
        
        public ListBookViewModel()
        {
            LoadListBooks();
            #region Test
            /*Temp = new ObservableCollection<string>();
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");*/
            #endregion

            AddBookButtonClickCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { AddNewBook(p); });
            EditBookButtonClickCommand = new RelayCommand<Page>((p) => { return EditBookNeed(); }, (p) => { EditBook(p); });
            DeleteBookButtonClickCommand = new RelayCommand<Page>((p) => { return DeleteBookNeed(); }, (p) => { DeleteBook(p); });
        }

        private bool DeleteBookNeed()
        {
            return SelectedMBook != null;
        }

        private void DeleteBook(Page p)
        {
            var tmp = DataProvider.Ins.DB.SACHes.Where(x => x.MaDauSach == SelectedMBook.MaDauSach);
            if (tmp.Count() > 0)
            {
                MessageBoxResult tmpRel = MessageBox.Show("Không thể xóa vì đã sử dụng dữ liệu đầu sách, bạn có muốn chuyển đầu sách vào lưu trữ \n" +
                    "(Những đầu sách lưu trữ sẽ không còn khả thi nhưng vẫn hiển thị đối với dữ liệu đã tạo)", "Thông báo",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (tmpRel == MessageBoxResult.OK)
                {
                    var tmpMBook = DataProvider.Ins.DB.DAUSACHes.Where(x => x.MaDauSach == SelectedMBook.MaDauSach).FirstOrDefault();
                    tmpMBook.TrangThai = 1;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListBooks();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBoxResult tmpRel = MessageBox.Show("Xác nhận xóa đầu sách", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (tmpRel == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.DAUSACHes.Remove(SelectedMBook);
                    SelectedMBook = null;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadListBooks();
                }
                else
                {
                    return;
                }
            }
        }

        private bool EditBookNeed()
        {
            return SelectedMBook != null;
        }

        public void LoadListBooks()
        {
            ListBooks = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);
        }

        public void LoadPage(BookPage p)
        {
            if(FlagIntent == 0)
            {
                p.IfShowListBook.Visibility = Visibility.Visible;
                p.IfSelectBook.Visibility = Visibility.Hidden;
                p.ListBookTitle.Text = "Danh sách đầu sách ";
            }
            else if(FlagIntent == 1)
            {
                p.IfShowListBook.Visibility = Visibility.Hidden;
                p.IfSelectBook.Visibility = Visibility.Visible;
                p.ListBookTitle.Text = "Tra cứu sách";
            }
        }

        private void AddNewBook(Page p)
        {

            var tmpPg = p as BookPage ;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddBookWindow();
            var tmpVM = tmp.DataContext as AddBookViewModel;
            tmpVM.CleanUpData();
            tmpVM.FlagIntent = 0;
            tmp.ShowDialog();

            LoadListBooks();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }

        private void EditBook(Page p)
        {
            var tmpPg = p as BookPage;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddBookWindow();
            var tmpVM = tmp.DataContext as AddBookViewModel;
            tmpVM.CleanUpData();
            tmpVM.FlagIntent = 1;
            tmpVM.EditBook = SelectedMBook;
            tmp.ShowDialog();

            LoadListBooks();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }

        public ICommand AddBookButtonClickCommand { get; set; }
        public ICommand EditBookButtonClickCommand { get; set; }
        public ICommand DeleteBookButtonClickCommand { get; set; }

        private ObservableCollection<DAUSACH> _ListBooks;
        private ObservableCollection<string> _Temp;
        private int _FlagIntent;
        private DAUSACH _SelectedMBook;

        public ObservableCollection<DAUSACH> ListBooks { get => _ListBooks; set { _ListBooks = value; OnPropertyChanged(); } }
        public ObservableCollection<string> Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }
        public int FlagIntent { get => _FlagIntent; set => _FlagIntent = value; }
        public DAUSACH SelectedMBook { get => _SelectedMBook; set => _SelectedMBook = value; }
    }
}
