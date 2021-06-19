using BookStore.View;
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
            var tmp = new BookEntryWindow();
            tmp.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var tmp = new InvoiceWindow();
            tmp.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var tmp = new BillWindow();
            tmp.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
