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

        void InsertAndFindingError(string[] ListLine,StackPanel Stp)
        {
            var thisUser = DialogLogginViewModel.Users[0];
            var DisciplineRegistered = ST.GetListDisciplineForThisUser(thisUser.ID).ToList();
            string speak = string.Empty;
            foreach (string Line in ListLine)
            {
                TextBlock Announcement = new TextBlock();
                Announcement.FontSize = 14;
                int CheckDiscipline = 1;
                string ID = Line.Substring(0, 5);
                var data = ST.IsDateRegister().ToList()[0];
                foreach (GetListDisciplineForThisUser_Result Discipline in DisciplineRegistered)
                {


                    if (Discipline.DisciplineID != ID) CheckDiscipline = 1;
                    else if (Discipline.DisciplineID == ID.Trim() && Discipline.DisciplineStatus == true)
                    {
                        CheckDiscipline = 0;
                        Announcement.Foreground = new SolidColorBrush(Colors.Red);
                        Announcement.Text = ID + " Discipline had registered";
                        break;
                    }
                    else if (Discipline.DisciplineID == ID.Trim() && Discipline.DisciplineStatus == false) CheckDiscipline = 1;
                }
                if (CheckDiscipline == 1 && ID != " " && ID != "\n" && ID != null)
                {
                    try
                    {
                        
                        ST.InsertRegisterStudyUnit(thisUser.ID, ID, data.SemesterID);
                        ST.SaveChanges();
                        Announcement.Foreground = new SolidColorBrush(Colors.Green);
                        Announcement.Text = ID + " Successfully";
                    }
                    catch
                    {
                        Announcement.Foreground = new SolidColorBrush(Colors.Red);
                        Announcement.Text = ID + " Discipline not open or does not exist. Please check back...";
                    }
                }
                speak += Announcement.Text + "...";
                warningAudio.SpeakAsync(speak);
                if (Announcement != null || Announcement.Text != " ")
                    Stp.Children.Add(Announcement);
            }
        }
        private void OnRegisterCommand(object parameters)
        {

            var values = (object[])parameters;
            TextBox txtwrap = values[0] as TextBox;
            StackPanel Stp = values[1] as StackPanel;
            var thisUser = DialogLogginViewModel.Users[0];

            txtwrap.Text = StandardWord(txtwrap.Text);
            string[] ListLine = txtwrap.Text.Split(' ', ',', '-', '+', '\n');
            InsertAndFindingError(ListLine, Stp);
           
           
            ListRegistered = new ObservableCollection<GetInfoRegistered_Result>(ST.GetInfoRegistered(thisUser.ID).ToList());
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


        public ICommand DeleteRegistered { get; set; }

        private void OnDeleteRegistered(DataGrid Dtg)
        {
            var thisUser = DialogLogginViewModel.Users[0];
            foreach (GetInfoRegistered_Result DisciplineChecked in Dtg.Items)
            {
                if (DisciplineChecked.check == true)
                    ST.DeleteRegisterStudyUnit(thisUser.ID, DisciplineChecked.DisciplineID);
            }

            ListRegistered = new ObservableCollection<GetInfoRegistered_Result>(ST.GetInfoRegistered(thisUser.ID).ToList());


        }

        public ICommand InsertRegisterFromListDisciplineCommand { get; set; }
        private void OnInsertRegisterFromListDisciplineCommand(object Parameters)
        {
            var values = (object[])Parameters;
            DataGrid Dtg = values[0] as DataGrid;
            StackPanel Stp = values[1] as StackPanel;
            var data = ST.IsDateRegister().ToList()[0];
            var thisUser = DialogLogginViewModel.Users[0];  
            var DisciplineRegistered = ST.GetListDisciplineForThisUser(thisUser.ID).ToList();
            string speak = string.Empty;
            foreach (GetInfoDiscipline_Result DisciplineChecked in Dtg.Items)
            {
                TextBlock Announcement = new TextBlock();
                Announcement.FontSize = 14;
                int CheckDiscipline = 1;

                if (DisciplineChecked.check == true)
                {
                    foreach (GetListDisciplineForThisUser_Result Discipline in DisciplineRegistered)
                    {
                        if (Discipline.DisciplineID != DisciplineChecked.DisciplineID.ToString()) CheckDiscipline = 1;
                        else if (Discipline.DisciplineID == DisciplineChecked.DisciplineID.ToString().Trim() && Discipline.DisciplineStatus == true)
                        {
                            CheckDiscipline = 0;
                            Announcement.Foreground = new SolidColorBrush(Colors.Red);
                            Announcement.Text = DisciplineChecked.DisciplineID.ToString() + " Discipline had registered";
                            break;
                        }
                        else if (Discipline.DisciplineID == DisciplineChecked.DisciplineID.ToString().Trim() && Discipline.DisciplineStatus == false) CheckDiscipline = 1;
                    }
                    if (CheckDiscipline == 1 && DisciplineChecked.DisciplineID.ToString() != " " && DisciplineChecked.DisciplineID.ToString() != "\n" && DisciplineChecked.DisciplineID.ToString() != null)
                    {
                        try
                        {

                            ST.InsertRegisterStudyUnit(thisUser.ID, DisciplineChecked.DisciplineID.ToString(), data.SemesterID);
                            ST.SaveChanges();
                            Announcement.Foreground = new SolidColorBrush(Colors.Green);
                            Announcement.Text = DisciplineChecked.DisciplineID.ToString() + " Successfully";
                        }
                        catch
                        {
                            Announcement.Foreground = new SolidColorBrush(Colors.Red);
                            Announcement.Text = DisciplineChecked.DisciplineID.ToString() + " Discipline not open or does not exist. Please check back...";
                        }
                    }
                    speak += Announcement.Text + "...";
                    warningAudio.SpeakAsync(speak);
                    if (Announcement != null || Announcement.Text != " ")
                        Stp.Children.Add(Announcement);
                }
            }
            ListRegistered = new ObservableCollection<GetInfoRegistered_Result>(ST.GetInfoRegistered(thisUser.ID).ToList());
        }

        void Command()
        {
            RegisterCommand = new RelayCommand<object>((p) => true, OnRegisterCommand);
            DeleteRegistered = new RelayCommand<DataGrid>((p) => true, OnDeleteRegistered);
            InsertRegisterFromListDisciplineCommand = new RelayCommand<object>((p) => true, OnInsertRegisterFromListDisciplineCommand);
        }

        public RegisterStudyUnitViewModel()
        {
            Command();
        }
    }
}
