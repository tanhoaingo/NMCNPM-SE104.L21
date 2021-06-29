using BookStore.Pages;
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
    /// Interaction logic for BookEntryWindow.xaml
    /// </summary>
    public partial class BookEntryWindow : Window
    {
        public BookEntryWindow()
        {
            InitializeComponent();
            PEPage = new BookEntryPage();
            this.Main.Content = PEPage;
        }

        private BookEntryPage _PEPage;

        public BookEntryPage PEPage { get => _PEPage; set => _PEPage = value; }
    }
}
