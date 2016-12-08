using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        StudentDBEntities ST = new StudentDBEntities();

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

        private readonly List<ViewModelBase> _ViewModelList;
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

      
        private int _selectedUCIndex;



        public MainViewModel()
        {
            // Create a list to hold all the UserControl ViewModel Class
            _ViewModelList = new List<ViewModelBase>()
            {
                new IntroWindowViewModel(),
                new MainMenuViewModel(),

            };
            Settings = new RelayCommand<object>((p) => true, (p) =>
            {
                IsSettingsFlyoutOpen = true;
                string ID = DialogLogginViewModel.Users[0].ID;
                //Students = new ObservableCollection<GetStudentsInfoByID_Result>(ST.GetStudentsInfoByID(ID));
                //Name = Students[0].Name + " " + Students[0].MiddleName + " " + Students[0].LastName;
                //Email = Students[0].Email;
 
      
            });



            CurrentUserControl = _ViewModelList[0];
        }
        public ICommand Settings { get; set; }

        private bool isSettingsFlyoutOpen; //This will decide if Account Settings open or not

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

     
    }
}