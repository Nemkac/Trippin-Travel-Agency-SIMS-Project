using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.WPF.ViewModels;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{

    public class TourDisplayViewModel : ViewModelBase
    {
        public static bool CanExecute { get; set; }

        public ViewModelCommand DetailedTourViewCommand { get; private set; }
        public ViewModelCommand BookingConfirmationViewCommand { get; private set; }

        private readonly GuestTwoInterfaceViewModel _mainViewModel;

        public TourDisplayViewModel(GuestTwoInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            DetailedTourViewCommand = new ViewModelCommand(ShowDetailedTourView);
            BookingConfirmationViewCommand = new ViewModelCommand(ShowBookingConfirmation);
        }

        public void ShowDetailedTourView(object obj)
        {
            _mainViewModel.ExecuteShowDetailedTourView(null);
        }

        public void ShowBookingConfirmation(object obj)
        {


            if (CanExecute == true)
            {

                _mainViewModel.ExecuteShowBookingConfirmation(null);
            }
        }
    }
}
