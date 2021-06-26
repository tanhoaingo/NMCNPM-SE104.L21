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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookStore.ViewModel;

namespace BookStore.View
{
    /// <summary>
    /// Interaction logic for NewMainWindow.xaml
    /// </summary>
    public partial class NewMainWindow : Window
    {
        Uri invoicePage;
        Uri bookEntryPage;

        public NewMainWindow()
        {
            InitializeComponent();
            invoicePage =new  Uri("../Pages/InvoicePage.xaml", UriKind.Relative);
            bookEntryPage =new Uri("../Pages/BookEntryPage.xaml", UriKind.Relative);


        }

        private void ListView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (grid as Grid).Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            AddCustomerWindow tmp = new AddCustomerWindow();
            tmp.ShowDialog();

            // Splash.Visibility = Visibility.Collapsed;
            (grid as Grid).Effect = null;
          

            //  MainFrame.Navigate(new Uri("../Pages/HomePage.xaml", UriKind.Relative));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*ListBookWindow tmp = new ListBookWindow();
            tmp.ShowDialog();*/

        } 
        private void Btn_traSach(object sender, RoutedEventArgs e)
        {
            ListBookWindow tmp = new ListBookWindow();
            tmp.ShowDialog();

        } 
        private void Btn_tacGia(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.RemoveBackEntry();
            MainFrame.NavigationService.Refresh();
            MainFrame.DataContext = null;
            MainFrame.Navigate(new Uri("../Pages/AuthorPage.xaml", UriKind.Relative));

        }

        private void Btn_phieunhap(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.RemoveBackEntry();
            MainFrame.NavigationService.Refresh();
            MainFrame.DataContext = null;
            MainFrame.Navigate(new Uri("../Pages/BookEntryPage.xaml", UriKind.Relative));
        }

        private void Btn_HoaDon(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.RemoveBackEntry();
            MainFrame.NavigationService.Refresh();
            MainFrame.Navigate(invoicePage);
        }
    }
}
