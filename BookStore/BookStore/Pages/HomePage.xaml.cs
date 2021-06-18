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
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            var parGrid = grid.Parent as Grid;
            var border = parGrid.Parent as Border;
            Window window = border.Parent as Window;
            NhapSachWindow thuTienWindow = new NhapSachWindow();
            window.Hide();
            thuTienWindow.ShowDialog();
            window.Show();
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
            var parGrid = grid.Parent as Grid;
            var border = parGrid.Parent as Border;
            Window window = border.Parent as Window;
            TraCuuWindow thuTienWindow = new TraCuuWindow();
            window.Hide();
            thuTienWindow.ShowDialog();
            window.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            var parGrid = grid.Parent as Grid;
            var border = parGrid.Parent as Border;
            Window window = border.Parent as Window;
            ThuTienWindow thuTienWindow = new ThuTienWindow();
            window.Hide();
            thuTienWindow.ShowDialog();
            window.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            var parGrid = grid.Parent as Grid;
            var border = parGrid.Parent as Border;
            Window window = border.Parent as Window;
            BaoCaoWindow thuTienWindow = new BaoCaoWindow();
            window.Hide();
            thuTienWindow.ShowDialog();
            window.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            var parGrid = grid.Parent as Grid;
            var border = parGrid.Parent as Border;
            Window window = border.Parent as Window;
            ThayDoiWindow thuTienWindow = new ThayDoiWindow();
            window.Hide();
            thuTienWindow.ShowDialog();
            window.Show();
        }
    }
}
