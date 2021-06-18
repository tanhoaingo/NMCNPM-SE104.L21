using System.Windows;
using System.Windows.Controls;

namespace BookStore.Pages
{
    /// <summary>
    /// Interaction logic for BaoCaoPage.xaml
    /// </summary>
    public partial class BaoCaoPage : Page
    {
        public BaoCaoPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
            grid.Width = 610;
            grid.Height = 410;
            ((MainWindow)Application.Current.MainWindow).MainFrame.GoBack();
        }
    }
}
