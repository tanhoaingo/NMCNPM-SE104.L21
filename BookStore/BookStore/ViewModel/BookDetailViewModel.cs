using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookStore.ViewModel
{
    class BookDetailViewModel : BaseViewModel
    {

        public DAUSACH EditBook { get => _EditBook; set => _EditBook = value; }
        private DAUSACH _EditBook;

        public BookDetailViewModel()
        {

        }

        public void LoadData()
        {
            BookName = EditBook.TenSach;
            Author = EditBook.TACGIAs.FirstOrDefault().TenTacGia;
            Type = EditBook.THELOAIs.FirstOrDefault().TenTheLoai;
            BookImage = ByteArrayToBitmapImage(EditBook.HinhAnhSach);
            LuongTon = EditBook.LuongTon ?? 0;
            MaDauSach = EditBook.MaDauSach.ToString();
            return;
        }
        public BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public string BookName { get => _bookName; set { _bookName = value; OnPropertyChanged(); } }
        private string _bookName;
        public int LuongTon { get => _luongton; set { _luongton = value; OnPropertyChanged(); } }
        private int _luongton;
        public string MaDauSach { get => _madausach; set { _madausach = value; OnPropertyChanged(); } }
        private string _madausach;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public string Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }
        public BitmapImage BookImage { get => _bookImage; set { _bookImage = value; OnPropertyChanged(); } }
        private string _Type;
        private string _Author;
        private BitmapImage _bookImage;

    }
}
