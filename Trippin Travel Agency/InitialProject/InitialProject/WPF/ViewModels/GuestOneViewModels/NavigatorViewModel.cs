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
    public class NavigatorViewModel
    {
        public ViewModelCommand GoToBookings { get; set; }
        public ViewModelCommand GoToHome { get; set; }
        public NavigatorViewModel()
        {
            GoToBookings = new ViewModelCommand(GoToFutureBookings);
            GoToHome = new ViewModelCommand(GoHome);
        }
        public void GoToFutureBookings(object sender)
        {
            GuestOneStaticHelper.futureBookingsInterface?.Close();
            FutureBookingsInterface futureBookingsInterface = new FutureBookingsInterface();
            futureBookingsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            futureBookingsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            futureBookingsInterface.Show();
            GuestOneStaticHelper.futureBookingsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            GuestOneStaticHelper.navigator.Close();
            GuestOneStaticHelper.bookAccommodationInterface?.Close();
            GuestOneStaticHelper.pastBookingsInterface?.Close();
            GuestOneStaticHelper.rateAccommodationInterface?.Close();
            GuestOneStaticHelper.sendBookingDelaymentInterface?.Close();
        }

        public void GoHome(object sender)
        {
            GuestOneStaticHelper.futureBookingsInterface?.Close();
            GuestOneStaticHelper.guestOneInterface.Show();
            GuestOneStaticHelper.navigator.Close();
            GuestOneStaticHelper.bookAccommodationInterface?.Close();
            GuestOneStaticHelper.pastBookingsInterface?.Close();
            GuestOneStaticHelper.rateAccommodationInterface?.Close();
            GuestOneStaticHelper.sendBookingDelaymentInterface?.Close();
        }
    }
}
