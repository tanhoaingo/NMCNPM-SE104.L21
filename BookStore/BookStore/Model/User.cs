using BookStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class User : BaseViewModel
    {
        private static User _ins;
        public static User Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new User();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        public NGUOIDUNG nguoiDung { get; set; }
        private User()
        {
            nguoiDung = new NGUOIDUNG();
        }
    }
}