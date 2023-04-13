using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Input;
using InitialProject.WPF.ViewModels;

namespace InitialProject.WPF.ViewModels
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
        public ICommand ShowBookingConfirmation { get; }
        public ICommand ShowGuestTwoMessages { get; }

        public ICommand ShowLiveTour { get; }


        public GuestTwoInterfaceViewModel()
        {
            ShowTourViewCommand = new ViewModelCommand(ExecuteTourViewCommand);
            //Default view
            ShowGuestTwoCoupons = new ViewModelCommand(ExecuteShowGuestTwoCoupons);

            ShowLiveTour = new ViewModelCommand(ExecuteShowLiveTour);

            ShowGuestTwoMessages = new ViewModelCommand(ExecuteShowGuestTwoMessages);

        }

        private void ExecuteTourViewCommand(object obj)
        {
            CurrentChildView = new TourDisplayViewModel(this);

        }
        public void ExecuteShowDetailedTourView(object obj)
        {

            CurrentChildView = new DetailedTourViewModel();

        }

        public void ExecuteShowGuestTwoCoupons(object obj)
        {
            CurrentChildView = new GuestTwoCouponsViewModel();
        }

        public void ExecuteShowBookingConfirmation(object obj)
        {
            CurrentChildView = new TourConfirmationViewModel(); // OVO MENJAJ NA NOVO IZBACIVANJE 
        }

        public void ExecuteShowLiveTour(object obj)
        {
            CurrentChildView = new JoinLiveTourViewModel();
        }

        public void ExecuteShowGuestTwoMessages(object obj)
        {
            CurrentChildView = new GuestTwoMesagesViewModel();
        }

    }
}
