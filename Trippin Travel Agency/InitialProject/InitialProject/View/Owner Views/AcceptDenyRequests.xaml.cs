﻿using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using InitialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml.Schema;

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AcceptDenyRequests.xaml
    /// </summary>
    public partial class AcceptDenyRequests : UserControl
    {

        public AcceptDenyRequests()
        {
            InitializeComponent();
            this.Loaded += DisplayData;
        }

        public void DisplayData(object sender, RoutedEventArgs e)
        {
            DataBaseContext acceptDenyContext = new DataBaseContext();
            List<RequestDTO> requests = acceptDenyContext.SelectedRequestTransfers.ToList();
            FillBookingDataFields(requests);
        }

        private void FillBookingDataFields(List<RequestDTO> requests)
        {
            this.OldArrivalTextBlock.Text = requests.First().oldArrival.ToString();
            this.OldDepartureTextBlock.Text = requests.First().oldDeparture.ToString();
            this.NewArrivalTextBlock.Text = requests.First().newArrival.ToString();
            NewDepartureTextBlock.Text = requests.First().newDeparture.ToString();
            BookingService bookingService = new BookingService();
            AccommodationService accommodationService = new AccommodationService();
            GuestTextBlock.Text = bookingService.GetGuestName(requests.First().bookingId);
            Booking booking = bookingService.GetById(requests.First().bookingId);
            AccommodationNameTextBlock.Text = (accommodationService.GetById(booking.accommodationId)).name;
            AccommodationTypeTextBlock.Text = (accommodationService.GetById(booking.accommodationId)).type.ToString();
            BookingIdTextBlock.Text = booking.Id.ToString();
            List<string> location = accommodationService.GetAccommodationLocation(booking.accommodationId);
            LocationTextBlock.Text = location[0] + ", " + location[1];
        }

        private void AcceptRequest(object sender, RoutedEventArgs e)
        {
            DataBaseContext bookingContext = new DataBaseContext();
            DataBaseContext acceptContext = new DataBaseContext();

            List<Booking> bookings = bookingContext.Bookings.ToList();
            List<RequestDTO> selectedRequest = acceptContext.SelectedRequestTransfers.ToList();

            foreach (Booking booking in bookings.ToList()) { 
                if(booking.Id == selectedRequest.First().bookingId)
                {
                    booking.arrival = selectedRequest.First().newArrival.ToString("M/dd/yyyy");
                    booking.departure = selectedRequest.First().newDeparture.ToString("M/dd/yyyy");
                    bookingContext.SaveChanges();
                }
            }

            DataBaseContext requestContext = new DataBaseContext();
            List<BookingDelaymentRequest> bookingDelaymentRequests = requestContext.BookingDelaymentRequests.ToList();

            foreach(BookingDelaymentRequest request in bookingDelaymentRequests.ToList()) {
                if(request.bookingId == selectedRequest.First().bookingId)
                {
                    requestContext.BookingDelaymentRequests.Remove(request);
                    requestContext.SaveChanges();
                }
            }

            AcceptFeedBack.Text = "Request accepted!";
            acceptContext.SelectedRequestTransfers.Remove(acceptContext.SelectedRequestTransfers.First());
            acceptContext.SaveChanges();
        }

        private void DenyRequest(object sender, RoutedEventArgs e)
        {
            DataBaseContext acceptContext = new DataBaseContext();
            List<RequestDTO> selectedRequest = acceptContext.SelectedRequestTransfers.ToList();
            DataBaseContext requestContext = new DataBaseContext();
            List<BookingDelaymentRequest> bookingDelaymentRequests = requestContext.BookingDelaymentRequests.ToList();

            foreach (BookingDelaymentRequest request in bookingDelaymentRequests.ToList())
            {
                if (request.bookingId == selectedRequest.First().bookingId)
                {
                    requestContext.BookingDelaymentRequests.Remove(request);
                    requestContext.SaveChanges();
                }
            }

            AcceptFeedBack.Text = "Request denied!";
            acceptContext.SelectedRequestTransfers.Remove(acceptContext.SelectedRequestTransfers.First());
            acceptContext.SaveChanges();
        }
    }
}
