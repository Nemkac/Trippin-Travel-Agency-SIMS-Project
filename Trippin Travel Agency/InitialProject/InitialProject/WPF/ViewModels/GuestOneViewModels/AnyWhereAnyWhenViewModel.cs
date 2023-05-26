using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class AnyWhereAnyWhenViewModel : ViewModelBase
    {
        public AnyWhereAnyWhenViewModel()
        {
            Test = "testara";
        }

        private string test;
        public string Test
        {
            get { return test; }
            set
            {
                if (test != value)
                {
                    test = value;
                    OnPropertyChanged(nameof(Test));
                }
            }
        }
    }
}
