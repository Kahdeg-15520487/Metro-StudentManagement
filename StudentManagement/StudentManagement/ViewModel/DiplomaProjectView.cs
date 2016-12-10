using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    class DiplomaProjectView:ViewModelBase
    {
        StudentDBEntities ST = new StudentDBEntities();
        private ObservableCollection<object> _DiplomaProject;

        public ObservableCollection<object> DiplomaProject
        {
            get
            {
                var thisUser = DialogLogginViewModel.Users[0];
                if (_DiplomaProject == null)
                {
                    _DiplomaProject = new ObservableCollection<object>(ST.GetDiplomaProject1(thisUser.ID).ToList());
                }
                return _DiplomaProject;
            }

            set
            {
                _DiplomaProject = value; OnPropertyChanged("DiplomaProject");
            }
        }
    }
}
