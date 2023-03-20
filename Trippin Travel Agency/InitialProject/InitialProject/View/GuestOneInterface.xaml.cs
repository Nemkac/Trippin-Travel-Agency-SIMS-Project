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
using InitialProject.DTO;

namespace InitialProject.View
{

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
            this.Loaded += ShowAccommodations;
        }

        private void ShowAccommodations(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in accommodations)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation,accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
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
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;

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
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
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
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
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
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
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
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
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
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetLocationList(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void CheckForDates(object sender, RoutedEventArgs e)
        {
            int daysToBook;
            List<string> displayableDates;
            GetBasicDatesProperties(sender, e, out daysToBook, out displayableDates);

            dynamic result = displayableDates.Select(s => new { value = s }).ToList();
            if (daysToBook < selectedAccommodation.minDaysBooked)
            {
                warningText.Text = selectedAccommodation.name + " cannot be booked for under " + selectedAccommodation.minDaysBooked.ToString() + " days.";
            }
            else
            {
                BookAccommodationInterface BookAccommodationInterface = new BookAccommodationInterface();
                BookAccommodationInterface.SetAttributes(selectedAccommodation.id,LoggedUser.id);
                BookAccommodationInterface.ShowBookings(result);
                BookAccommodationInterface.Show();
            }
        }

        private void GetBasicDatesProperties(object sender, RoutedEventArgs e, out int daysToBook, out List<string> displayableDates)
        {
            AccommodationService accommodationService = new AccommodationService();
            AccommodationDTO accommodationDTO = (AccommodationDTO)dataGrid.SelectedItem;
            Accommodation accommodation = accommodationService.GetById(accommodationDTO.id);
            selectedAccommodation = accommodation;
            List<DateTime> dateLimits = GetDateLimits(sender, e);
            daysToBook = (int.Parse(numberOfDays.Text));
            List<List<DateTime>> availableDates = accommodationService.GetAvailableDates(accommodation, daysToBook, dateLimits);
            displayableDates = Service.BookingService.GetDisplayableDates(availableDates);
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
    }
}
