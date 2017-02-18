using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    public class MoreAnouncementViewModel : ViewModelBase
    {
        StudentDBEntities ST = new StudentDBEntities();
        private int currentPage = 1;


        public int CurrentPage
        {
            get
            {
                return currentPage;
            }

            set
            {
                if (currentPage != value)
                    currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private ObservableCollection<SelectGeneralAnouncementInRange_Result> _CurrentPageAnouncement;

        public ObservableCollection<SelectGeneralAnouncementInRange_Result> CurrentPageAnouncement
        {
            get
            {
                if (_CurrentPageAnouncement == null)
                    _CurrentPageAnouncement = new ObservableCollection<SelectGeneralAnouncementInRange_Result>(ST.SelectGeneralAnouncementInRange(currentPage,currentPage+4));
                return _CurrentPageAnouncement;
            }

            set
            {
                if (value == _CurrentPageAnouncement)
                    return;
                _CurrentPageAnouncement = value;
                OnPropertyChanged("CurrentPageAnouncement");
            }
        }

        public MoreAnouncementViewModel()
        {
            Messager.CurrnentPageMessageTransmitted += OnChangePageAnouncement;
        }

        private void OnChangePageAnouncement(string obj)
        {
            CurrentPage = Convert.ToInt32(obj);
            CurrentPageAnouncement = new ObservableCollection<SelectGeneralAnouncementInRange_Result>(ST.SelectGeneralAnouncementInRange(currentPage, currentPage + 4));
        }
    }
}
