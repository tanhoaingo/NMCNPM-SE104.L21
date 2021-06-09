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
    /// Interaction logic for ThayDoiPage.xaml
    /// </summary>
    public partial class ThayDoiPage : Page
    {
        public ThayDoiPage()
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
