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
using InitialProject.WPF.View.GuestOne_Views;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;

namespace InitialProject.WPF.View.GuestOne_Views
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

        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingDelaymentRequestService bookingDelaymentRequestService;

        public GuestOneInterface()
        {
            InitializeComponent();
            this.Loaded += ShowAccommodations;
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            accommodationRepository = new AccommodationRepository();
            this.accommodationService = new AccommodationService(accommodationRepository);
            this.bookingDelaymentRequestService = new(new BookingDelaymentRequestRepository());
            GuestOneStaticHelper.guestOneInterface = this;
        }

        private void ShowAccommodations(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
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
            SendBookingDelaymentUpdate(sender, e);
        }

        private void GetByName(object sender, KeyEventArgs k)
        {
            string input = input_name.Text + k.Key.ToString();
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byName = this.accommodationRepository.GetAllByName(input);
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
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = this.accommodationService.ConvertDtoToInitial(dtos);

            List<int> byCity = accommodationRepository.GetAllByCity(input);
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
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byType = accommodationRepository.GetAllByType(input);
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
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byGuests = accommodationRepository.GetAllByGuestsNumber(int.Parse(input));
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
            List<AccommodationDTO> dtos = new List<AccommodationDTO>();
            dtos = dataGrid.ItemsSource as List<AccommodationDTO>;
            List<Accommodation> accommodations = accommodationService.ConvertDtoToInitial(dtos);

            List<int> byDays = accommodationRepository.GetAllByMininumDays(int.Parse(input));
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
                GuestOneStaticHelper.result = result;
                ShowBookInterface(result);
            }
        }

        private void ShowBookInterface(dynamic result)
        {
            GuestOneStaticHelper.id = selectedAccommodation.id;
            BookAccommodationInterface BookAccommodationInterface = new BookAccommodationInterface();
            BookAccommodationInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            BookAccommodationInterface.Left = this.Left;
            BookAccommodationInterface.Top = this.Top;
            BookAccommodationInterface.Show();
            GuestOneStaticHelper.guestOneInterface = this;
            this.Hide();
        }

        private void GoToGuestsReviews(object sender, RoutedEventArgs e)
        {
            GuestsReviewsInterface GuestsReviewsInterface = new GuestsReviewsInterface();
            GuestsReviewsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            GuestsReviewsInterface.Left = this.Left;
            GuestsReviewsInterface.Top = this.Top;
            GuestsReviewsInterface.Show();
            this.Close();
        }

        private void GetBasicDatesProperties(object sender, RoutedEventArgs e, out int daysToBook, out List<string> displayableDates)
        {
            AccommodationDTO accommodationDTO = (AccommodationDTO)dataGrid.SelectedItem;
            Accommodation accommodation = accommodationService.GetById(accommodationDTO.accommodationId);
            selectedAccommodation = accommodation;
            List<DateTime> dateLimits = GetDateLimits(sender, e);
            daysToBook = (int.Parse(numberOfDays.Text));
            List<List<DateTime>> availableDates = accommodationService.GetAvailableDatePeriods(accommodation, daysToBook, dateLimits);
            displayableDates = BookingService.FormDisplayableDates(availableDates);
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
