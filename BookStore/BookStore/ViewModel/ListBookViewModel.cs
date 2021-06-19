using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModel
{
    public class ListBookViewModel : BaseViewModel
    {
        
        public ListBookViewModel()
        {
            //ListBooks = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes.Where(x => x.LuongTon > 0));
            Temp = new ObservableCollection<string>();
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto"); Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto"); Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
        }

        private ObservableCollection<DAUSACH> _ListBooks;
        private ObservableCollection<string> _temp;

        public ObservableCollection<DAUSACH> ListBooks { get => _ListBooks; set => _ListBooks = value; }
        public ObservableCollection<string> Temp { get => _temp; set => _temp = value; }
    }
}
