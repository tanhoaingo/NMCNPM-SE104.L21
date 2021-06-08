using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStore.Model;

namespace BookStore.ViewModel
{
    public class InvoiceViewModel :BaseViewModel
    {
        public ObservableCollection<CT_HD> IsSelected { get; set; }
        public ObservableCollection<CT_HD> Items { get; set; }

        public IEnumerable<string> TheLoai => new[] { "One piece", "Naruto", "Fairy Tail", "One punch man", "Con meo","Con cho", "Con lon",
        "One piece", "Naruto", "Fairy Tail", "One punch man", "Con meo","Con cho", "Zdragom" };

        public string SelectedItem { get; set; }

        public InvoiceViewModel()
        {
            Items = CreateData();

        }

        private static ObservableCollection<CT_HD> CreateData()
        {
            return new ObservableCollection<CT_HD>
            {
                new CT_HD
                {
                    MaSach = 1,
                    SoLuong = 10,
                    DonGiaBan = 1000,
                },
                new CT_HD
                {
                    MaSach = 2,
                    SoLuong = 20,
                    DonGiaBan = 2000,
                },
                new CT_HD
                {
                    MaSach = 2,
                    SoLuong = 20,
                    DonGiaBan = 2000,
                }

            };
        }
    }
}
