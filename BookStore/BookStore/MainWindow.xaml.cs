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
using System.Data;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Username.Text != "")
                tbUsername.Text = "";
            else
                tbUsername.Text = "Username";
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Pass.Password != "")
                tbPass.Text = "";
            else
                tbPass.Text = "Password";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DataSet1TableAdapters.ACCOUNTTableAdapter adp = new DataSet1TableAdapters.ACCOUNTTableAdapter();
            DataTable table = adp.GetData();
            string user = null;
            string pass = null;
            foreach (DataRow row in table.Rows)
            {
                user = row["USERNAME"].ToString();
                pass = row["PASSWORD"].ToString();
                if (user == Username.Text && pass == Pass.Password.ToString())
                {
                    Welcome welcome = new Welcome();
                    welcome.Show();
                    this.Close();
                }
            }
            
        }
    }
}
