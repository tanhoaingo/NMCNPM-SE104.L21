using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStore.ViewModel
{
    public class ChangeRulesViewModel:BaseViewModel
    {
        private ObservableCollection<THAMSO> _ListParameter;
        public ObservableCollection<THAMSO> ListParameter { get => _ListParameter; set => _ListParameter = value; }

        public ChangeRulesViewModel()
        {
            ListParameter = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            SaveButtonClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SaveRule(); });
        }

        public ICommand SaveButtonClickCommand { get; set; }

        void SaveRule()
        {
            var DauSach = new ObservableCollection<DAUSACH>(DataProvider.Ins.DB.DAUSACHes);
            var min = DauSach.Min(x => x.LuongTon);
            if (ListParameter[0].ChoPhepThuLonHonNo == null || ListParameter[0].LuongTonToiThieuSauBan == null || ListParameter[0].SoNoToiDa == null || ListParameter[0].LuongTonToiThieu == null || ListParameter[0].LuongNhapToiThieu == null)
            { MessageBox.Show("Vui lòng nhập đủ thông tin !");
                return;
            }
            if (ListParameter[0].LuongTonToiThieu > min)
            {
                MessageBox.Show("Lượng tồn tối thiểu không được lớn hơn " + min.ToString());
                return;
            }
            if (ListParameter[0].LuongTonToiThieuSauBan > min)
            {
                MessageBox.Show("Lượng tồn tối thiểu sau bán không được lớn hơn " + min.ToString());
                return;
            }
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
