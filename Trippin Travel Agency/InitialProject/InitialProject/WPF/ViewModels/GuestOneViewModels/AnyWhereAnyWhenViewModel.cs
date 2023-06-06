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
using ToastNotifications.Messages.Warning;
using Xceed.Wpf.Toolkit.Primitives;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class AnyWhereAnyWhenViewModel : ViewModelBase
    {
        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingDelaymentRequestService bookingDelaymentRequestService;
        bool isHelpOn = false;
        public ViewModelCommand CheckDates { get; set; }
        public ViewModelCommand Search { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand GoBack { get; set; }

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
            Help = new ViewModelCommand(ShowHelp);
            GoBack = new ViewModelCommand(GoToPreviousWindow);

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

        private string helpLand;
        public string HelpLand
        {
            get { return helpLand; }
            set
            {
                if (helpLand != value)
                {
                    helpLand = value;
                    OnPropertyChanged(nameof(HelpLand));
                }
            }
        }

        private string helpCheck;
        public string HelpCheck
        {
            get { return helpCheck; }
            set
            {
                if (helpCheck != value)
                {
                    helpCheck = value;
                    OnPropertyChanged(nameof(HelpCheck));
                }
            }
        }

        private string helpExit;
        public string HelpExit
        {
            get { return helpExit; }
            set
            {
                if (helpExit != value)
                {
                    helpExit = value;
                    OnPropertyChanged(nameof(helpExit));
                }
            }
        }

        private string warningMessageSearch;
        public string WarningMessageSearch
        {
            get { return warningMessageSearch; }
            set
            {
                if (warningMessageSearch != value)
                {
                    warningMessageSearch = value;
                    OnPropertyChanged(nameof(WarningMessageSearch));
                }
            }
        }

        private string warningMessageCheck;
        public string WarningMessageCheck
        {
            get { return warningMessageCheck; }
            set
            {
                if (warningMessageCheck != value)
                {
                    warningMessageCheck = value;
                    OnPropertyChanged(nameof(WarningMessageCheck));
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
            GuestOneStaticHelper.InterfaceToGoBack = GuestOneStaticHelper.anyWhereAnyWhenInterface;
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.anyWhereAnyWhenInterface.Left + (GuestOneStaticHelper.anyWhereAnyWhenInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.anyWhereAnyWhenInterface.Top + (GuestOneStaticHelper.anyWhereAnyWhenInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.anyWhereAnyWhenInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        public void GoToPreviousWindow(object sedner)
        {
            GuestOneStaticHelper.anyWhereAnyWhenInterface.Hide();
            GuestOneStaticHelper.InterfaceToGoBack.Top = GuestOneStaticHelper.anyWhereAnyWhenInterface.Top;
            GuestOneStaticHelper.InterfaceToGoBack.Left = GuestOneStaticHelper.anyWhereAnyWhenInterface.Left;
            GuestOneStaticHelper.InterfaceToGoBack.Show();
        }

        public void ShowHelp(object sender)
        {
            if (isHelpOn)
            {
                HelpLand = string.Empty;
                HelpCheck = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                WarningMessageCheck = string.Empty;
                WarningMessageSearch = string.Empty;
                HelpLand = "Go through search parameters with Left and Right Shift.\nIf you don't want to state dates, leave it as it is.\nPress S to search accommodations.";
                HelpCheck = "Once you selected accommodation, press ENTER to book it.";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
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
            if (InputGuests != null && InputDays != null && int.TryParse(InputGuests,out int x) && int.TryParse(InputDays, out int y) && InputStartingDate < inputEndingDate)
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
                }
                else
                {
                    dateLimits.Add(DateTime.Today);
                    dateLimits.Add(DateTime.Today.AddYears(1));
                }

                List<Accommodation> foundAccommodations = new List<Accommodation>();
                foreach (Accommodation accommodation in foundResults)
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
            } else
            {
                WarningMessageSearch = "Incorrect input for search parameters";
            }
        }

        private void CheckForDates(object sender)
        {
            if (SelectedAccommodation != null)
            {
                if (InputDays != null && InputDays != string.Empty && InputGuests != null && InputGuests != string.Empty)
                {
                    int daysToBook;
                    List<string> displayableDates;
                    GetBasicDatesProperties(sender, out daysToBook, out displayableDates);
                    WarningMessageCheck = string.Empty;
                    dynamic result = displayableDates.Select(s => new { value = s }).ToList();
                    GuestOneStaticHelper.result = result;
                    ShowBookInterface();
                }
                else
                {
                    WarningMessageCheck = "Please enter parameters";
                }
            }
            else
            {
                WarningMessageCheck = "You need to select accommodation first";
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
