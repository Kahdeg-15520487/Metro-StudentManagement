using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    class DialogVIewModle
    {

        StudentDBEntities St = new StudentDBEntities();
        public ICommand LoginCommand { get; set; }
        public DialogVIewModle()
        {
            LoginCommand = new RelayCommand<MetroWindow>((p) => true, ShowLoginDialogWithRememberCheckBox);


        }

        public async void ShowLoginDialogWithRememberCheckBox(MetroWindow MT)
        {
            LoginDialogData result = await MT.ShowLoginAsync("Authentication", "Enter your password", new LoginDialogSettings { ColorScheme = MT.MetroDialogOptions.ColorScheme, RememberCheckBoxVisibility = System.Windows.Visibility.Visible });
            if (St.GetStudentUser(result.Username,result.Password).Count()==0)
            {
                MessageDialogResult messageResult = await MT.ShowMessageAsync("Authentication Information", String.Format("Loggin Failed"));
            }
            else
            {
                MessageDialogResult messageResult = await MT.ShowMessageAsync("Authentication Information", String.Format("Logged in successfully "));                
            }
        }
    }
}
