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
using System.Diagnostics;
using System.Xml.Linq;

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
                accommodationsDTO.Add(new AccommodationDTO(accommodation,accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            input_name.Clear();
            input_country.Clear();
            input_city.Clear();
            input_type.Clear();
            input_guests.Clear();
            input_days.Clear();

            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void GetByName(object sender, KeyEventArgs k)
        {
            string input = input_name.Text + k.Key.ToString();
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byName = accommodationService.GetAllByName(input);
            List<Accommodation> foundResults = accommodationService.GetMatching(byName, accommodations);
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void GetByCountry(object sender, KeyEventArgs k)
        {
            string input = input_country.Text + k.Key.ToString();
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byCountry = accommodationService.GetAllByCountry(input);
            List<Accommodation> foundResults = accommodationService.GetMatching(byCountry, accommodations);
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void GetByCity(object sender, KeyEventArgs k)
        {
            string input = input_city.Text + k.Key.ToString();
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byCity = accommodationService.GetAllByCity(input);
            List<Accommodation> foundResults = accommodationService.GetMatching(byCity, accommodations);
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void GetByType(object sender, KeyEventArgs k)
        {
            string input = input_type.Text + k.Key.ToString();
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byType = accommodationService.GetAllByType(input);
            List<Accommodation> foundResults = accommodationService.GetMatching(byType, accommodations);
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void GetByGuests(object sender, KeyEventArgs k)
        {
            string input = input_guests.Text + k.Key.ToString()[1];
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byGuests = accommodationService.GetAllByGuestsNumber(int.Parse(input));
            List<Accommodation> foundResults = accommodationService.GetMatching(byGuests, accommodations);
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            this.dataGrid.ItemsSource = accommodationsDTO;
        }

        private void GetByDays(object sender, KeyEventArgs k)
        {
            string input = input_days.Text + k.Key.ToString()[1];
            AccommodationService accommodationService = new AccommodationService();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byDays = accommodationService.GetAllByMininumDays(int.Parse(input));
            List<Accommodation> foundResults = accommodationService.GetMatching(byDays, accommodations);
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
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
                BookAccommodationInterface.WindowStartupLocation = WindowStartupLocation.Manual;
                BookAccommodationInterface.Left = this.Left;
                BookAccommodationInterface.Top = this.Top;
                BookAccommodationInterface.Show();
                this.Close();
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
            List<List<DateTime>> availableDates = accommodationService.GetAvailableDatePeriods(accommodation, daysToBook, dateLimits);
            displayableDates = Service.BookingService.FormDisplayableDates(availableDates);
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

        private void GoToBookings(object sender, RoutedEventArgs e)
        {
            FutureBookingsInterface futureBookingsInterface = new FutureBookingsInterface();
            futureBookingsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            futureBookingsInterface.Left = this.Left;
            futureBookingsInterface.Top = this.Top;
            this.Close();
            futureBookingsInterface.Show();
        }

        private void GoToBookingDelaymentRequests(object sender, RoutedEventArgs e)
        {
            GuestsBookingDelaymentRequestsInterface guestsBookingDelaymentRequestsInterface = new GuestsBookingDelaymentRequestsInterface();
            guestsBookingDelaymentRequestsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            guestsBookingDelaymentRequestsInterface.Left = this.Left;
            guestsBookingDelaymentRequestsInterface.Top = this.Top;
            this.Close();
            guestsBookingDelaymentRequestsInterface.Show();
        }
    }
}
