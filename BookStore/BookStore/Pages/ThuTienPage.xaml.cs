using System.Windows;
using System.Windows.Controls;

namespace BookStore.Pages
{
    /// <summary>
    /// Interaction logic for ThuTienPage.xaml
    /// </summary>
    public partial class ThuTienPage : Page
    {
        public ThuTienPage()
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
