using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StudentManagement.ViewModel
{
    public class MainViewModel : ViewModelBase
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
            string ID = DialogLogginViewModel.Users[0].ID;
            Students = new ObservableCollection<GetStudentsInfoByID_Result>(ST.GetStudentsInfoByID(ID));
            GetImageUrlFromDatabase();
            Name = Students[0].Name + " " + Students[0].MiddleName + " " + Students[0].LastName;
            Email = Students[0].Email;
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
        public ICommand ChangePictureCommand { get; set; }
        public ICommand ChangePassworkCommand { get; set; }
        public ICommand SignOutCommand { get; set; }

        private bool isChangePassworkOpen; //This hold the visibility of Change Passwork groupbox
        public bool IsChangePassworkOpen
        {
            get
            {
                return isChangePassworkOpen;
            }

            set
            {
                if (isChangePassworkOpen!=value)
                    isChangePassworkOpen = value;
                OnPropertyChanged("IsChangePassworkOpen");
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*";
            openFileDialog1.DefaultExt = ".jpeg";
            PicturePath= openFileDialog1.FileName;
            ProfilePictureSourse = new BitmapImage(new Uri(PicturePath));         
        }

        private void OnConfirmButtonCommand(object obj)
        {
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

        private void OnChangePassworkCommand(object obj)
        {
            IsChangePassworkOpen = true;
            IsChangeProfilePictureOpen = false;

        }

        private void OnChangeProfilePictureCommand(object obj)
        {
            IsChangePassworkOpen = false;
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
                IsChangePassworkOpen = false;
                IsChangeProfilePictureOpen = false;
                Students.Clear();
            }
        }

        private void InitUserSettingsView()
        {
            BrowseCommand = new RelayCommand<object>((p) => true, OnBrowseButtonCommand);
            ConfirmCommand = new RelayCommand<object>((p) => true, OnConfirmButtonCommand);
            ChangePassworkCommand= new RelayCommand<object>((p) => true, OnChangePassworkCommand);
            ChangePictureCommand = new RelayCommand<object>((p) => true, OnChangeProfilePictureCommand);
            SignOutCommand= new RelayCommand<object>((p) => true, OnSignOutCommand);
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