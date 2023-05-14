using InitialProject.Context;
using Microsoft.Data.Sqlite;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Configuration;
using InitialProject.DTO;
using System.Diagnostics;
using System.Xml.Linq;
using InitialProject.WPF.ViewModels.GuestOneViewModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;

namespace InitialProject.WPF.View.GuestOne_Views
{

    public partial class GuestOneInterface : Window
    {
        public Accommodation selectedAccommodation = new Accommodation();
        public DateTime selectedDate = new DateTime();

        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingDelaymentRequestService bookingDelaymentRequestService;

        public GuestOneInterface()
        {
            this.DataContext = new GuestOneViewModel();
            InitializeComponent();
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            accommodationRepository = new AccommodationRepository();
            this.accommodationService = new AccommodationService(accommodationRepository);
            this.bookingDelaymentRequestService = new(new BookingDelaymentRequestRepository());
            GuestOneStaticHelper.guestOneInterface = this;
        }


        private void GoToGuestsReviews(object sender, RoutedEventArgs e)
        {
            GuestsReviewsInterface GuestsReviewsInterface = new GuestsReviewsInterface();
            GuestsReviewsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            GuestsReviewsInterface.Left = this.Left;
            GuestsReviewsInterface.Top = this.Top;
            GuestsReviewsInterface.Show();
            this.Hide();
        }

        private void GoToBookings(object sender, RoutedEventArgs e)
        {
            FutureBookingsInterface futureBookingsInterface = new FutureBookingsInterface();
            futureBookingsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            futureBookingsInterface.Left = this.Left;
            futureBookingsInterface.Top = this.Top;
            this.Hide();
            futureBookingsInterface.Show();
        }

        private void GoToBookingDelaymentRequests(object sender, RoutedEventArgs e)
        {
            GuestsBookingDelaymentRequestsInterface guestsBookingDelaymentRequestsInterface = new GuestsBookingDelaymentRequestsInterface();
            guestsBookingDelaymentRequestsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            guestsBookingDelaymentRequestsInterface.Left = this.Left;
            guestsBookingDelaymentRequestsInterface.Top = this.Top;
            this.Hide();
            guestsBookingDelaymentRequestsInterface.Show();
        }

        public void SendBookingDelaymentUpdate(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            if (userService.GetResolvedBookingDelaymentRequests() != null)
            {
                List<DelaymentRequestUpdate> delaymentRequestUpdates = new List<DelaymentRequestUpdate>();
                foreach (BookingDelaymentRequest bookingDelaymentRequest in userService.GetResolvedBookingDelaymentRequests())
                {
                    FIllRequestUpdateComment(bookingDelaymentRequestService, delaymentRequestUpdates, bookingDelaymentRequest);
                }
                foreach (DelaymentRequestUpdate delaymentRequestUpdate in delaymentRequestUpdates)
                {
                    delaymentRequestUpdate.Show();
                }
            }
        }

        private void FIllRequestUpdateComment(BookingDelaymentRequestService bookingDelaymentRequestService, List<DelaymentRequestUpdate> delaymentRequestUpdates, BookingDelaymentRequest bookingDelaymentRequest)
        {
            List<string> output = bookingDelaymentRequestService.GetTextOutput(bookingDelaymentRequest);
            DelaymentRequestUpdate delaymentRequestUpdate = new DelaymentRequestUpdate();
            delaymentRequestUpdate.messageBlock.Text = "Your booking delayment request has been " + output[0];
            delaymentRequestUpdate.requestsUpdateBlockLabels.Text = "Booking ID: " + "\n\nAccommodation name:" + "\n\nDesired arrival" + "\n\nDesired departure";
            delaymentRequestUpdate.requestsUpdateBlock.Text = output[1] + "\n\n" + output[2] + "\n\n" + output[3] + "\n\n" + output[4];
            delaymentRequestUpdate.WindowStartupLocation = WindowStartupLocation.Manual;
            delaymentRequestUpdate.Left = this.Left + (this.Width - delaymentRequestUpdate.Width) / 2;
            delaymentRequestUpdate.Top = this.Top + (this.Height - delaymentRequestUpdate.Height) / 2;
            delaymentRequestUpdate.SetAttributes(bookingDelaymentRequest);
            delaymentRequestUpdate.Topmost = true;
            delaymentRequestUpdates.Add(delaymentRequestUpdate);
        }
    }
}
