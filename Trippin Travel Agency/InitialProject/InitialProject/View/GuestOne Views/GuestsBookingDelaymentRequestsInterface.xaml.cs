using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using InitialProject.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
            BookingDelaymentRequestService bookingDelaymentRequestService = new BookingDelaymentRequestService(); 
            BookingService bookingService = new BookingService();
            AccommodationService accommodationService = new AccommodationService();
            List<BookingDelaymentRequest> resolvedDelaymentRequests = userService.GetResolvedBookingDelaymentRequests();
            List<BookingDelaymentRequest> pendingDelaymentRequests = userService.GetPendingBookingDelaymentRequests();

            if (resolvedDelaymentRequests != null)
            {
                var resolvedDelaymentsToGrid = from bookingDelaymentRequest in resolvedDelaymentRequests
                                               select new
                                               {
                                                   bookingId = bookingDelaymentRequest.bookingId,
                                                   accommodationService.GetById(bookingService.GetById(bookingDelaymentRequest.bookingId).accommodationId).name,
                                                   desiredArrival = bookingDelaymentRequest.newArrival.ToString().Substring(0, 9),
                                                   desiredDeparture = bookingDelaymentRequest.newDeparture.ToString().Substring(0, 9),
                                                   bookingDelaymentRequest.status
                                               };
                this.resolvedRequestsGrid.ItemsSource = resolvedDelaymentsToGrid;

            }

            if (pendingDelaymentRequests != null)
            {
                var pendingDelaymentsToGrid = from bookingDelaymentRequest in pendingDelaymentRequests
                                              select new
                                              {
                                                  bookingId = bookingDelaymentRequest.bookingId,
                                                  accommodationService.GetById(bookingService.GetById(bookingDelaymentRequest.bookingId).accommodationId).name,
                                                  desiredArrival = bookingDelaymentRequest.newArrival.ToString().Substring(0, 9),
                                                  desiredDeparture = bookingDelaymentRequest.newDeparture.ToString().Substring(0, 9),
                                                  bookingDelaymentRequest.status
                                              };
                this.pendingRequestsGrid.ItemsSource = pendingDelaymentsToGrid;
            }
        }

        private void ShowResolvedRequestComment(object sender, RoutedEventArgs e)
        {
            DelaymentRequestComment delaymentRequestComment = new DelaymentRequestComment();
            UserService userService = new UserService();
            
            int selectedRowIndex  = resolvedRequestsGrid.SelectedIndex;

            delaymentRequestComment.WindowStartupLocation = WindowStartupLocation.Manual;
            delaymentRequestComment.Left = this.Left + (this.Width - delaymentRequestComment.Width) / 2;
            delaymentRequestComment.Top = this.Top + (this.Height - delaymentRequestComment.Height) / 2;
            delaymentRequestComment.Topmost = true;
            delaymentRequestComment.ownersComment.Text = userService.GetResolvedBookingDelaymentRequests()[selectedRowIndex].comment;
            delaymentRequestComment.Show();
        }

        private void ShowPendingRequestComment(object sender, RoutedEventArgs e)
        {
            DelaymentRequestComment delaymentRequestComment = new DelaymentRequestComment();
            UserService userService = new UserService();

            int selectedRowIndex = pendingRequestsGrid.SelectedIndex;

            delaymentRequestComment.WindowStartupLocation = WindowStartupLocation.Manual;
            delaymentRequestComment.Left = this.Left + (this.Width - delaymentRequestComment.Width) / 2;
            delaymentRequestComment.Top = this.Top + (this.Height - delaymentRequestComment.Height) / 2;
            delaymentRequestComment.Topmost = true;
            delaymentRequestComment.ownersComment.Text = userService.GetPendingBookingDelaymentRequests()[selectedRowIndex].comment;
            delaymentRequestComment.Show();
        }

        private void DeleteResolvedRequest(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = resolvedRequestsGrid.SelectedIndex;
            UserService userService = new UserService();
            BookingDelaymentRequestService bookingDelaymentRequestService = new BookingDelaymentRequestService();
            bookingDelaymentRequestService.Delete(userService.GetResolvedBookingDelaymentRequests()[selectedRowIndex]);
        }

        private void DeletePendingRequest(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = pendingRequestsGrid.SelectedIndex;
            UserService userService = new UserService();
            BookingDelaymentRequestService bookingDelaymentRequestService = new BookingDelaymentRequestService();
            bookingDelaymentRequestService.Delete(userService.GetPendingBookingDelaymentRequests()[selectedRowIndex]);
        }
    }
}
