using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Speech.Synthesis;

namespace StudentManagement.ViewModel
{
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        SpeechSynthesizer warningAudio = new SpeechSynthesizer();
        StudentDBEntities ST = new StudentDBEntities();
        MetroWindow metroWindow = (Application.Current.MainWindow as MetroWindow);
        string UserPassword = string.Empty; //Hold the User current Password

        #region For Account View

        #region Student Collection and Get-Set Property
        private ObservableCollection<GetStudentsInfoByID_Result> _Students; //This hold all Students's Infomation

        public ObservableCollection<GetStudentsInfoByID_Result> Students
        {
            get
            {
                if (_Students == null)
                {
                    _Students = new ObservableCollection<GetStudentsInfoByID_Result>();
                }
                return _Students;
            }

            set
            {
                if (_Students != value)
                {
                    _Students = value;
                    OnPropertyChanged("Students");
                }
            }
        }
        #endregion

        #region User Collection and Get-Set Property
        private ObservableCollection<GetUsersDetail_Result> _User; //This hold all the User's infomation


        public ObservableCollection<GetUsersDetail_Result> User
        {
            get
            {
                if (_User == null)
                {
                    _User = new ObservableCollection<GetUsersDetail_Result>();
                }
                return _User;
            }

            set
            {
                if (_User != value)
                {
                    _User = value;
                    OnPropertyChanged("User");
                }

            }
        }

        #endregion

        #region Name variable-hold the Name of Student in Student Collection
        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #region Email variable-hold the Email of Student in Student Collection
        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }

            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        #endregion

        #region PicturePath variable-hold the Path of Image of Student in Student Collection
        private string _PicturePath;
        public string PicturePath
        {
            get
            {
                return _PicturePath;
            }

            set
            {
                _PicturePath = value;
                OnPropertyChanged("PicturePath");
            }
        }
        #endregion

        #region isAccountFlyoutOpen-decide Account open or not
        private bool isAccountFlyoutOpen; //This will decide if Account Settings open or not

        public bool IsAccountFlyoutOpen
        {
            get
            {
                return isAccountFlyoutOpen;
            }

            set
            {
                if (value.Equals(isAccountFlyoutOpen))
                    return;
                isAccountFlyoutOpen = value;
                OnPropertyChanged("IsAccountFlyoutOpen");
            }
        }

        #endregion

        #region isSettingsFlyoutOpen-decide AccountSettings open or not
        private bool isSettingsFlyoutOpen;

        public bool IsSettingsFlyoutOpen
        {
            get
            {
                return isSettingsFlyoutOpen;
            }

            set
            {
                if (value.Equals(isSettingsFlyoutOpen))
                    return;
                isSettingsFlyoutOpen = value;
                OnPropertyChanged("IsSettingsFlyoutOpen");
            }
        }
        #endregion


        #region AccountCommand-Open Account Flyout and do some stuffs
     
        public ICommand Accounts { get; set; } 
        private void OnAccountCommand(object obj)
        {
            IsAccountFlyoutOpen = true;
            string UserName = DialogLogginViewModel.Users[0].UserName;
            string ID = DialogLogginViewModel.Users[0].ID;
            Students = new ObservableCollection<GetStudentsInfoByID_Result>(ST.GetStudentsInfoByID(ID));
            User = new ObservableCollection<GetUsersDetail_Result>(ST.GetUsersDetail(UserName));
            GetImageUrlFromDatabase();
            Name = Students[0].Name + " " + Students[0].MiddleName + " " + Students[0].LastName;
            Email = Students[0].Email;
            UserPassword = User[0].Passwords;
        }
        #endregion

        #region SettingsCommand-Open AccountSettings and do some stuffs
        public ICommand Settings { get; set; } //This command open Settings Flyout and do some stuffs
        void OnSettingsCommand(object obj)
        {
            IsAccountFlyoutOpen = false;
            IsSettingsFlyoutOpen = true;
        }
        #endregion

        #endregion

        #region For UserSettings

        #region GetImageUrlFromDatabase-Get current User Image from Database
        private void GetImageUrlFromDatabase()
        {
            UserImage images = new UserImage();
            var result = Students[0].ImageToByte;
            Stream StreamObj = new MemoryStream(result);
            BitmapImage BitObj = new BitmapImage();
            BitObj.BeginInit();
            BitObj.StreamSource = StreamObj;
            BitObj.EndInit();
            ImageUrl = BitObj;
        }
        #endregion

        #region IsChangePasswordOpen-hold the visibility of Change Password groupbox
        private bool isChangePasswordOpen; 
        public bool IsChangePasswordOpen
        {
            get
            {
                return isChangePasswordOpen;
            }

            set
            {
                if (isChangePasswordOpen != value)
                    isChangePasswordOpen = value;
                OnPropertyChanged("IsChangePasswordOpen");
            }
        }
        #endregion

        #region IsChangeProfilePictureOpen-hold the visibility of Change ProfilePicture groupbox
        private bool isChangeProfilePictureOpen;
        public bool IsChangeProfilePictureOpen
        {
            get
            {
                return isChangeProfilePictureOpen;
            }

            set
            {
                if (IsChangeProfilePictureOpen != value)
                    isChangeProfilePictureOpen = value;
                OnPropertyChanged("IsChangeProfilePictureOpen");
            }
        }
        #endregion

        #region ChangePasswordOpenCommand-Set the visibility of ChangePassword Groupbox
        public ICommand ChangePasswordOpenCommand { get; set; }
        private void OnChangePasswordOpenCommand(object obj)
        {
            IsChangePasswordOpen = true;
            IsChangeProfilePictureOpen = false;
        }
        #endregion

        #region ChangePictureOpenCommand-Set the visibility of ChangeProfilePicture GroupBox
        public ICommand ChangePictureOpenCommand { get; set; }
        private void OnChangeProfilePictureOpenCommand(object obj)
        {
            IsChangePasswordOpen = false;
            IsChangeProfilePictureOpen = true;
        }
        #endregion

        #region ProfilePictureSource variable-hold the Source of ProfilePicture
        private ImageSource _ProfilePictureSource; 
        public ImageSource ProfilePictureSource
        {
            get
            {
                return _ProfilePictureSource;
            }

            set
            {
                _ProfilePictureSource = value;
                OnPropertyChanged("ProfilePictureSource");
            }
        }
        #endregion

        #region ImageUrl-hold the Image of current User
        private BitmapImage _ImageUrl; //Hold the Url of Picture 

        public BitmapImage ImageUrl
        {
            get
            {
                return _ImageUrl;
            }

            set
            {
                _ImageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }
        #endregion


 
        #region BrowseCommand-Click to open a FileOpenDialog and choose profile picture
        public ICommand BrowseCommand { get; set; }
        private void OnBrowseButtonCommand(object obj)
        {
            UserImage image = new UserImage();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.png, *.bmp, *.jpg)|*.png;*.bmp;*.jpg|All files (*.*)|*.*";
            fileDialog.DefaultExt = ".jpeg";
            var result = fileDialog.ShowDialog();
            if (result == false)
                return;
            PicturePath = fileDialog.FileName;
            ProfilePictureSource = new BitmapImage(new Uri(PicturePath));
        }
        #endregion

        #region ConfirmChangePictureCommand-Confirm the Selected Image and do some stuffs
        public ICommand ConfirmChangePictureCommand { get; set; }
        private async void OnConfirmChangePictureCommand(object obj)
        {
            if (ProfilePictureSource == null && PicturePath == null)
            {
                var conntroller = await metroWindow.ShowMessageAsync("Information", "You have not selected a picture yet, click BROWSE to select one..");
                return;
            }
            UserImage data = new UserImage();
            data.ImagePath = PicturePath;
            data.ImageToByte = File.ReadAllBytes(PicturePath);
            ST.UserImage.Add(data);
            ST.SaveChanges();
        }
        #endregion

        #region ConfirmChangePasswordCommand-Confirm the input password
        public ICommand ConfirmChangePasswordCommand { get; set; }
        private void OnComfirmChangePasswordCommand(object obj)
        {
            if (checkError || correctPasswordProperty == string.Empty || NewPassword==string.Empty)
                return;
            var updateStudent = ST.StudentUser.Find(User[0].UserName);
            updateStudent.Pasworkd = NewPassword;
            ST.Entry(updateStudent).State = System.Data.Entity.EntityState.Modified;
            ST.SaveChanges();
        }
        #endregion

        #region SignOutCommand-for signing out

        private void ReInit()
        {
            IsAccountFlyoutOpen = false;
            IsSettingsFlyoutOpen = false;
            IsChangePasswordOpen = false;
            IsChangeProfilePictureOpen = false;
            Students.Clear();
            NewPassword = string.Empty;
            RetypePasswordProperty = string.Empty;
            CorrectPasswordProperty = string.Empty;
            ProfilePictureSource = null;
            PicturePath = string.Empty;
            ImageUrl = null;
        }
        public ICommand SignOutCommand { get; set; }
        private void OnSignOutCommand(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).cmbChangeUC.SelectedIndex = 0;
                    (window as MainWindow).WindowState = System.Windows.WindowState.Normal;
                    (window as MainWindow).Account.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            DialogLogginViewModel.isLoggedIn = false;
            ReInit();
        }

        #endregion

        #region NewPassword variable-hold the NewPassword input
        private string newPassword = string.Empty;

        public string NewPassword
        {
            get
            {
                return newPassword;
            }

            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
        #endregion

        #region CorrectPasswordProperty-check that User input correct password or not
        private string correctPasswordProperty = string.Empty;

        public string CorrectPasswordProperty
        {
            get
            {
                return correctPasswordProperty;
            }

            set
            {
                if (correctPasswordProperty == value)
                    return;
                correctPasswordProperty = value;
                OnPropertyChanged("CorrectPasswordProperty");
            }
        }

        #endregion

        #region RetypePasswordProperty-check that RetypePassword correct or not
        private string retypePasswordProperty = string.Empty;
        public string RetypePasswordProperty
        {
            get
            {
                return retypePasswordProperty;
            }

            set
            {
                if (retypePasswordProperty == value)
                    return;
                retypePasswordProperty = value;
                OnPropertyChanged("RetypePasswordProperty");
            }
        }
        #endregion

        #region CheckError-check that User can change Password or not
        private bool checkError = false;

        public bool CheckError
        {
            get
            {
                return checkError;
            }

            set
            {
                checkError = value;
                OnPropertyChanged("CheckError");
            }
        }

        #endregion

        #region NewPasswordGotFocusCommand-GetFocus command on NewPassword Text Box
        public ICommand NewPasswordGotFocusCommand { get; set; }
        private void OnNewPasswordGotFocusCommand(object obj)
        {
            NewPassword = string.Empty;
            RetypePasswordProperty = string.Empty;
        }
        #endregion

        #region RetypeGotFocusCommand-GetFocus command on RetypePassword Text Box
        public ICommand RetypePasswordGotFocusCommand { get; set; }
        private void OnRetypePasswordGotFocusCommand(object obj)
        {
            RetypePasswordProperty = string.Empty;
        }
        #endregion

        #region IDataErrorInfo Interface-check error
        public string this[string columnName]
        {
            get
            {
                if (columnName == "CorrectPasswordProperty" && CorrectPasswordProperty != UserPassword && CorrectPasswordProperty!=string.Empty)
                {
                    CheckError = true;
                    return "Incorrect Password..";
                }
                else if (columnName == "RetypePasswordProperty" && RetypePasswordProperty != string.Empty && RetypePasswordProperty != NewPassword)
                {
                    CheckError = true;
                    return "Password not match..";
                }
                else
                {
                    checkError = false;
                    return null;
                }
            }
        }
        #endregion

        #endregion
        
        #region Replace UserControl
        private List<ViewModelBase> _ViewModelList;
        //This hold the current Page, which will be displayed
        private ViewModelBase _currentUserControl;

        public ViewModelBase CurrentUserControl
        {
            get
            {
                return _currentUserControl;
            }

            set
            {
                if (_currentUserControl == value)
                    return;
                _currentUserControl = value;
                OnPropertyChanged("CurrentUserControl");
            }
        }

        //Hold the Selected UserControl index

        private int _selectedUCIndex;
        public int SelectedUCIndex
        {
            get
            {
                return _selectedUCIndex;
            }

            set
            {
                if (_selectedUCIndex == value)
                    return;
                _selectedUCIndex = value;
                OnPropertyChanged("SelectedUCIndex");
                CurrentUserControl = _ViewModelList[_selectedUCIndex];
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }



        private void InitViewModelList()
        {
            // Create a list to hold all the UserControl ViewModel Class
            _ViewModelList = new List<ViewModelBase>()
            {
                new IntroWindowViewModel(),
                new MainMenuViewModel(),

            };
            CurrentUserControl = _ViewModelList[0];
        }

        #endregion

        private void InitAccountView()
        {
            Accounts = new RelayCommand<object>((p) => true, OnAccountCommand);
            Settings = new RelayCommand<object>((p) => true, OnSettingsCommand);
        }


        private void InitUserSettingsView()
        {
            BrowseCommand = new RelayCommand<object>((p) => true, OnBrowseButtonCommand);
            ConfirmChangePictureCommand = new RelayCommand<object>((p) => true, OnConfirmChangePictureCommand);
            ChangePasswordOpenCommand = new RelayCommand<object>((p) => true, OnChangePasswordOpenCommand);
            ChangePictureOpenCommand = new RelayCommand<object>((p) => true, OnChangeProfilePictureOpenCommand);
            SignOutCommand = new RelayCommand<object>((p) => true, OnSignOutCommand);
            ConfirmChangePasswordCommand = new RelayCommand<object>((p) => true, OnComfirmChangePasswordCommand);
            NewPasswordGotFocusCommand = new RelayCommand<object>((p) => true, OnNewPasswordGotFocusCommand);
            RetypePasswordGotFocusCommand = new RelayCommand<object>((p) => true, OnRetypePasswordGotFocusCommand);
        }

        public MainViewModel()
        {
            InitViewModelList();
            InitAccountView();
            InitUserSettingsView();
        }
    }
}