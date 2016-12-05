using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    class AccountViewModel : ViewModelBase
    {

        StudentDBEntities ST = new StudentDBEntities();

        private ObservableCollection<GetStudentInfoByID_Result> _Students;
        public AccountViewModel()
        {
            if (DialogLogginViewModel.isLoggedIn == true)
            {
                string ID = DialogLogginViewModel.Users[0].ID;
                Students = new ObservableCollection<GetStudentInfoByID_Result>(ST.GetStudentInfoByID(ID).ToList());
         
            }
            else
                return;
        }

        public ObservableCollection<GetStudentInfoByID_Result> Students
        {
            get
            {
                if (_Students == null)
                {
                    _Students = new ObservableCollection<GetStudentInfoByID_Result>();
                }
                return _Students;
            }

            set
            {
                _Students = value;
                OnPropertyChanged("Students");

            }
        }
    }
}
