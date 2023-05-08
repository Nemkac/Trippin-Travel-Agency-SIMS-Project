using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock.Layout;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class BookingConfirmationViewModel : ViewModelBase
    {
      
        public ViewModelCommand Ok { get; set; }
        public ViewModelCommand ShowBookings { get; set; }
        public BookingConfirmationViewModel()
        {
            Ok = new ViewModelCommand(Continue);
            ShowBookings = new ViewModelCommand(GoToBookings);
        }

        public void Continue(object sender)
        {
            GuestOneStaticHelper.bookAccommodationInterface.Show();
            GuestOneStaticHelper.bookingConfirmationInterface.Close();
            GuestOneStaticHelper.bookAccommodationInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");

        }

        public void GoToBookings(object sender)
        {
            FutureBookingsInterface futureBookingsInterface = new FutureBookingsInterface();
            futureBookingsInterface.Show();
            GuestOneStaticHelper.bookAccommodationInterface.Close();
            GuestOneStaticHelper.bookingConfirmationInterface.Close();
        }


    }
}
