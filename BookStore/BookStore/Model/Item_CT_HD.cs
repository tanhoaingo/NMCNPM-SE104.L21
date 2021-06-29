using BookStore.ViewModel;
using System;

namespace BookStore.Model
{
    public class Item_CT_HD : BaseViewModel
    {

        public Item_CT_HD()
        {

        }
        private DAUSACH _DauSach;
        private int _ID;
        private string _Amount;
        private decimal _IntoMoney;
        private int _IDinDataBase;
        private CT_PNS _CTPNS;
        private string _BookTypes;
        private decimal? _InputPrice;
        private decimal? _OutputPrice;
        private int _MaSachInNeed;
        private int _MaCTHDInNeed;

        public decimal? OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }
        public int MaSachInNeed { get => _MaSachInNeed; set => _MaSachInNeed = value; }
        public int MaCTHDInNeed { get => _MaCTHDInNeed; set => _MaCTHDInNeed = value; }
        public decimal? InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }
        public string BookTypes { get => _BookTypes; set { _BookTypes = value; OnPropertyChanged(); } }
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
        public decimal IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }
        public int IDinDataBase { get => _IDinDataBase; set { _IDinDataBase = value; OnPropertyChanged(); } }
        public DAUSACH DauSach { get => _DauSach; set { _DauSach = value; OnPropertyChanged(); } }
        public CT_PNS CTPNS { get => _CTPNS; set { _CTPNS = value; OnPropertyChanged(); } }
    }
}
