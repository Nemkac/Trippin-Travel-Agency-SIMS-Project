﻿using System;
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
            pastBookingsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            pastBookingsInterface.Left = this.Left;
            pastBookingsInterface.Top = this.Top;
            this.Close();
            this.Close();
            pastBookingsInterface.Show();
        }

        private void DeleteBooking(object sender, RoutedEventArgs e)
        {
            BookingService bookingService = new BookingService();
            UserService userService = new UserService();
            BookingCancelationMessageService BookingCancelationMessageService = new BookingCancelationMessageService();
            Booking booking = (Booking)futureBookingsGrid.SelectedItem;
            bookingService.Delete(booking);
            futureBookingsGrid.ItemsSource = userService.GetGuestsFutureBookings(LoggedUser.id);
            string message = "Booking with ID: " + booking.Id + " has been canceled.";
            BookingCancelationMessage bookingCancelationMessage = new BookingCancelationMessage(message, booking.Id);
            BookingCancelationMessageService.Save(bookingCancelationMessage);
            DataBaseContext canceledContext = new DataBaseContext();
            CanceledBooking canceledBooking = new CanceledBooking(booking.Id);
            canceledContext.CanceledBookings.Add(canceledBooking);
            canceledContext.SaveChanges();
        }

        private void GoToBookingDelayment(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            if ((DateTime.Parse((((Booking)futureBookingsGrid.SelectedItem).arrival)).Subtract(DateTime.Today)).Days >= (accommodationService.GetById(((Booking)futureBookingsGrid.SelectedItem).accommodationId)).bookingCancelPeriodDays)
            {
                SendBookingDelaymentInterface sendBookingDelaymentInterface = new SendBookingDelaymentInterface();
                sendBookingDelaymentInterface.SetAttribures((Booking)futureBookingsGrid.SelectedItem);
                sendBookingDelaymentInterface.WindowStartupLocation = WindowStartupLocation.Manual;
                sendBookingDelaymentInterface.Left = this.Left;
                sendBookingDelaymentInterface.Top = this.Top;
                this.Close();
                sendBookingDelaymentInterface.Show();
            } else
            {
                warningBlock.Text = "ne moze";
            }
        }

        private void ShowDetailed(object sender, RoutedEventArgs e)
        {

        }
    }
}
