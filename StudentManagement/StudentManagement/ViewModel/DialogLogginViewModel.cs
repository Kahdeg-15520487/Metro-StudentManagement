using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    class DialogLogginViewModel
    {

        StudentDBEntities St = new StudentDBEntities();
        private ObservableCollection<GetStudentUser_Result> _StudentUsers;
        MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
        public ICommand LoginCommand { get; set; }

        public ObservableCollection<GetStudentUser_Result> StudentUsers
        {
            get
            {
                return _StudentUsers;
            }

            set
            {
                _StudentUsers = value;
            }
        }

        public DialogLogginViewModel()
        {
            LoginCommand = new RelayCommand<MetroWindow>((p) => true, ShowLoginDialogWithRememberCheckBox);


        }

        public async void ShowLoginDialogWithRememberCheckBox(object obj)
        {
            
            LoginDialogSettings settings = new LoginDialogSettings
            {
                ColorScheme = metroWindow.MetroDialogOptions.ColorScheme,
                RememberCheckBoxVisibility = System.Windows.Visibility.Visible,
                NegativeButtonVisibility = System.Windows.Visibility.Visible
            };
            LoginDialogData result = await metroWindow.ShowLoginAsync("Authentication", "Enter your password", settings);
            if (result==null)
            {
                MessageDialogResult messageResult = await metroWindow.ShowMessageAsync("Authentication Information", String.Format("You have canceled"));
            }
            else
            {
                StudentUsers =new ObservableCollection<GetStudentUser_Result>(St.GetStudentUser(result.Username, result.Password).ToList());
                if (StudentUsers.Count()!=0)
                {
                    var x = StudentUsers.ElementAt(0).ID;
                    MessageDialogResult messageResult = await metroWindow.ShowMessageAsync("Authentication Information", String.Format("Log in successfully.."));
                    
                  
                }
                else
                {
                    MessageDialogResult messageResult = await metroWindow.ShowMessageAsync("Authentication Information", String.Format("Valid Username or Password.."));
                }
            }
          
        }

    }
}
