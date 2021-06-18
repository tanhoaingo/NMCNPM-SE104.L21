using BookStore.ViewModel;
using System;

namespace BookStore.Model
{
    public class Item_CT_PNS : BaseViewModel
    {
        public Item_CT_PNS()
        {
        }
        private DAUSACH _Book;
        private string _Authors;
        private string _Amount;
        private string _InputPrice;
        private Int64 _IntoMoney;
        private int _ID;


        public DAUSACH Book { get => _Book; set => _Book = value; }
        public string Authors { get => _Authors; set => _Authors = value; }
        public string Amount { get => _Amount; set => _Amount = value; }
        public string InputPrice { get => _InputPrice; set => _InputPrice = value; }
        public long IntoMoney { get => _IntoMoney; set => _IntoMoney = value; }
        public int ID { get => _ID; set => _ID = value; }
    }

}
