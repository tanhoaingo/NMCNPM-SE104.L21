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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore.Pages
{
    /// <summary>
    /// Interaction logic for InvoicePage.xaml
    /// </summary>
    public partial class InvoicePage : Page
    {
        public InvoicePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Grid as Grid).Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            AddCustomerWindow tmp = new AddCustomerWindow();
            tmp.ShowDialog();

            // Splash.Visibility = Visibility.Collapsed;
            (Grid as Grid).Effect = null;

        }
    }
}
