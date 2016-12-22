using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManagement.ViewModel
{
    class MainMenuViewModel : ViewModelBase
    {
        //This class just hold the Main Menu Model, see detail in MainWindow.xaml
        StudentDBEntities ST = new StudentDBEntities();
        private ObservableCollection<IsDateRegister_Result> _IsDateRegister;

        public ObservableCollection<IsDateRegister_Result> IsDateRegister
        {
            get
            {
                if (_IsDateRegister == null)
                {
                    _IsDateRegister = new ObservableCollection<IsDateRegister_Result>(ST.IsDateRegister().ToList());
                }
                return _IsDateRegister;
            }

            set
            {
                _IsDateRegister = value;
                OnPropertyChanged("IsDateRegister");
            }
        }
    }
}
