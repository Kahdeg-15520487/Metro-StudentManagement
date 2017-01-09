using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    class RegisterStudyUnitViewModel : ViewModelBase
    {

        StudentDBEntities ST = new StudentDBEntities();
        private string _Header;

        public string Header
        {

            get
            {
                var data = ST.IsDateRegister().ToList()[0];
                if (_Header == null)
                {
                    _Header = string.Format("Register Study Unit {0} Open on {1}", data.SemesterName, data.DateRegister);
                }
                return _Header;
            }
            set
            {
                _Header = value; OnPropertyChanged("Header");
            }
        }

        public string Info
        {
            get
            {
                var thisUser = DialogLogginViewModel.Users[0];
                var data = ST.IsDateRegister().ToList()[0];
                var InfoStudent = ST.GetStudentsInfoByID(thisUser.ID).ToList()[0];
                if (_Info == null)
                {
                    _Info = string.Format(" Full Name     {0} {1} {2} \n Student ID    {3}\n School          {4}\n Class            {5}", InfoStudent.LastName, InfoStudent.MiddleName, InfoStudent.Name, InfoStudent.StudentID, InfoStudent.FacultyID, InfoStudent.ClassID);
                }
                return _Info;
            }
            set
            {
                _Info = value; OnPropertyChanged("Info");
            }

        }


        private string _ReplaceSpaceToEnter;
        public string ReplaceSpaceToEnter
        {
            get
            {
                return _ReplaceSpaceToEnter;
            }

            set
            {
                if (value.Contains(' ') == false)
                    _ReplaceSpaceToEnter = value;
                else
                {
                    _ReplaceSpaceToEnter = value.Replace(' ', '\n');
                }



                OnPropertyChanged("ReplaceSpaceToEnter");
            }
        }
        private string _Info;


        public ICommand RegisterCommand { get; set; }

       

        private void OnRegisterCommand(object parameters)
        {
            
            var values = (object[])parameters;         
            TextBox txt = values[0] as TextBox;
            var thisUser = DialogLogginViewModel.Users[0];
            var DisciplineRegistered = ST.GetListDisciplineForThisUser(thisUser.ID).ToList();
            txt.Text = txt.Text.Trim();
            txt.Text = StandardWord(txt.Text);
            string[] ListLine = txt.Text.Split('\n', ' ', ',', '-', '+', '|');
           
            foreach (string Line in ListLine)
            {
                int flag = 1;

                foreach (GetListDisciplineForThisUser_Result Discipline in DisciplineRegistered)
                {
                    if (Discipline.DisciplineID == Line && Discipline.DisciplineStatus == false || Discipline.DisciplineID != Line) flag = 1;
                    else { flag = 0; break; }
                }
                if (flag == 1 && Line != " " && Line != "\n" && Line != null)
                {
                    try
                    {
                        var data = ST.IsDateRegister().ToList()[0];
                        ST.InsertRegisterStudyUnit(thisUser.ID, Line, data.SemesterID);
                        ST.SaveChanges();
                    }
                    catch { }
                }

            }


        }






        private string StandardWord(string data)
        {
            if (data.Length > 0)
            {
                while (data.IndexOf("  ") > 0)
                {
                    data = data.Replace("  ", " ");
                    data = data.Trim();
                }
            }
            return data;
        }


        void Command()
        {
            RegisterCommand = new RelayCommand<object>((p) => true, OnRegisterCommand);
        }

        public RegisterStudyUnitViewModel()
        {
            Command();
        }
    }
}
