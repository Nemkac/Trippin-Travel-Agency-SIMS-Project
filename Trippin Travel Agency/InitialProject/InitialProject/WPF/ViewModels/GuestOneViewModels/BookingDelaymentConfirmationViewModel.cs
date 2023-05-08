using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class BookingDelaymentConfirmationViewModel : ViewModelBase
    {
        public ViewModelCommand ShowRequests { get; set; }
        public BookingDelaymentConfirmationViewModel()
        {
            ShowRequests = new ViewModelCommand(GoToDelaymentRequests);
        }
        public void GoToDelaymentRequests(object sender)
        {
            GuestsBookingDelaymentRequestsInterface guestsBookingDelaymentRequestsInterface = new GuestsBookingDelaymentRequestsInterface();
            guestsBookingDelaymentRequestsInterface.Show();
            GuestOneStaticHelper.bookingDelaymentConfirmationInterface.Close();
            GuestOneStaticHelper.sendBookingDelaymentInterface.Close();
        }

    }
}
