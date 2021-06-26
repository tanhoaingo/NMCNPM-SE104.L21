using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace BookStore.ViewModel
{
    class NewMainViewModel :BaseViewModel
    {

        public NewMainViewModel()
        {
            BtnInvoiceCommand = new RelayCommand<object>((p) => { return true; }, (p) => 
             {
                 
             });

          
        }

    
        //IECommand
        public ICommand BtnInvoiceCommand { get; set; } 
        public ICommand mainSource { get; set; } 
        

        private void navigator()
        {
            foreach(Window w in Application.Current.Windows)
            {
               
            }
        }
    }
}
