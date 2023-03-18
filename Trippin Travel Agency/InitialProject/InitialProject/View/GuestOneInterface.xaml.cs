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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestOneInterface.xaml
    /// </summary>
    public partial class GuestOneInterface : Window
    {
        public AccommodationLocation location { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int guestsNumber { get; set; }
        public int daysNumber { get; set; }

        public Accommodation selectedAccommodation = new Accommodation();
        public DateTime selectedDate = new DateTime();


        public GuestOneInterface()
        {
            InitializeComponent();
            this.Loaded += showAccommodations;
        }

        private void showAccommodations(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();

            this.dataGrid.ItemsSource = dataList;
        }

        private void GetByName(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byName = accommodationService.GetByName(input_name.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byName.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byName[i]));
            }
            this.dataGrid.ItemsSource = foundResults;

        }

        private void GetByCountry(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            AccommodationLocationService locationService = new AccommodationLocationService();
            List<int> byCountry = accommodationService.GetByCountry(input_country.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byCountry.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byCountry[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByCity(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            AccommodationLocationService locationService = new AccommodationLocationService();
            List<int> byCity = accommodationService.GetByCity(input_city.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byCity.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byCity[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByType(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byType = accommodationService.GetByType(input_type.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byType.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byType[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByGuests(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byGuests = accommodationService.GetByGuestsNumber(int.Parse(input_guests.Text));
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byGuests.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byGuests[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByDays(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byDays = accommodationService.GetByMininumDays(int.Parse(input_days.Text));
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byDays.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byDays[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void CheckForDates(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            Accommodation accommodation = (Accommodation)dataGrid.SelectedItem;
            selectedAccommodation = accommodation;
            List<DateTime> dateLimits = GetDateLimits(sender, e);
            int daysToBook = (int.Parse(input_days.Text));
            List<List<DateTime>> availableDates = accommodationService.GetAvailableDates(accommodation, daysToBook, dateLimits);

            // razlaganje u normalne datume
            List<string> displayableDates = new List<string>();
            foreach (List<DateTime> checkInCheckOut in availableDates)
            {
                string date = checkInCheckOut[0].ToString().Substring(0, checkInCheckOut[0].ToString().Length - 11) + "  -  " + checkInCheckOut[1].ToString().Substring(0, checkInCheckOut[0].ToString().Length - 11);
                displayableDates.Add(date);
            }
            var result = displayableDates.Select(s => new { value = s }).ToList();
            dataGrid2.ItemsSource = result;

        }

        private List<DateTime> GetDateLimits(object sender, RoutedEventArgs e)
        {
            DateTime startingDate = input_starting_date.SelectedDate.Value;
            DateTime endingDate = input_ending_date.SelectedDate.Value;
            List<DateTime> dateLimits = new List<DateTime>();
            dateLimits.Add(startingDate);
            dateLimits.Add(endingDate);
            return dateLimits;
        }

        private void BookAccommodation(object sender, RoutedEventArgs e)
        {
            string selectedDate = dataGrid2.SelectedItem.ToString();
            selectedDate = selectedDate.Substring(10, selectedDate.Length - 12);
            List<string> dates = selectedDate.Split("-").ToList();
            List<string> checkInCheckOut = new List<string>();
            string arrival = dates[0].Substring(0, dates[0].Length - 2);
            string departure = dates[1].Substring(2, dates[0].Length - 2);

            int accommodationId = selectedAccommodation.id;
            string accommodationName = selectedAccommodation.name;
            int userId = 4;
            int guestsNumber = int.Parse(input_guests.Text);
            int stayingPeriod = (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days;
            string report = "";
            report = "Accommodations name :" + accommodationName + "\nAccommodation id : " + accommodationId + "\nGuests id : " + userId + "\nArrival : " + arrival;
            report += "\nDeparture : " + departure + "\nDays : " + stayingPeriod + "\nNo.of guests : " + guestsNumber + "\nAccommodation successfully booked !";
            successfullyBooked.Text = report;
            Booking booking = new Booking(accommodationId, arrival, departure, stayingPeriod, userId);
            Services.BookingService.Save(booking);
        }
    }
}
