using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
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
            Settings = new RelayCommand<object>((p) => true, OnSettingsCommand);



            CurrentUserControl = _ViewModelList[0];
        }
        public ICommand Settings { get; set; }

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
        private void OnSettingsCommand(object obj)
        {
            IsSettingsFlyoutOpen = true;
 
        }



    }
}