using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BookStore.Model;
using System.Windows.Controls;
using System.Windows.Input;
using BookStore.Tools;
using System.Windows;

namespace BookStore.ViewModel
{
    public class RuleViewModel : BaseViewModel
    {
        private ObservableCollection<THAMSO> _ListParameter;
        public ObservableCollection<THAMSO> ListParameter { get => _ListParameter; set => _ListParameter = value; }
        
        public RuleViewModel()
        {
            ListParameter = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveRule(); });
        }

        public ICommand SaveButtonClickCommand { get; set; }

        void SaveRule() {
            if(ListParameter[0].ChoPhepThuLonHonNo == null || ListParameter[0].LuongTonToiThieuSauBan == null || ListParameter[0].SoNoToiDa == null || ListParameter[0].LuongTonToiThieu == null || ListParameter[0].LuongNhapToiThieu == null )
                MessageBox.Show("Vui lòng nhập đủ thông tin !");
            else
            {
                THAMSO parameter = DataProvider.Ins.DB.THAMSOes.First();
                parameter.LuongNhapToiThieu = ListParameter[0].LuongNhapToiThieu;
                parameter.LuongTonToiThieu = ListParameter[0].LuongTonToiThieu;
                parameter.LuongTonToiThieuSauBan = ListParameter[0].LuongTonToiThieuSauBan;
                parameter.SoNoToiDa = ListParameter[0].SoNoToiDa;
                parameter.ChoPhepThuLonHonNo = ListParameter[0].ChoPhepThuLonHonNo;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Lưu thành công");
            }    
        }
    }
}
