using System.Windows;
using System.Windows.Controls;

namespace BookStore.Pages
{
    /// <summary>
    /// Interaction logic for NhapSachPage.xaml
    /// </summary>
    public partial class BookEntryPage : Page
    {
        public BookEntryPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (Application.Current.MainWindow != null)
            {
                var grid = ((MainWindow)Application.Current.MainWindow).MainFrame.Parent as Grid;
                grid.Width = 610;
                grid.Height = 410;
                ((MainWindow)Application.Current.MainWindow).MainFrame.GoBack();
            }
        }
    }
}
