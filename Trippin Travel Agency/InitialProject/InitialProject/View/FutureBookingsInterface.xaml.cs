using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Service;
using InitialProject.Model;
using InitialProject.Context;



namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for FutureBookingsInterface.xaml
    /// </summary>
    public partial class FutureBookingsInterface : Window
    {
        public FutureBookingsInterface()
        {
            InitializeComponent();
            this.Loaded += ShowFutureBookings;
        }

        public void ShowFutureBookings(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            futureBookingsGrid.ItemsSource = userService.GetGuestsFutureBookings(LoggedUser.id);
        }

        private void ShowPastBookings(object sender, RoutedEventArgs e)
        {
            PastBookingsInterface pastBookingsInterface = new PastBookingsInterface();
            pastBookingsInterface.Show();
        }

        private void DeleteBooking(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            // brisanje bukinga
        }

        private void GoToBookingDelayment(object sender, RoutedEventArgs e)
        {
            SendBookingDelaymentInterface sendBookingDelaymentInterface = new SendBookingDelaymentInterface();
            sendBookingDelaymentInterface.SetAttribures((Booking)futureBookingsGrid.SelectedItem);
            sendBookingDelaymentInterface.Show();
        }

        private void ShowDetailed(object sender, RoutedEventArgs e)
        {

        }
    }
}
