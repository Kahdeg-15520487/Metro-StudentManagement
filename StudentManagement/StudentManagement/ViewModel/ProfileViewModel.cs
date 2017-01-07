using MahApps.Metro.Controls;
using StudentManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class ProfileViewModel : ViewModelBase, IDataErrorInfo
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

        #region Some Stuffs

        public static string ClassID;
        private string studentName = string.Empty;

        public string StudentName
        {
            get
            {
                return studentName;
            }

            set
            {
                studentName = value;
            }
        }

        private string studentID = string.Empty;
        public string StudentID
        {
            get
            {
                return studentID;
            }

            set
            {
                studentID = value;
            }
        }



        private string className = string.Empty;

        public string ClassName
        {
            get
            {
                return className;
            }

            set
            {
                className = value;
            }
        }
        private string facultyName = string.Empty;
        public string FacultyName
        {
            get
            {
                return facultyName;
            }

            set
            {
                facultyName = value;
            }
        }

        private string departmentName = string.Empty;
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }

            set
            {
                departmentName = value;
            }
        }


        private string schoolName = string.Empty;
        public string SchoolName
        {
            get
            {
                return schoolName;
            }

            set
            {
                schoolName = value;
            }
        }

        private string email = string.Empty;
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }


        private string academicYear = string.Empty;
        public string AcademicYear
        {
            get
            {
                return academicYear;
            }

            set
            {
                academicYear = value;
            }
        }

        private string parentName = string.Empty;

        public string ParentName
        {
            get
            {
                return parentName;
            }

            set
            {
                parentName = value;
                OnPropertyChanged("ParentName");
            }
        }
        private string parentMobile = string.Empty;
        public string ParentMobile
        {
            get
            {
                return parentMobile;
            }

            set
            {
                parentMobile = value;
                OnPropertyChanged("ParentMobile");
            }
        }
        private string currentAddress = string.Empty;
        public string CurrentAddress
        {
            get
            {
                return currentAddress;
            }

            set
            {
                currentAddress = value;
                OnPropertyChanged("CurrentAddress");
            }
        }
        private string permanentAddress = string.Empty;

        public string PermanentAddress
        {
            get
            {
                return permanentAddress;
            }

            set
            {
                permanentAddress = value;
                OnPropertyChanged("PermanentAddress");
            }
        }
        private string mobile = string.Empty;

        public ICommand SaveChangesCommand { get; set; }
        public string Mobile
        {
            get
            {
                return mobile;
            }

            set
            {
                mobile = value;
                OnPropertyChanged("Mobile");
            }
        }

        #endregion

        public ICommand TextReviveWhenLostFocus { get; set; }

        private void OnTextReviveWhenLostFocus(object parameters)
        {
            var values = (object[])parameters;
            var currentText = values[0].ToString();
            TextBox currentTextBox = values[1] as TextBox;
            Clipboard.SetText(currentText);
            currentTextBox.Clear();
            currentTextBox.Paste();
        }
        private bool canSave = true;
        public bool CanSave
        {
            get
            {
                return canSave;
            }

            set
            {
                canSave = value;
                OnPropertyChanged("CanSave");
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }



        public string this[string columnName]
        {
            get
            {
                if (columnName == "CurrentAddress" && CurrentAddress == string.Empty)
                {
                    CanSave = false;
                    return "Can not be empty";
                }
                else
                    if (columnName == "ParentName" && ParentName == string.Empty)
                {
                    CanSave = false;
                    return "Can not be empty";
                }
                else
                    if (columnName == "ParentMobile" && ParentMobile == string.Empty)
                {
                    CanSave = false;
                    return "Can not be empty";
                }
                else
                    if (columnName == "PermanentAddress" && PermanentAddress == string.Empty)
                {
                    CanSave = false;
                    return "Can not be empty";
                }
                else
                    if (columnName == "Mobile" && Mobile == string.Empty)
                {
                    CanSave = false;
                    return "Can not be empty";
                }
                else
                {
                    CanSave = true;
                    return null;
                }

            }
        }

        private void OnSaveChangesCommand(object obj)
        {
            if (CanSave)
            {
                ST.UpdateStudentProfile(StudentInfo[0].StudentID, StudentInfo[0].ParentName, StudentInfo[0].ParentPhone, StudentInfo[0].ParentsGender, StudentInfo[0].CurrentAddress, StudentInfo[0].Hometown, StudentInfo[0].PhoneNumber, StudentInfo[0].Gender);
                warningAudio.SpeakAsync("Save successfully..");
            }
            else
                warningAudio.SpeakAsync("Save failed..");
        }

        public ICommand OpenProfileDetail { get; set; }

        private void OnOpenProfileDetail(object obj)
        {
            MetroWindow detailWindow = new MetroWindow();
            detailWindow.Width = 650;
            detailWindow.Height = 350;
            detailWindow.Title = "This is DetailWindow";
            detailWindow.Content = new ProfileDetailView();
            detailWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            var ViewModel = new ProfileDetailViewModel();
            detailWindow.DataContext = ViewModel;
            detailWindow.Owner = Application.Current.MainWindow;
            detailWindow.Show();
        }




        private void GetDataFromServer()
        {
            ClassID = StudentInfo[0].ClassID;
            StudentName = StudentInfo[0].StudentName;
            StudentID = StudentInfo[0].StudentID;
            ClassName = StudentInfo[0].ClassName;
            FacultyName = StudentInfo[0].FacultyName;
            DepartmentName = StudentInfo[0].DepartmentName;
            SchoolName = StudentInfo[0].SchoolName;
            Email = StudentInfo[0].Email;
            AcademicYear = StudentInfo[0].AcademicYear.ToString();
            CurrentAddress = StudentInfo[0].CurrentAddress;
            ParentMobile = StudentInfo[0].ParentPhone;
            ParentName = StudentInfo[0].ParentName;
            PermanentAddress = StudentInfo[0].Hometown;
            Mobile = StudentInfo[0].PhoneNumber;
        }

        private void InitProfile()
        {
            GetDataFromServer();
            SaveChangesCommand = new RelayCommand<object>((p) => true, OnSaveChangesCommand);
            TextReviveWhenLostFocus = new RelayCommand<object>((p) => true, OnTextReviveWhenLostFocus);
            OpenProfileDetail = new RelayCommand<object>((p) => true, OnOpenProfileDetail);
        }
        public ProfileViewModel()
        {
            InitProfile();
        }
    }
}