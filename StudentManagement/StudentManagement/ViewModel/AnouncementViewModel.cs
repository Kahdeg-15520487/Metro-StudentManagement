using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class AnouncementViewModel:MainAnouncementViewModel
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

        public ICommand AnouncementClicked
        {
            get
            {
                if (_AnouncementClicked==null)
                {
                    _AnouncementClicked = new RelayCommand<TextBlock>((p) => true, OnAnouncementClicked);
                }
                return _AnouncementClicked;
            }

         
        }
     

        private ICommand _AnouncementClicked;
        private void OnAnouncementClicked(TextBlock currentAnouncement)
        {
            string currentText = currentAnouncement.Text;
            IsAnouncementOpen = false;
            IsDetailAnouncementOpen = true;
        }
        

        public AnouncementViewModel()
        {
            
        }
    }
}
