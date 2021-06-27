using BookStore.Model;
using BookStore.Pages;
using BookStore.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

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

            AddBookButtonClickCommand = new RelayCommand<Page>((p) => { return true; }, (p) => { AddNewBook(p); });
        }

        private void AddNewBook(Page p)
        {

            var tmpPg = p as BookPage ;

            tmpPg.Grid.Effect = new BlurEffect();

            // Splash.Visibility = Visibility.Visible;

            var tmp = new AddBookWindow();
            tmp.ShowDialog();

            // Splash.Visibility = Visibility.Collapsed;
            tmpPg.Grid.Effect = null;
        }

        public ICommand AddBookButtonClickCommand { get; set; }

        private ObservableCollection<DAUSACH> _ListBooks;
        private ObservableCollection<string> _temp;

        public ObservableCollection<DAUSACH> ListBooks { get => _ListBooks; set => _ListBooks = value; }
        public ObservableCollection<string> Temp { get => _temp; set => _temp = value; }
    }
}
