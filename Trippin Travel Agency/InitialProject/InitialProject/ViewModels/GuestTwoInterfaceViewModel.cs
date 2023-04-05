using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class GuestTwoInterfaceViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }


        public ICommand ShowTourViewCommand { get; }
        public ICommand ShowGuestTwoCoupons { get; }
        public GuestTwoInterfaceViewModel()
        {
            ShowTourViewCommand = new ViewModelCommand(ExecuteTourViewCommand);
            //Default view
            ShowGuestTwoCoupons = new ViewModelCommand(ExecuteShowGuestTwoCoupons);

        }

        private void ExecuteTourViewCommand(object obj)
        {
            CurrentChildView = new TourDisplayViewModel(this);
           
        }
        public void ExecuteShowDetailedTourView(object obj) {

            CurrentChildView = new DetailedTourViewModel();

        }

        public void ExecuteShowGuestTwoCoupons(object obj)
        { 
            CurrentChildView = new GuestTwoCouponsViewModel();
        }

    }
}
