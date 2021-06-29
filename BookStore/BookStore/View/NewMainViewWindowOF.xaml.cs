using BookStore.DesignControls;
using BookStore.ViewModel;
using MaterialDesignThemes.Wpf;
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

namespace BookStore.View
{
    /// <summary>
    /// Interaction logic for NewMainViewWindowOF.xaml
    /// </summary>
    public partial class NewMainViewWindowOF : Window
    {
        public NewMainViewWindowOF()
        {
            InitializeComponent();

            /* var menuRes = new List<SubItem>();
             menuRes.Add(new SubItem("a1"));
             menuRes.Add(new SubItem("a2"));
             menuRes.Add(new SubItem("a3"));
             menuRes.Add(new SubItem("a4"));
             menuRes.Add(new SubItem("a5"));

             var item0 = new ItemMenu("some", menuRes, PackIconKind.Register);

             var menuRes1 = new List<SubItem>();
             menuRes.Add(new SubItem("a1"));
             menuRes.Add(new SubItem("a2"));
             menuRes.Add(new SubItem("a3"));
             menuRes.Add(new SubItem("a4"));
             menuRes.Add(new SubItem("a5"));

             var item1 = new ItemMenu("some", menuRes1, PackIconKind.Register);

             var menuRes2 = new List<SubItem>();
             menuRes.Add(new SubItem("a1"));
             menuRes.Add(new SubItem("a2"));
             menuRes.Add(new SubItem("a3"));
             menuRes.Add(new SubItem("a4"));
             menuRes.Add(new SubItem("a5"));

             var item2 = new ItemMenu("some", menuRes2, PackIconKind.Register);

             var menuRes3 = new List<SubItem>();
             menuRes.Add(new SubItem("a1"));
             menuRes.Add(new SubItem("a2"));
             menuRes.Add(new SubItem("a3"));
             menuRes.Add(new SubItem("a4"));
             menuRes.Add(new SubItem("a5"));

             var item3= new ItemMenu("some", menuRes3, PackIconKind.Register);

             var menuRes4 = new List<SubItem>();
             menuRes.Add(new SubItem("a1"));
             menuRes.Add(new SubItem("a2"));
             menuRes.Add(new SubItem("a3"));
             menuRes.Add(new SubItem("a4"));
             menuRes.Add(new SubItem("a5"));

             var item4 = new ItemMenu("some", menuRes4, PackIconKind.Register);


             var item5 = new ItemMenu("some", new UserControl(), PackIconKind.Register);

             menu.Children.Add(new UserControlMenuItem(item1));
             menu.Children.Add(new UserControlMenuItem(item2));
             menu.Children.Add(new UserControlMenuItem(item3));
             menu.Children.Add(new UserControlMenuItem(item5));
             menu.Children.Add(new UserControlMenuItem(item4));*/


            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("Customer"));
            menuRegister.Add(new SubItem("Providers"));
            menuRegister.Add(new SubItem("Employees"));
            menuRegister.Add(new SubItem("Products"));
            var item6 = new ItemMenu("Register", menuRegister, PackIconKind.Register);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Services"));
            menuSchedule.Add(new SubItem("Meetings"));
            var item1 = new ItemMenu("Appointments", menuSchedule, PackIconKind.Schedule);

            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Customers"));
            menuReports.Add(new SubItem("Providers"));
            menuReports.Add(new SubItem("Products"));
            menuReports.Add(new SubItem("Stock"));
            menuReports.Add(new SubItem("Sales"));
            var item2 = new ItemMenu("Reports", menuReports, PackIconKind.FileReport);

            var menuExpenses = new List<SubItem>();
            menuExpenses.Add(new SubItem("Fixed"));
            menuExpenses.Add(new SubItem("Variable"));
            var item3 = new ItemMenu("Expenses", menuExpenses, PackIconKind.ShoppingBasket);

            var menuFinancial = new List<SubItem>();
            menuFinancial.Add(new SubItem("Cash flow"));
            var item4 = new ItemMenu("Financial", menuFinancial, PackIconKind.ScaleBalance);

            var item0 = new ItemMenu("Dashboard", new UserControl(), PackIconKind.ViewDashboard);

            listmenu.Children.Add(new UserControlMenuItem(item0));
            listmenu.Children.Add(new UserControlMenuItem(item6));
            listmenu.Children.Add(new UserControlMenuItem(item1));
            listmenu.Children.Add(new UserControlMenuItem(item2));
            listmenu.Children.Add(new UserControlMenuItem(item3));
            listmenu.Children.Add(new UserControlMenuItem(item4));
        }
    }
}
