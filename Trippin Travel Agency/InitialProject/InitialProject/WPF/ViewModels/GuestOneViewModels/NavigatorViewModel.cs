using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class NavigatorViewModel : ViewModelBase
    {
        public ViewModelCommand GoToBookings { get; set; }
        public ViewModelCommand GoToHome { get; set; }
        public ViewModelCommand GoReviews { get; set; }
        public ViewModelCommand GoAccount { get; set; }
        public ViewModelCommand GoDelaymentRequests { get; set; }
        public NavigatorViewModel()
        {
            GoToBookings = new ViewModelCommand(GoToFutureBookings);
            GoToHome = new ViewModelCommand(GoHome);
            GoReviews = new ViewModelCommand(GoToReviews);
            GoAccount = new ViewModelCommand(GoToAccount);
            GoDelaymentRequests = new ViewModelCommand(GoToDelaymentRequests);

        }

        public void GoToFutureBookings(object sender)
        {
            
        }

        public void GoToDelaymentRequests(object sender)
        {
            CloseInterfaces();
            FutureBookingsInterface futureBookingsInterface = new FutureBookingsInterface();
            futureBookingsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            futureBookingsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            futureBookingsInterface.Show();
        }

        public void GoHome(object sender)
        {
            GuestOneStaticHelper.guestOneInterface.Show();
            CloseInterfaces();
        }

        public void GoToReviews(object sender)
        {
            CloseInterfaces();
            GuestsReviewsInterface guestsReviewsInterface = new GuestsReviewsInterface();
            guestsReviewsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            guestsReviewsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestsReviewsInterface.Show();
        }

        public void GoToAccount(object sender)
        {
            CloseInterfaces();
            GuestsAccountInterface accountInterface = new GuestsAccountInterface();
            accountInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            accountInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            accountInterface.Show();
        }

        public void CloseInterfaces()
        {
            GuestOneStaticHelper.navigator.Close();
            GuestOneStaticHelper.bookAccommodationInterface?.Hide();
            GuestOneStaticHelper.pastBookingsInterface?.Hide();
            GuestOneStaticHelper.rateAccommodationInterface?.Hide();
            GuestOneStaticHelper.sendBookingDelaymentInterface?.Hide();
            GuestOneStaticHelper.futureBookingsInterface?.Hide();
            GuestOneStaticHelper.guestsReviewsInterface?.Hide();
            GuestOneStaticHelper.renovationSuggestionInterface?.Hide();
            GuestOneStaticHelper.guestsAccountInterface?.Hide();
        }
    }
}
