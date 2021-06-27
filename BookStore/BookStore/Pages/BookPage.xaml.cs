using BookStore.View;
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
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public BookPage()
        {
            InitializeComponent();
        }

        private void Btn_themDauSach(object sender, RoutedEventArgs e)
        {
            (Grid as Grid).Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddBookWindow();
            tmp.ShowDialog();

            // Splash.Visibility = Visibility.Collapsed;
            (Grid as Grid).Effect = null;

        }
    }
}
