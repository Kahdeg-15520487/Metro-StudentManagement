using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    class AccountViewModel:ViewModelBase
    {
        public AccountViewModel()
        {
            Settings = new RelayCommand<object>((p) => true, OnSettingsCommand);
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
            MessageBox.Show("Command");
        }
    }
}
