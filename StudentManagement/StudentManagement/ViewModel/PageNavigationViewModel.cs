using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentManagement.ViewModel
{
    public class PageNavigationViewModel : ViewModelBase
    {
        StudentDBEntities ST = new StudentDBEntities();
        private string largestPageNumber = "0";

        public string LargestPageNumber
        {
            get
            {
                return largestPageNumber;
            }

            set
            {
                if (largestPageNumber != value)
                    largestPageNumber = value;
                OnPropertyChanged("LargestPageNumber");
            }
        }

        public string CurrentPageNumber
        {
            get
            {
                return currentPageNumber;
            }

            set
            {
                if (currentPageNumber != value)
                    currentPageNumber = value;
                OnPropertyChanged("CurrentPageNumber");
            }
        }



        private string currentPageNumber = "1";
        private int DevideRoundingUp(int para1, int para2)
        {
            int remainder;
            int quotient = Math.DivRem(para1, para2, out remainder);
            return remainder == 0 ? quotient : quotient + 1;
        }
        private void Init()
        {
            int numberOfAnouncement = 0;
            if (ST.CountGeneralAnouncement() != null)
            {
                try { numberOfAnouncement = int.Parse(ST.CountGeneralAnouncement().ToList()[0].ToString()); }
                catch { }

                LargestPageNumber = DevideRoundingUp(numberOfAnouncement, 5).ToString();
            }


        }

        private ICommand _FirstPageCommand;
        public ICommand FirstPageCommand
        {
            get
            {
                if (_FirstPageCommand == null)
                    _FirstPageCommand = new RelayCommand<object>((p) => true, OnFirstPageCommand);
                return _FirstPageCommand;
            }
        }
        private void OnFirstPageCommand(object obj)
        {
            CurrentPageNumber = 1.ToString();
            Messager.CurrentPageBroadCast(CurrentPageNumber);
        }

        private ICommand _LastPageCommand;
        public ICommand LastPageCommand
        {
            get
            {
                if (_LastPageCommand == null)
                    _LastPageCommand = new RelayCommand<object>((p) => true, OnLastPageCommand);
                return _LastPageCommand;
            }
        }
        private void OnLastPageCommand(object obj)
        {
            CurrentPageNumber = LargestPageNumber.ToString();
            Messager.CurrentPageBroadCast(CurrentPageNumber);
        }

        private ICommand _PreviousPageCommand;
        public ICommand PreviousPageCommand
        {
            get
            {
                if (_PreviousPageCommand == null)
                    _PreviousPageCommand = new RelayCommand<object>((p) => true, OnPreviousPageCommand);
                return _PreviousPageCommand;
            }
        }
        private void OnPreviousPageCommand(object obj)
        {
            if (CurrentPageNumber != "1")
                CurrentPageNumber = (Convert.ToInt32(CurrentPageNumber) - 1).ToString();
            Messager.CurrentPageBroadCast(CurrentPageNumber);
        }

        private ICommand _NextPageCommand;
        public ICommand NextPageCommand
        {
            get
            {
                if (_NextPageCommand == null)
                    _NextPageCommand = new RelayCommand<object>((p) => true, OnNextPageCommand);
                return _NextPageCommand;
            }
        }
        private void OnNextPageCommand(object obj)
        {
            if (CurrentPageNumber != LargestPageNumber)
                CurrentPageNumber = (Convert.ToInt32(CurrentPageNumber) + 1).ToString();
            Messager.CurrentPageBroadCast(CurrentPageNumber);
        }

        private ICommand _GoToPageCommand;
        public ICommand GoToPageCommand
        {
            get
            {
                if (_GoToPageCommand == null)
                    _GoToPageCommand = new RelayCommand<TextBox>((p) => true, OnGoToPageCommand);
                return _GoToPageCommand;
            }
        }
        private void OnGoToPageCommand(TextBox currentTextBox)
        {
            try
            {
                if (Convert.ToInt32(currentTextBox.Text) >= 1 && Convert.ToInt32(currentTextBox.Text) <= Convert.ToInt32(LargestPageNumber))
                {
                    CurrentPageNumber = currentTextBox.Text;
                    Messager.CurrnentPageMessageTransmitted(CurrentPageNumber);
                }
            }
            catch { }
            Messager.CurrentPageBroadCast(CurrentPageNumber);
        }

        public PageNavigationViewModel()
        {
            Init();
        }

    }
}
