using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Primitives;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class AnyWhereAnyWhenViewModel : ViewModelBase
    {
        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingDelaymentRequestService bookingDelaymentRequestService;
        public ViewModelCommand CheckDates { get; set; }
        public ViewModelCommand Search { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }

        public AnyWhereAnyWhenViewModel()
        {
            this.bookingService = new BookingService(new BookingRepository());
            this.accommodationService = new AccommodationService(new AccommodationRepository());
            this.accommodationRepository = new AccommodationRepository();
            this.bookingDelaymentRequestService = new(new BookingDelaymentRequestRepository());

            AccommodationsGrid = ShowAccommodations();
            Introduction = GenerateIntroductionText();
            Search = new ViewModelCommand(SearchAccommodations);
            CheckDates = new ViewModelCommand(CheckForDates);
            OpenNavigator = new ViewModelCommand(ShowNavigator);

            InputStartingDate = DateTime.Today;
            InputEndingDate = DateTime.Today;

        }

        private string introduction;
        public string Introduction
        {
            get { return introduction; }
            set
            {
                if (introduction != value)
                {
                    introduction = value;
                    OnPropertyChanged(nameof(Introduction));
                }
            }
        }

        private string inputGuests;
        public string InputGuests
        {
            get { return inputGuests; }
            set
            {
                if (inputGuests != value)
                {
                    inputGuests = value;
                    OnPropertyChanged(nameof(InputGuests));
                }
            }
        }

        private string inputDays;
        public string InputDays
        {
            get { return inputDays; }
            set
            {
                if (inputDays != value)
                {
                    inputDays = value;
                    OnPropertyChanged(nameof(InputDays));
                }
            }
        }

        private DateTime inputStartingDate;
        public DateTime InputStartingDate
        {
            get { return inputStartingDate; }
            set
            {
                if (inputStartingDate != value)
                {
                    inputStartingDate = value;
                    OnPropertyChanged(nameof(InputStartingDate));
                }
            }
        }

        private DateTime inputEndingDate;
        public DateTime InputEndingDate
        {
            get { return inputEndingDate; }
            set
            {
                if (inputEndingDate != value)
                {
                    inputEndingDate = value;
                    OnPropertyChanged(nameof(InputEndingDate));
                }
            }
        }

        private ObservableCollection<AccommodationDTO> accommodationsGrid;
        public ObservableCollection<AccommodationDTO> AccommodationsGrid
        {
            get { return accommodationsGrid; }
            set
            {
                if (accommodationsGrid != value)
                {
                    accommodationsGrid = value;
                    OnPropertyChanged(nameof(AccommodationsGrid));
                }
            }
        }

        private AccommodationDTO selectedAccommodation;
        public AccommodationDTO SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                if (selectedAccommodation != value)
                {
                    selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                }
            }
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.anyWhereAnyWhenInterface.Left + (GuestOneStaticHelper.anyWhereAnyWhenInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.anyWhereAnyWhenInterface.Top + (GuestOneStaticHelper.anyWhereAnyWhenInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.anyWhereAnyWhenInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private string GenerateIntroductionText()
        {
            return "AnyWhere - AnyWhen lets you search accommodations based only on number of days you want to " +
                "spend there and a number of people you plan to go with.\nYou can but don't have to state date range";
        }

        private ObservableCollection<AccommodationDTO> ShowAccommodations()
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in accommodations)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            return new ObservableCollection<AccommodationDTO>(accommodationsDTO);
        }

        private void SearchAccommodations(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<DateTime> dateLimits = new List<DateTime>();
            List<int> byGuests = this.accommodationRepository.GetAllByGuestsNumber(int.Parse(InputGuests));
            List<int> byDays = this.accommodationRepository.GetAllByMininumDays(int.Parse(InputDays));

            List<Accommodation> foundResults1 = accommodationService.GetMatching(byGuests, accommodations);
            List<Accommodation> foundResults = accommodationService.GetMatching(byDays, foundResults1);

            if (InputStartingDate != DateTime.Today && InputEndingDate != DateTime.Today)
            {
                dateLimits.Add(InputStartingDate);
                dateLimits.Add(InputEndingDate);
            } else
            {
                dateLimits.Add(DateTime.Today);
                dateLimits.Add(DateTime.Today.AddYears(1));
            }

            List<Accommodation> foundAccommodations = new List<Accommodation>();
            foreach(Accommodation accommodation in foundResults)
            {
                if (accommodationService.GetDatesForAnyWhereAnyWhen(accommodation, int.Parse(InputDays), dateLimits) != null)
                {
                    foundAccommodations.Add(accommodation);
                }
            }

            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundAccommodations)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            AccommodationsGrid = new ObservableCollection<AccommodationDTO>(accommodationsDTO);
        }

        private void CheckForDates(object sender)
        {
            int daysToBook;
            List<string> displayableDates;
            GetBasicDatesProperties(sender, out daysToBook, out displayableDates);

            dynamic result = displayableDates.Select(s => new { value = s }).ToList();
            if (daysToBook < selectedAccommodation.minDaysBooked)
            {
                // ne moze da se bukira
            }
            else
            {
                GuestOneStaticHelper.result = result;
                ShowBookInterface();
            }
        }

        private void ShowBookInterface()
        {
            GuestOneStaticHelper.id = SelectedAccommodation.accommodationId;
            GuestOneStaticHelper.numberOfGuests = InputGuests;
            BookAccommodationInterface bookAccommodationInterface = new BookAccommodationInterface();
            bookAccommodationInterface.Left = GuestOneStaticHelper.anyWhereAnyWhenInterface.Left;
            bookAccommodationInterface.Top = GuestOneStaticHelper.anyWhereAnyWhenInterface.Top;
            bookAccommodationInterface.Show();
            GuestOneStaticHelper.anyWhereAnyWhenInterface.Hide();
        }

        private void GetBasicDatesProperties(object sender, out int daysToBook, out List<string> displayableDates)
        {
            AccommodationDTO accommodationDTO = SelectedAccommodation;
            Accommodation accommodation = accommodationService.GetById(accommodationDTO.accommodationId);
            GuestOneStaticHelper.id = accommodation.id;
            List<DateTime> dateLimits = GetDateLimits(sender);
            daysToBook = (int.Parse(InputDays));
            List<List<DateTime>> availableDates = accommodationService.GetAvailableDatePeriods(accommodation, daysToBook, dateLimits);
            displayableDates = BookingService.FormDisplayableDates(availableDates);
        }

        private List<DateTime> GetDateLimits(object sender)
        {
            DateTime startingDate = InputStartingDate;
            DateTime endingDate = InputEndingDate;
            List<DateTime> dateLimits = new List<DateTime>();
            dateLimits.Add(startingDate);
            dateLimits.Add(endingDate);
            return dateLimits;
        }


    }
}
