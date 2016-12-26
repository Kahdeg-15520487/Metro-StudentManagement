using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get { return CreateViewModel<MainViewModel>(); }
        }

        private T CreateViewModel<T>() where T : new()
        {
            return new T();
        }
    }
}
