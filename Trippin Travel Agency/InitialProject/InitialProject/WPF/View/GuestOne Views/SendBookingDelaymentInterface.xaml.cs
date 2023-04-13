using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for SendBookingDelaymentInterface.xaml
    /// </summary>
    public partial class SendBookingDelaymentInterface : Window
    {
        Booking selectedBooking = new Booking();
        private BookingDelaymentRequestService bookingDelaymentRequestService;
        public SendBookingDelaymentInterface()
        {
            InitializeComponent();
            BookingDelaymentRequestRepository bookingDelaymentRequestRepository = new BookingDelaymentRequestRepository();
            this.bookingDelaymentRequestService = new BookingDelaymentRequestService(bookingDelaymentRequestRepository);
        }

        public void SetAttribures(Booking selectedBooking)
        {
            this.selectedBooking = selectedBooking;
            bookingInfoLabelsBlock.Text = "Booking ID:" + "\n\nInitial arrival date" + "\n\nInitiral departure date:";
            bookingInfoBlock.Text = selectedBooking.Id + "\n\n" + selectedBooking.arrival + "\n\n" + selectedBooking.departure;
        }

        private void SendBookingDelaymentRequest(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            BookingDelaymentRequest bookingDelaymentRequest = new BookingDelaymentRequest(selectedBooking.Id, newArrival.SelectedDate.Value,newDeparture.SelectedDate.Value, Status.Pending, new string(""));
            bookingDelaymentRequestService.Save(bookingDelaymentRequest);
        }
    }
}
