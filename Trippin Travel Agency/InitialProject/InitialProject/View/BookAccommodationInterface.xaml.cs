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
using InitialProject.Service;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Configuration;

namespace InitialProject.View
{
    public partial class BookAccommodationInterface
    {

        public int accommodationId;
        public int userId;
        public BookAccommodationInterface()
        {
            InitializeComponent();
        }

        public void ShowsBookings(dynamic result)
        {
            dataGrid.ItemsSource = result;
        }
        public void SetAattributes(int accommodationId, int userId)
        {
            this.accommodationId = accommodationId;
            this.userId = userId;
        }
        private void BookAccommodation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            string selectedDate = dataGrid.SelectedItem.ToString();
            selectedDate = selectedDate.Substring(10, selectedDate.Length - 12);
            List<string> dates = selectedDate.Split("-").ToList();
            string arrival = dates[0].Substring(0, dates[0].Length - 2);
            string departure = dates[1].Substring(2, dates[1].Length - 2);
            string guestsNumber = numberOfGuests.Text;
            if (int.Parse(guestsNumber) > accommodationService.GetById(accommodationId).guestLimit)
            {
                warningText.Text = accommodationService.GetById(accommodationId).name + " cannot take more then " + accommodationService.GetById(accommodationId).guestLimit.ToString() + " guests.";
            } else
            {
                Booking booking = new Booking(accommodationId, arrival, departure, (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days, userId);
                Services.BookingService.Save(booking);
                string report = "";
                report = "Accommodations name :" + accommodationService.GetById(accommodationId).name + "\nAccommodation id : " + accommodationId + "\nGuests id : " + userId;
                report += "\nArrival : " + arrival + "\nDeparture : " + departure + "\nDays : " + (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days;
                report += "\nNo.of guests : " + guestsNumber + "\nAccommodation successfully booked !";
                successfullyBooked.Text = report;
            }

        }


    }
}