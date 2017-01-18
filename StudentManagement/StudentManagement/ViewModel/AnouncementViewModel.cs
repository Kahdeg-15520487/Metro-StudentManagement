using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    public class AnouncementViewModel:ViewModelBase
    {
        StudentDBEntities ST = new StudentDBEntities();
        private ObservableCollection<Get10NewestAnouncements_Result> _TopTenNewestAnouncement;

        public ObservableCollection<Get10NewestAnouncements_Result> TopTenNewestAnouncement
        {
            get
            {
                if (_TopTenNewestAnouncement == null)
                    _TopTenNewestAnouncement = new ObservableCollection<Get10NewestAnouncements_Result>(ST.Get10NewestAnouncements());
                return _TopTenNewestAnouncement;
            }

            set
            {
                if (value == _TopTenNewestAnouncement)
                    return;
                _TopTenNewestAnouncement = value;
                OnPropertyChanged("TopTenNewestAnouncement");
            }
        }
    }
}
