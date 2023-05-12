using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class RateAccommodationConfirmationViewModel : ViewModelBase
    {
        public ViewModelCommand GoToPastBookings { get; set; }
        public RateAccommodationConfirmationViewModel()
        {
            GoToPastBookings = new ViewModelCommand(ShowPastBookings);
        }
        public void ShowPastBookings(object sender)
        {
            GuestOneStaticHelper.pastBookingsInterface.Show();
            GuestOneStaticHelper.rateAccommodationInterface.Close();
            GuestOneStaticHelper.rateAccommodationConfirmationInterface.Close();
        }
    }
}
