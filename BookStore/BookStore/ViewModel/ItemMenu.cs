using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class ItemMenu

    {
        public string Header { get; set; }
        public PackIconKind Icon { get; set; }
        public List<SubItem> SubItems { get; set; }
        public UserControl Screen { get; set; }

        public ItemMenu(string header , List<SubItem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }
        public ItemMenu(string header , UserControl screen, PackIconKind icon)
        {
            Header = header;
            Screen = screen;
            Icon = icon;
        }

    }
}
