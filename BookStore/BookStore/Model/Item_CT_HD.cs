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
        private Int64 _IntoMoney;
        private int _IDinDataBase;
        private CT_PNS _CTPNS;

        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
        public Int64 IntoMoney { get => _IntoMoney; set { _IntoMoney = value; OnPropertyChanged(); } }
        public int IDinDataBase { get => _IDinDataBase; set { _IDinDataBase = value; OnPropertyChanged(); } }
        public DAUSACH DauSach { get => _DauSach; set { _DauSach = value; OnPropertyChanged(); } }
        public CT_PNS CTPNS { get => _CTPNS; set { _CTPNS = value; OnPropertyChanged(); } }
    }
}
