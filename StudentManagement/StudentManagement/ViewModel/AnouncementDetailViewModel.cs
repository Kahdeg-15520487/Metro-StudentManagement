using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class AnouncementDetailViewModel : AnouncementViewModel
    {
        private ObservableCollection<GetGeneralAnoucementDetailByTitle_Result> _GeneralAnouncementDetail;
        StudentDBEntities ST = new StudentDBEntities();
        private string anouncementTitle;
        public ObservableCollection<GetGeneralAnoucementDetailByTitle_Result> GeneralAnouncementDetail
        {
            get
            {
                if (_GeneralAnouncementDetail == null)
                    _GeneralAnouncementDetail = new ObservableCollection<GetGeneralAnoucementDetailByTitle_Result>();
                return _GeneralAnouncementDetail;
            }

            set
            {
                if (_GeneralAnouncementDetail != value)
                    _GeneralAnouncementDetail = value;
                OnPropertyChanged("GeneralAnouncementDetail");
            }
        }

        public string AnouncementTitle
        {
            get
            {
                return anouncementTitle;
            }

            set
            {
                if (anouncementTitle != value)
                    anouncementTitle = value;
                OnPropertyChanged("AnouncementTitle");
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                if (_PreviousCommand == null)
                    _PreviousCommand = new RelayCommand<object>((p) => true, OnPreviousCommand);
                return _PreviousCommand;
            }
        }

        private ICommand _PreviousCommand;

        public AnouncementDetailViewModel()
        {
            Messager.AnouncementDetailMessageTransmitted += OnMessageReceived;
   
        }

        private void OnPreviousCommand(object obj)
        {
            Messager.AnouncementBroadCast(true, false);
        }

        private void OnMessageReceived(string Title)
        {
            AnouncementTitle = Title;
            GeneralAnouncementDetail = new ObservableCollection<GetGeneralAnoucementDetailByTitle_Result>(ST.GetGeneralAnoucementDetailByTitle(AnouncementTitle));
        }
    }
}
