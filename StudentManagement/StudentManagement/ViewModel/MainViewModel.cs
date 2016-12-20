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

namespace StudentManagement.ViewModel
{
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {

        StudentDBEntities ST = new StudentDBEntities();
        MetroWindow metroWindow = (Application.Current.MainWindow as MetroWindow);

        #region For Account View
        public ICommand Accounts { get; set; }
        public ICommand Settings { get; set; }
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
        string UserPassword = string.Empty;

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

        void OnSettingsCommand(object obj)
        {
            IsAccountFlyoutOpen = false;
            IsSettingsFlyoutOpen = true;
        }

        private void InitAccountView()
        {
            Accounts = new RelayCommand<object>((p) => true, OnAccountCommand);
            Settings = new RelayCommand<object>((p) => true, OnSettingsCommand);
        }

        #endregion

        #region For UserSettings

        public ICommand BrowseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand ChangePictureOpenCommand { get; set; }
        public ICommand ChangePasswordOpenCommand { get; set; }
        public ICommand SignOutCommand { get; set; }
        public ICommand ComfirmChangePasswordCommand { get; set; }
        public ICommand NewPasswordGotFocusCommand { get; set; }
        public ICommand RetypePasswordGotFocusCommand { get; set; }

        private bool isChangePasswordOpen; //This hold the visibility of Change Password groupbox
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

        private bool isChangeProfilePictureOpen;//This hold the visibility of Change ProfilePicture groupbox
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


        private ImageSource _ProfilePictureSourse; //This is the path of Profile Picture
        public ImageSource ProfilePictureSourse
        {
            get
            {
                return _ProfilePictureSourse;
            }

            set
            {
                _ProfilePictureSourse = value;
                OnPropertyChanged("ProfilePictureSourse");
            }
        }

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
            ProfilePictureSourse = new BitmapImage(new Uri(PicturePath));
        }

        private async void OnConfirmButtonCommand(object obj)
        {
            if (ProfilePictureSourse == null && PicturePath == null)
            {
                var conntroller = await metroWindow.ShowMessageAsync("Information", "You have not selected a picture yet, click BROWSE to select one..");
                return;
            }
            UserImage data = new UserImage();
            data.ImagePath = PicturePath;
            data.ImageToByte = File.ReadAllBytes(PicturePath);
            ST.UserImage.Add(data);
            ST.SaveChanges();
            MessageBox.Show("OK");
        }

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

        private void OnChangePasswordOpenCommand(object obj)
        {
            IsChangePasswordOpen = true;
            IsChangeProfilePictureOpen = false;

        }

        private void OnChangeProfilePictureOpenCommand(object obj)
        {
            IsChangePasswordOpen = false;
            IsChangeProfilePictureOpen = true;
        }

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
                IsAccountFlyoutOpen = false;
                IsSettingsFlyoutOpen = false;
                IsChangePasswordOpen = false;
                IsChangeProfilePictureOpen = false;
                Students.Clear();
            }
        }


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

        private bool canChangePassword = false;

        public bool CanChangePassword
        {
            get
            {
                return canChangePassword;
            }

            set
            {
                if (canChangePassword == value)
                {
                    return;
                }
                canChangePassword = value;
                OnPropertyChanged("CanChangePassword");
            }
        }

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

        private void OnComfirmChangePasswordCommand(object obj)
        {
            if (checkError)
                return;
            var updateStudent = ST.StudentUser.Find(User[0].UserName);
            updateStudent.Pasworkd = NewPassword;
            ST.Entry(updateStudent).State = System.Data.Entity.EntityState.Modified;
            ST.SaveChanges();
            
            MessageBox.Show("OK");
        }

        private void OnNewPasswordGotFocusCommand(object obj)
        {
            NewPassword = string.Empty;
            RetypePasswordProperty = string.Empty;
        }

        private void OnRetypePasswordGotFocusCommand(object obj)
        {
            RetypePasswordProperty = string.Empty;
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "CorrectPasswordProperty" && CorrectPasswordProperty != UserPassword)
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





        private void InitUserSettingsView()
        {
            BrowseCommand = new RelayCommand<object>((p) => true, OnBrowseButtonCommand);
            ConfirmCommand = new RelayCommand<object>((p) => true, OnConfirmButtonCommand);
            ChangePasswordOpenCommand = new RelayCommand<object>((p) => true, OnChangePasswordOpenCommand);
            ChangePictureOpenCommand = new RelayCommand<object>((p) => true, OnChangeProfilePictureOpenCommand);
            SignOutCommand = new RelayCommand<object>((p) => true, OnSignOutCommand);
            ConfirmCommand = new RelayCommand<object>((p) => true, OnComfirmChangePasswordCommand);
            NewPasswordGotFocusCommand = new RelayCommand<object>((p) => true, OnNewPasswordGotFocusCommand);
            RetypePasswordGotFocusCommand = new RelayCommand<object>((p) => true, OnRetypePasswordGotFocusCommand);
        }

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

        public MainViewModel()
        {
            InitViewModelList();
            InitAccountView();
            InitUserSettingsView();
        }
    }
}