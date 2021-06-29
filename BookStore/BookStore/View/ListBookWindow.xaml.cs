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
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for ListBookWindow.xaml
    /// </summary>
    public partial class ListBookWindow : Window
    {
        public ListBookWindow()
        {
            InitializeComponent();

         /*   tmpWd.MainFrame.NavigationService.RemoveBackEntry();
            tmpWd.MainFrame.NavigationService.Refresh();
            tmpWd.MainFrame.DataContext = null;
            tmpWd.MainFrame.Navigate(new Uri("../Pages/BCTpage.xaml", UriKind.Relative));
            tmpWd.MainTitle.Text = "Thay đổi quy định";
            tmpWd.MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;*/
        }
    }
}
