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
    public class BehaviorViewModel : ViewModelBase
    {

        public string GroupName = string.Empty;
        public string Activities_ = string.Empty;
        public BehaviorViewModel()
        {
            //Command Selection change  activity group
            ActivitiesCommand = new RelayCommand<ListBox>((p) => true, (p) =>
            {
                GetActivityGroup_Result data = (GetActivityGroup_Result)p.SelectedItem;
                GroupName = data.GroupName;
                Activities = new ObservableCollection<object>(ST.GetActivities(GroupName).ToList());
            });

            //Command selection change activies

            DetailCommand = new RelayCommand<ListBox>((p) => true, (p) =>
            {

                if (p.Items.Count <= 0) Activities_ = string.Empty;
                else
                {
                    try
                    {
                        GetActivities_Result data = (GetActivities_Result)p.SelectedItem;
                        Activities_ = data.ActivityName;
                    }
                    catch { }
                }
                if (p.SelectedIndex < 0) p.SelectedIndex = 0;
                DetailActivity = new ObservableCollection<object>(ST.GetDetailActivity(Activities_).ToList());
            });
        }

        //List Activivy Group
        StudentDBEntities ST = new StudentDBEntities();
        private ObservableCollection<object> _ActiveGroup;
        public ObservableCollection<object> ActiveGroup
        {
            get
            {
                if (_ActiveGroup == null)
                {
                    _ActiveGroup = new ObservableCollection<object>(ST.GetActivityGroup().ToList());
                }
                return _ActiveGroup;
            }
            set
            {
                if (_ActiveGroup != value)
                {
                    _ActiveGroup = value; OnPropertyChanged("ActiveGroup");
                }
            }
        }
        //List Activities
        private ObservableCollection<object> _Activities;
        public ObservableCollection<object> Activities
        {
            get
            {
                if (_Activities == null)
                {
                    _Activities = new ObservableCollection<object>(ST.GetActivities(GroupName).ToList());
                }
                return _Activities;
            }
            set
            {
                if (_Activities != value)
                {
                    _Activities = value; OnPropertyChanged("Activities");
                }
            }
        }
        //List DetailActivity
        private ObservableCollection<object> _DetailActivity;
        public ObservableCollection<object> DetailActivity
        {
            get
            {
                if (_DetailActivity == null)
                {
                    _DetailActivity = new ObservableCollection<object>(ST.GetDetailActivity(Activities_).ToList());
                }
                return _DetailActivity;
            }
            set
            {
                if (_DetailActivity != value)
                {
                    _DetailActivity = value; OnPropertyChanged("DetailActivity");
                }
            }
        }
        public ICommand ActivitiesCommand { get; set; }
        public ICommand DetailCommand { get; set; }
    }
}
