using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace StudentManagement.ViewModel
{
    class RegisterStudyUnitViewModel : ViewModelBase
    {

        StudentDBEntities ST = new StudentDBEntities();
        SpeechSynthesizer warningAudio = new SpeechSynthesizer();
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

        private ObservableCollection<GetInfoRegistered_Result> _ListRegistered;
        public ObservableCollection<GetInfoRegistered_Result> ListRegistered
        {
            get
            {
                var thisUser = DialogLogginViewModel.Users[0];
                if (_ListRegistered == null)
                {
                    _ListRegistered = new ObservableCollection<GetInfoRegistered_Result>(ST.GetInfoRegistered(thisUser.ID).ToList());
                }
                return _ListRegistered;
            }

            set
            {
                if (_ListRegistered != value)
                {
                    _ListRegistered = value; OnPropertyChanged("ListRegistered");
                }
            }
        }

        private ObservableCollection<GetInfoDiscipline_Result> _ListDiscipline;
        public ObservableCollection<GetInfoDiscipline_Result> ListDiscipline
        {
            get
            {
                
                if (_ListDiscipline == null)
                {
                    _ListDiscipline = new ObservableCollection<GetInfoDiscipline_Result>(ST.GetInfoDiscipline().ToList());
                }
                return _ListDiscipline;
            }

            set
            {
                if (_ListDiscipline != value)
                {
                    _ListDiscipline = value; OnPropertyChanged("ListDiscipline");
                }
            }
        }

        private void OnRegisterCommand(object parameters)
        {

            var values = (object[])parameters;
            TextBox txtwrap = values[0] as TextBox;
            StackPanel Stp = values[1] as StackPanel;
            var thisUser = DialogLogginViewModel.Users[0];
            var DisciplineRegistered = ST.GetListDisciplineForThisUser(thisUser.ID).ToList();
            string deleteEnter = txtwrap.Text.Replace('\n', ' ');
            deleteEnter = StandardWord(deleteEnter);
            string[] ListLine = deleteEnter.Split(' ', ',', '-', '+', '|');
            string speak=string.Empty;
            foreach (string Line in ListLine)
            {
                TextBlock Announcement = new TextBlock();
                Announcement.FontSize = 14;
                int flag = 1;

                foreach (GetListDisciplineForThisUser_Result Discipline in DisciplineRegistered)
                {
                    if (Discipline.DisciplineID != Line.Trim()) flag = 1;
                    else if (Discipline.DisciplineID == Line.Trim() && Discipline.DisciplineStatus == true)
                    {
                        flag = 0;
                        Announcement.Foreground = new SolidColorBrush(Colors.Red);
                        Announcement.Text = Line + " Discipline had registered";             
                        break;
                    }
                    else if (Discipline.DisciplineID == Line.Trim() && Discipline.DisciplineStatus == false) flag = 1;
                }
                if (flag == 1 && Line != " " && Line != "\n" && Line != null)
                {
                    try
                    {
                        var data = ST.IsDateRegister().ToList()[0];
                        ST.InsertRegisterStudyUnit(thisUser.ID, Line, data.SemesterID);
                        ST.SaveChanges();
                        Announcement.Foreground = new SolidColorBrush(Colors.Green);
                        Announcement.Text = Line + " successfully";
                    }
                    catch
                    {
                        Announcement.Foreground = new SolidColorBrush(Colors.Red);
                        Announcement.Text = Line + "   Discipline not open or does not exist. Please check back...";
                    }
                }
                speak += Announcement.Text + "...";
                warningAudio.SpeakAsync(speak);
                if (Announcement != null || Announcement.Text != " ")
                    Stp.Children.Add(Announcement);
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
