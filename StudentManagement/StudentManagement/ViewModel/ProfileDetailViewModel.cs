using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    public class ProfileDetailViewModel : ViewModelBase
    {
        StudentDBEntities ST = new StudentDBEntities();
        private ObservableCollection<GetClassDetail_Result> _ClassInfo;

        public ObservableCollection<GetClassDetail_Result> ClassInfo
        {
            get
            {
                if (_ClassInfo == null)
                    _ClassInfo = new ObservableCollection<GetClassDetail_Result>(ST.GetClassDetail(ProfileViewModel.ClassID));
                return _ClassInfo;
            }

            set
            {
                if (value.Equals(_ClassInfo))
                    return;
                _ClassInfo = value;
                OnPropertyChanged("ClassInfo");
            }
        }
    }
}
