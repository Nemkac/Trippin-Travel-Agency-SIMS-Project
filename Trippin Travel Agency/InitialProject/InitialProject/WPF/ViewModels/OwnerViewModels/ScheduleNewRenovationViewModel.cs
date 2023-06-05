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
using System.Windows.Controls;

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

        private string locationText;
        public string LocationText
        {
            get { return locationText; }
            set
            {
                if (locationText != value)
                {
                    locationText = value;
                    OnPropertyChanged(nameof(LocationText));
                }
            }
        }

        private string _accommodationTypeText;
        public string AccommodationTypeText
        {
            get { return _accommodationTypeText; }
            set
            {
                if (_accommodationTypeText != value)
                {
                    _accommodationTypeText = value;
                    OnPropertyChanged(nameof(AccommodationTypeText));
                }
            }
        }

        private string _hintText;

        public string HintText
        {
            get { return _hintText; }
            set
            {
                _hintText = value;
                OnPropertyChanged(nameof(HintText));
            }
        }

        private string _startingDateText;

        public string StartingDateText
        {
            get { return _startingDateText; }
            set
            {
                _startingDateText = value;
                OnPropertyChanged(nameof(StartingDateText));
            }
        }

        private string _endingDateText;

        public string EndingDateText
        {
            get { return _endingDateText; }
            set
            {
                _endingDateText = value;
                OnPropertyChanged(nameof(EndingDateText));
            }
        }

        private string _durationText;

        public string DurationText
        {
            get { return _durationText; }
            set
            {
                _durationText = value;
                OnPropertyChanged(nameof(DurationText));
            }
        }
        private string _descriptionText;

        public string DescriptionText
        {
            get { return _descriptionText; }
            set
            {
                _descriptionText = value;
                OnPropertyChanged(nameof(DescriptionText));
            }
        }

        private string _proceedText;

        public string ProceedText
        {
            get { return _proceedText; }
            set
            {
                _proceedText = value;
                OnPropertyChanged(nameof(ProceedText));
            }
        }

        private string _scheduleRenovationText;

        public string ScheduleRenovationText
        {
            get { return _scheduleRenovationText; }
            set
            {
                _scheduleRenovationText = value;
                OnPropertyChanged(nameof(ScheduleRenovationText));
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
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";

            LocationText = Mediator.GetCurrentIsLanguageChecked() ? "Lokacija:" : "Location:";
            AccommodationTypeText = Mediator.GetCurrentIsLanguageChecked() ? "Tip Smestaja:" : "Accommodation Type:";
            HintText = Mediator.GetCurrentIsLanguageChecked() ? "Izaberite jedan od dostupnih perioda" :
                                                                "Choose one of the available periods";
            StartingDateText = Mediator.GetCurrentIsLanguageChecked() ? "Pocetni datum" : "Starting date";
            EndingDateText = Mediator.GetCurrentIsLanguageChecked() ? "Krajnji datum" : "Ending date";
            DurationText = Mediator.GetCurrentIsLanguageChecked() ? "Trajanje" : "Duration";
            DescriptionText = Mediator.GetCurrentIsLanguageChecked() ? "Opis renoviranja" : "Renovation descriotion";
            ProceedText = Mediator.GetCurrentIsLanguageChecked() ? "Nastavi" : "Proceed";
            ScheduleRenovationText = Mediator.GetCurrentIsLanguageChecked() ? "Zakazi renoviranje" : "Schedule a renovation";
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            LocationText = isChecked ? "Lokacija:" : "Location:";
            AccommodationTypeText = isChecked ? "Tip" : "Type";
            HintText = isChecked ? "Izaberite jedan od dostupnih perioda" :
                                                                "Choose one of the available periods";
            StartingDateText = isChecked ? "Pocetni datum" : "Starting date";
            EndingDateText = isChecked ? "Krajnji datum" : "Ending date";
            DurationText = isChecked ? "Trajanje" : "Duration";
            DescriptionText = isChecked ? "Opis renoviranja" : "Renovation descriotion";
            ProceedText = isChecked ? "Nastavi" : "Proceed";
            ScheduleRenovationText = isChecked ? "Zakazi renoviranje" : "Schedule a renovation";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
        }

        private void ProceedWithRenovation(object obj)
        {
            if(SelectedStartingDate == null || SelectedEndingDate == null || Duration == null)
            {
                MessageBox.Show("Required data is not available!");
            }
            else if(SelectedStartingDate > SelectedEndingDate)
            {
                MessageBox.Show("Starting date cannot be after the ending date of the renovation!");
            }
            else FindAvailableRenovationDates();
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
            if (SelectedDateRange != null)
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
            else MessageBox.Show("You must select one of given date ranges scheduling the renovation!");
        }
    }
}
