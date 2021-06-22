using BookStore.Model;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class ListBookViewModel : BaseViewModel
    {
        
        public ListBookViewModel()
        {
            ListBooks = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);
            #region Test
            /*Temp = new ObservableCollection<string>();
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
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");
            Temp.Add("Naruto");*/
            #endregion

            AddBookButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddNewBook(); });
        }

        private void AddNewBook()
        {
            var tmp = new AddBookWindow();
            tmp.ShowDialog();
            
        }

        public ICommand AddBookButtonClickCommand { get; set; }

        private ObservableCollection<DAUSACH> _ListBooks;
        private ObservableCollection<string> _temp;

        public ObservableCollection<DAUSACH> ListBooks { get => _ListBooks; set => _ListBooks = value; }
        public ObservableCollection<string> Temp { get => _temp; set => _temp = value; }
    }
}
