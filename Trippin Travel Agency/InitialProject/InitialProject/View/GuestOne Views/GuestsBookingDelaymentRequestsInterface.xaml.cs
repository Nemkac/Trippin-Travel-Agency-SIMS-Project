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
    /// Interaction logic for GuestsBookingDelaymentRequestsInterface.xaml
    /// </summary>
    public partial class GuestsBookingDelaymentRequestsInterface : Window
    {
        public GuestsBookingDelaymentRequestsInterface()
        {
            InitializeComponent();
            this.Loaded += ShowDelaymentRequests;
        }

        public void ShowDelaymentRequests(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            UserService userService = new UserService();
            BookingService bookingService = new BookingService();
            AccommodationService accommodationService = new AccommodationService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = userService.GetBookingDelaymentRequests(LoggedUser.id);

            var bookingDelayments = from bookingDelaymentRequest in bookingDelaymentRequests
                                    select new
                                    {
                                        bookingDelaymentRequest.bookingId,
                                        accommodationService.GetById(bookingService.GetById(bookingDelaymentRequest.bookingId).accommodationId).name,
                                        newArrival = bookingDelaymentRequest.newArrival.ToString().Substring(0, 9),
                                        newDeparture = bookingDelaymentRequest.newDeparture.ToString().Substring(0, 9),
                                        bookingDelaymentRequest.status
                                    };

            this.bookingDelaymentRequestsGrid.ItemsSource = bookingDelayments;
        }
    }
}
