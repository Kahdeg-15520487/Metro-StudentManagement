using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class AcademicViewModel : ViewModelBase
    {

        StudentDBEntities ST = new StudentDBEntities();

        #region DetailCommand, which shows Detail about a Dicipline
        //This hold all the detail about a Discipline
        private ObservableCollection<GetDetailDisciplineByID_Result> _SubjectDetail;
        public ObservableCollection<GetDetailDisciplineByID_Result> SubjectDetail
        {
            get
            {
                if (_SubjectDetail == null)
                {
                    _SubjectDetail = new ObservableCollection<GetDetailDisciplineByID_Result>();
                }
                return _SubjectDetail;
            }
            set
            {
                if (_SubjectDetail != value)
                {
                    _SubjectDetail = value; OnPropertyChanged("SubjectDetail");
                }
            }
        }
        public ICommand DetailCommand { get; set; }
        private string selectedSubjectID = string.Empty;

        private void InitDetailCommand()
        {
            DetailCommand = new RelayCommand<DataGrid>((p) => true, (p) =>
            {

                GetAcademicByID_Result data = (GetAcademicByID_Result)p.SelectedItem;
                selectedSubjectID = data.DisciplineID;
                SubjectDetail = new ObservableCollection<GetDetailDisciplineByID_Result>(ST.GetDetailDisciplineByID(selectedSubjectID).ToList());

            });
        }

        #endregion

        #region AcademicList, which shows Student's Mark

        private ObservableCollection<object> _AcademicList;
        public ObservableCollection<object> AcademicList
        {
            get
            {
                if (_AcademicList == null)
                {
                    var thisUser = DialogLogginViewModel.Users[0];
                    if (thisUser.Roll == 0)
                    {
                        _AcademicList = new ObservableCollection<object>(ST.GetAcademicByID(thisUser.ID).ToList());
                    }

                }
                return _AcademicList;
            }
            set
            {
                if (_AcademicList != value)
                {
                    _AcademicList = value; OnPropertyChanged("AcademicList");
                }
            }
        }

        #endregion

        public AcademicViewModel()
        {
            InitDetailCommand();
        }
    }
}

