using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class ProfileViewModel : ViewModelBase
    {
        StudentDBEntities ST = new StudentDBEntities();
        SpeechSynthesizer warningAudio = new SpeechSynthesizer();
        private ObservableCollection<GetStudentAndParentInfoByID_Result> _StudentInfo;

        public ObservableCollection<GetStudentAndParentInfoByID_Result> StudentInfo
        {
            get
            {
                if (_StudentInfo == null)
                    _StudentInfo = new ObservableCollection<GetStudentAndParentInfoByID_Result>(ST.GetStudentAndParentInfoByID(DialogLogginViewModel.Users[0].ID));
                return _StudentInfo;
            }

            set
            {
                if (value.Equals(_StudentInfo))
                    return;
                _StudentInfo = value;
                OnPropertyChanged("StudentInfo");
            }
        }

        public ICommand SaveChangesCommand { get; set; }


        private void OnSaveChangesCommand(object obj)
        {
            ST.UpdateStudentProfile(StudentInfo[0].StudentID, StudentInfo[0].ParentName, StudentInfo[0].ParentPhone, StudentInfo[0].ParentsGender, StudentInfo[0].CurrentAddress, StudentInfo[0].Hometown, StudentInfo[0].PhoneNumber, StudentInfo[0].Gender);
            warningAudio.SpeakAsync("Save successfully..");
        }

        private void InitProfile()
        {
            SaveChangesCommand = new RelayCommand<object>((p) => true, OnSaveChangesCommand);
        }
        public ProfileViewModel()
        {
            InitProfile();
        }
    }
}
