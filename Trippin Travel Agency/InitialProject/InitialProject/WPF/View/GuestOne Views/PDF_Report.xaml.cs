using InitialProject.Context;
using InitialProject.Model.TransferModels;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
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
using InitialProject.Service.GuestServices;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for PDF_Report.xaml
    /// </summary>
    public partial class PDF_Report : UserControl
    {
        private AccommodationService accommodationService { get; set; }
        public PDF_Report()
        {
            InitializeComponent();
            accommodationService = new AccommodationService(new AccommodationRepository());
            if (GuestOneStaticHelper.ifChoseCanceled)
            {
                CancelBooked.Text = new string("Canceled bookings");
                var bookingsToGrid = from canceledBooking in GuestOneStaticHelper.canceledBookingsForReport
                                     select new
                                     {
                                         Booking_Id = canceledBooking.bookingId,
                                         Accomodation = "Vila Kragujevac",
                                         Arrival = canceledBooking.plannedArrival.ToString().Substring(0, canceledBooking.plannedArrival.ToString().Length-11),
                                         Departure = canceledBooking.plannedArrival.AddDays(10).ToString().Substring(0, canceledBooking.plannedArrival.AddDays(10).ToString().Length - 11)
                                     };
                Grid.ItemsSource = bookingsToGrid;
            }
            else
            {
                CancelBooked.Text = new string("All bookings");
                var bookingsToGrid = from booking in GuestOneStaticHelper.bookingsForReport
                                   select new
                                   {
                                       Booking_Id = booking.Id,
                                       Accomodation = accommodationService.GetById(booking.Id).name,
                                       Arrival = booking.arrival,
                                       Departure = booking.departure

                                   };
                Grid.ItemsSource = bookingsToGrid;
            }
            Name.Text = LoggedUser.firstName + " " + LoggedUser.lastName;
            Mail.Text = LoggedUser.email;
            Dates.Text = "Bookings shown are in following period:\n" + GuestOneStaticHelper.chosenStartingDate.ToString().Substring(0,GuestOneStaticHelper.chosenStartingDate.ToString().Length-11) + 
                "  -  " + GuestOneStaticHelper.chosenEndingDate.ToString().Substring(0, GuestOneStaticHelper.chosenEndingDate.ToString().Length - 11);
            
        }

        


    }
}
