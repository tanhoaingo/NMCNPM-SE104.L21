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
        private string _Types;
        private int _Amount;
        private decimal? _InputPrice;
        private decimal? _IntoMoney;
        private int _ID;
        private int _MaSachInNeed;
        private int _MaCTPNSInNeed;


        public DAUSACH Book { get => _Book; set { _Book = value; OnPropertyChanged(); } }
        public string Authors { get => _Authors; set { _Authors = value; OnPropertyChanged(); } }
        public string Types { get => _Types; set { _Types = value; OnPropertyChanged(); } }
        public int Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
        public decimal? InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }
        public decimal? IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        public int MaSachInNeed { get => _MaSachInNeed; set => _MaSachInNeed = value; }
        public int MaCTPNSInNeed { get => _MaCTPNSInNeed; set => _MaCTPNSInNeed = value; }
    }

}
