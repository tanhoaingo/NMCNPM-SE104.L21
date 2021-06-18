using System;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((Application.Current.MainWindow != null))
            {
                var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
                grid.Width = 1600;
                grid.Height = 950;
                ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(
                    new Uri("../Pages/BookEntryPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            grid.Width = 1600;
            grid.Height = 950;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(
                new Uri("../Pages/InvoicePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            grid.Width = 1600;
            grid.Height = 1000;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(
                new Uri("../Pages/TraCuuPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            grid.Width = 1600;
            grid.Height = 1000;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(
                new Uri("../Pages/ThuTienPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            grid.Width = 1600;
            grid.Height = 1000;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(
                new Uri("../Pages/BaoCaoPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            grid.Width = 1600;
            grid.Height = 1000;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(
                new Uri("../Pages/ThayDoiPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
