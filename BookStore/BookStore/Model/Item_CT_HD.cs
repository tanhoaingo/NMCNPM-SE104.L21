using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Item_CT_HD
    {

        public Item_CT_HD()
        {
           
        }

        private string _BookName;
        private int _ID;
        private string _Category;
        private string _Amount;
        private Int64 _Price;
        private Int64 _IntoMoney;

        public string BookName { get => _BookName; set => _BookName = value; }
        public int ID { get => _ID; set => _ID = value; }
        public string Category { get => _Category; set => _Category = value; }
        public string Amount { get => _Amount; set => _Amount = value; }
        public Int64 Price { get => _Price; set => _Price = value; }
        public Int64 IntoMoney { get => _IntoMoney; set => _IntoMoney = value; }
    }
}
