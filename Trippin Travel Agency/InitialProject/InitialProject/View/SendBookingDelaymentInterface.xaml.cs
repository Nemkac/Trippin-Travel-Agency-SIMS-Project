using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for SendBookingDelaymentInterface.xaml
    /// </summary>
    public partial class SendBookingDelaymentInterface : Window
    {
        Booking selectedBooking = new Booking();
        public SendBookingDelaymentInterface()
        {
            InitializeComponent();
        }

        public void SetAttribures(Booking selectedBooking)
        {
            this.selectedBooking = selectedBooking;
        }

        private void SendBookingDelaymentRequest(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            BookingDelaymentRequestService bookingDelaymentRequestService = new BookingDelaymentRequestService();
            BookingDelaymentRequest bookingDelaymentRequest = new BookingDelaymentRequest(selectedBooking.Id, newArrival.SelectedDate.Value,newDeparture.SelectedDate.Value, Status.Pending, new string(""));
            BookingDelaymentRequestService.Save(bookingDelaymentRequest);
        }
    }
}
