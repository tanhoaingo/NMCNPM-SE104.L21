using BookStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for ListBookWindow.xaml
    /// </summary>
    public partial class ListBookWindow : Window
    {
        private BookPage bookPage;

        public BookPage BPage { get => bookPage; set => bookPage = value; }

        public ListBookWindow()
        {
            InitializeComponent();
            BPage = new BookPage();
            this.Main.Content =  BPage;
            /*this.Main.NavigationService.RemoveBackEntry();
            this.Main.NavigationService.Refresh();
            this.Main.DataContext = null;
            this.Main.Navigate(new Uri("../Pages/BookPage.xaml", UriKind.Relative));
            this.Title = "Thay đổi quy định";
            this.Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;*/
        }
    }
}
