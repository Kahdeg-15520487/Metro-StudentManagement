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

        private ObservableCollection<GetGeneralAnouncementDetailBySummary_Result> _AnouncementDetail= null;

        public ICommand AnouncementClicked { get; set; }

        public ObservableCollection<GetGeneralAnouncementDetailBySummary_Result> AnouncementDetail
        {
            get
            {
                return _AnouncementDetail;
            }

            set
            {
                _AnouncementDetail = value;
                OnPropertyChanged("AnouncementDetail");
            }
        }



        private bool isAnouncementDetailOpen = false;
        public bool IsAnouncementDetailOpen
        {
            get
            {
                return isAnouncementDetailOpen;
            }

            set
            {
                if (value == isAnouncementDetailOpen)
                    return;
                isAnouncementDetailOpen = value;
                OnPropertyChanged("IsAnouncementDetailOpen");
            }
        }

        private bool isAnouncementOpen = true;
        public bool IsAnouncementOpen
        {
            get
            {
                return isAnouncementOpen;
            }

            set
            {
                if (value == isAnouncementOpen)
                    return;
                isAnouncementOpen = value;
                OnPropertyChanged("IsAnouncementOpen");
            }
        }

        private void OnAnouncementClicked(TextBlock currentText)
        {
            string currentSummary = currentText.Text;
            _AnouncementDetail = new ObservableCollection<GetGeneralAnouncementDetailBySummary_Result>(ST.GetGeneralAnouncementDetailBySummary(currentSummary));
            IsAnouncementDetailOpen = true;
            IsAnouncementOpen = false;
        }

        public AnouncementViewModel()
        {
            AnouncementClicked = new RelayCommand<TextBlock>((p) => true, OnAnouncementClicked);
        }
    }
}
