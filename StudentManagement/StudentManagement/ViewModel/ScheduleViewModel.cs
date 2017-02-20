using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    class ScheduleViewModel : ViewModelBase
    {
        int i = 0;
        StudentDBEntities ST = new StudentDBEntities();
        SpeechSynthesizer warningAudio = new SpeechSynthesizer();
        int[] Period = { 1, 2, 3, 4 };
        #region Monday
        private ObservableCollection<GetScheduleForDetail1_Result> _Monday;

        public ObservableCollection<GetScheduleForDetail1_Result> Monday
        {
            get
            {

                return _Monday;
            }
            set
            {
                if (_Monday != value)
                {
                    _Monday = value; OnPropertyChanged("Monday");
                }
            }
        }
        #endregion
        #region Tuesday
        private ObservableCollection<GetScheduleForDetail1_Result> _Tuesday;

        public ObservableCollection<GetScheduleForDetail1_Result> Tuesday
        {
            get
            {

                return _Tuesday;
            }
            set
            {
                if (_Tuesday != value)
                {
                    _Tuesday = value; OnPropertyChanged("Tuesday");
                }
            }
        }
        #endregion
        #region Wednesday
        private ObservableCollection<GetScheduleForDetail1_Result> _Wednesday;

        public ObservableCollection<GetScheduleForDetail1_Result> Wednesday
        {
            get
            {

                return _Wednesday;
            }
            set
            {
                if (_Wednesday != value)
                {
                    _Wednesday = value; OnPropertyChanged("Wednesday");
                }
            }
        }
        #endregion
        #region Ads emptys
        void AddEmpty(ObservableCollection<GetScheduleForDetail1_Result> Day)
        {
            foreach (int Child in Period)
            {
                int Credits = 0;
                foreach (GetScheduleForDetail1_Result Schedule in Day)
                {
                    if (Child == Schedule.Period)
                    {
                        Credits = 1;
                        break;
                    }
                }

                if (Credits == 0)
                {
                    for (int Schedule = 0; Schedule < Day.Count; Schedule++)
                    {
                        if (Child + 1 == Day[Schedule].Period)
                        {
                            Credits = int.Parse(Day[Schedule].Credits.ToString()); break;
                        }
                        if (Child == 4 && Child - 1 == Day[Schedule].Period && Day[Day.Count - 1].DepartmentID != null) { Credits = int.Parse(Day[Schedule].Credits.ToString()); break; }
                    }
                    if (Credits != 0)
                    {
                        GetScheduleForDetail1_Result IconEmpty = new GetScheduleForDetail1_Result() { Period = Child - 1, Credits = (5 * Height - Credits) };
                        Day.Insert(int.Parse(IconEmpty.Period.ToString()), IconEmpty);
                    }
                }
            }
        }
        #endregion
        #region Thursday
        private ObservableCollection<GetScheduleForDetail1_Result> _Thursday;

        public ObservableCollection<GetScheduleForDetail1_Result> Thursday
        {
            get
            {

                return _Thursday;
            }
            set
            {
                if (_Thursday != value)
                {
                    _Thursday = value; OnPropertyChanged("Thursday");
                }
            }
        }
        #endregion
        #region Friday
        private ObservableCollection<GetScheduleForDetail1_Result> _Friday;

        public ObservableCollection<GetScheduleForDetail1_Result> Friday
        {
            get
            {

                return _Friday;
            }
            set
            {
                if (_Friday != value)
                {
                    _Friday = value; OnPropertyChanged("Friday");
                }
            }
        }

        #endregion
        #region Saturday

        private ObservableCollection<GetScheduleForDetail1_Result> _Saturday;

        public ObservableCollection<GetScheduleForDetail1_Result> Saturday
        {
            get
            {

                return _Saturday;
            }
            set
            {
                if (_Saturday != value)
                {
                    _Saturday = value; OnPropertyChanged("Saturday");
                }
            }
        }
        #endregion

        private double _Height;

        public ICommand GetHeightALesson { get; set; }

        public double Height
        {
            get
            {
                return _Height;
            }

            set
            {
                _Height = value;
                OnPropertyChanged("Height");
            }
        }
        private void OnGetHeightALesson(Grid Grd)
        {
            var data = ST.IsDateRegister().ToList()[0];
            var thisUser = DialogLogginViewModel.Users[0];
            Height = Math.Round(Grd.ActualHeight / 11-1, 0);
            Monday = new ObservableCollection<GetScheduleForDetail1_Result>(ST.GetScheduleForDetail1(thisUser.ID, data.SemesterName, "Monday", Height));
            Tuesday = new ObservableCollection<GetScheduleForDetail1_Result>(ST.GetScheduleForDetail1(thisUser.ID, data.SemesterName, "Tuesday", Height));
            Wednesday = new ObservableCollection<GetScheduleForDetail1_Result>(ST.GetScheduleForDetail1(thisUser.ID, data.SemesterName, "Wednesday", Height));
            Thursday = new ObservableCollection<GetScheduleForDetail1_Result>(ST.GetScheduleForDetail1(thisUser.ID, data.SemesterName, "Thursday", Height));
            Friday = new ObservableCollection<GetScheduleForDetail1_Result>(ST.GetScheduleForDetail1(thisUser.ID, data.SemesterName, "Friday", Height));
            Saturday = new ObservableCollection<GetScheduleForDetail1_Result>(ST.GetScheduleForDetail1(thisUser.ID, data.SemesterName, "Saturday", Height));
            AddEmpty(Monday);
            AddEmpty(Tuesday);
            AddEmpty(Wednesday);
            AddEmpty(Thursday);
            AddEmpty(Friday);
            AddEmpty(Saturday);
        }



        public ScheduleViewModel()
        {
            GetHeightALesson = new RelayCommand<Grid>((p) => true, OnGetHeightALesson);



        }



    }
}
