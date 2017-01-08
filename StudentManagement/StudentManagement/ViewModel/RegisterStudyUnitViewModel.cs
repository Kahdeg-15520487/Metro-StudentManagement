using System;
using System.Collections.Generic;
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


        private string FuckingTestingString;
        public string FuckingTestingString1
        {
            get
            {
                return FuckingTestingString;
            }

            set
            {
                if (value.Contains(' ') == false)
                    FuckingTestingString = value;
                else
                {
                    FuckingTestingString = value.Replace(' ', '\n');
                }



                OnPropertyChanged("FuckingTestingString1");
            }
        }
        private string _Info;

        public ICommand RichTextBoxSelectionChangeCommand { get; set; }
        private void OnRichTextBoxSelectionChangeCommand(object obj)
        {
        }




        public ICommand RegisterCommand { get; set; }



        private void OnRegisterCommand(RichTextBox Rtb)
        {
            TextRange textRange = new TextRange(Rtb.Document.ContentStart, Rtb.Document.ContentEnd);
            textRange.Text = textRange.Text.Trim();
            textRange.Text = StandardWord(textRange.Text);
            string[] ListLine = textRange.Text.Split('\n', ' ', ',', '-', '+', '|');
            foreach (string Line in ListLine)
            {

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
            RichTextBoxSelectionChangeCommand = new RelayCommand<object>((p) => true, OnRichTextBoxSelectionChangeCommand);
        }

        public RegisterStudyUnitViewModel()
        {
            Command();
        }
    }
}
