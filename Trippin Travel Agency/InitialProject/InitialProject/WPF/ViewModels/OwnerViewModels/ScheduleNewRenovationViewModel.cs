using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ScheduleNewRenovationViewModel : ViewModelBase
    {
        public ObservableCollection<string> AvailableDates
        {
            get { return availableDates; }
            set
            {
                if (availableDates != value)
                {
                    availableDates = value;
                    OnPropertyChanged(nameof(AvailableDates));
                }
            }
        }
        private ObservableCollection<string> availableDates = new ObservableCollection<string>();

        private string selectedDateRange;
        public string SelectedDateRange
        {
            get { return selectedDateRange; }
            set
            {
                if (selectedDateRange != value)
                {
                    selectedDateRange = value;
                    OnPropertyChanged(nameof(SelectedDateRange));
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private BookingService bookingService = new(new BookingRepository());
        public DateTime SelectedStartingDate { get; set; }
        public DateTime SelectedEndingDate { get; set; }

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        private string duration;
        public string Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        private string feedBack;
        public string FeedBack
        {
            get { return feedBack; }
            set
            {
                if (feedBack != value)
                {
                    feedBack = value;
                    OnPropertyChanged(nameof(FeedBack));
                }
            }
        }

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }

        private string _contentHintColor;
        public string ContentHintColor
        {
            get { return _contentHintColor; }
            set
            {
                _contentHintColor = value;
                OnPropertyChanged(nameof(ContentHintColor));
            }
        }

        public ViewModelCommand ShowAvailableDateRanges { get; set; }
        public ViewModelCommand ScheduleRenovationCommand { get; set; }

        public ScheduleNewRenovationViewModel() 
        { 
            AccommodationName = "Renovation of " + SelectedAccommodations.selectedAccommodationForRenovation.accommodationName;
            Location = SelectedAccommodations.selectedAccommodationForRenovation.location;
            Type = SelectedAccommodations.selectedAccommodationForRenovation.type.ToString();
            ShowAvailableDateRanges = new ViewModelCommand(ProceedWithRenovation);
            ScheduleRenovationCommand = new ViewModelCommand(ScheduleRenovation);

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
        }

        private void ProceedWithRenovation(object obj)
        {
            FindAvailableRenovationDates();
        }

        private void FindAvailableRenovationDates()
        {
            DateTime startingDate = SelectedStartingDate;
            DateTime endingDate = SelectedEndingDate;

            int duration;
            if (!int.TryParse(Duration, out duration))
            {
                MessageBox.Show("Please enter a valid duration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime lastPossibleRenovationDate = endingDate;

            List<Booking> bookings = this.bookingService.GetAllBookings();

            ObservableCollection<string> availableDates = new ObservableCollection<string>();
            DateTime currentDate = startingDate;
            while (currentDate <= lastPossibleRenovationDate)
            {

                bool isAvailable = true;
                DateTime endDate = currentDate.AddDays(duration);
                AccommodationStatisticsDTO selectedAccommodation = SelectedAccommodations.selectedAccommodationForRenovation;
                foreach (Booking booking in bookings)
                {
                    if (booking.accommodationId == selectedAccommodation.accommodationId)
                    {
                        DateTime tmpArrival = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                        DateTime tmpDeparture = DateTime.ParseExact(booking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);

                        if (tmpArrival <= currentDate && tmpDeparture >= currentDate ||
                                    endDate >= tmpArrival && endDate <= tmpDeparture ||
                                    currentDate <= tmpArrival && endDate >= tmpDeparture)
                        {
                            isAvailable = false;
                            break;
                        }
                    }
                }

                if (isAvailable)
                {
                    availableDates.Add(currentDate.ToString("M/d/yyyy") + " - " + endDate.ToString("M/d/yyyy"));
                }
                currentDate = currentDate.AddDays(1);
            }

            AvailableDates = availableDates;
        }

        private void ScheduleRenovation(object obj)
        {
            string selectedItem = SelectedDateRange;
            string[] parts = selectedItem.Split('-');
            string startDateString = parts[0].Trim();
            string endDateString = parts[1].Trim();

            string description = Description;
            int duration;
            if (!int.TryParse(Duration, out duration))
            {
                MessageBox.Show("Please enter a valid duration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AccommodationRenovation accommodationRenovation = new AccommodationRenovation(SelectedAccommodations.selectedAccommodationForRenovation.accommodationId,
                                                                  SelectedAccommodations.selectedAccommodationForRenovation.accommodationName, SelectedAccommodations.selectedAccommodationForRenovation.type.ToString(),
                                                                  startDateString, endDateString, duration, description);

            DataBaseContext renovationContext = new DataBaseContext();
            renovationContext.AccommodationRenovations.Add(accommodationRenovation);
            renovationContext.SaveChanges();

            AvailableDates.Clear();
            Description = null;
            FeedBack = "Renovation successfully scheduled!";
        }
    }
}
