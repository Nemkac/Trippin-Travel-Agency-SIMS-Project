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
using System.Windows;
using InitialProject.Interfaces;
using System.Windows.Controls;
using InitialProject.Service.GuestServices;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GuestOneViewModel : ViewModelBase
    {
        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingDelaymentRequestService bookingDelaymentRequestService;
        bool isHelpOn = false;

        public ViewModelCommand RefreshGrid { get;set; }
        public ViewModelCommand SearchBy { get; set; }
        public ViewModelCommand CheckDates { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help { get; set; }

        public GuestOneViewModel()
        {
            this.bookingService = new BookingService(new BookingRepository());
            this.accommodationService = new AccommodationService(new AccommodationRepository());
            this.accommodationRepository = new AccommodationRepository();
            this.bookingDelaymentRequestService = new(new BookingDelaymentRequestRepository());
            StartingDate = DateTime.Today;
            EndingDate = DateTime.Today;
            AccommodationsGrid = ShowAccommodations();
            SearchBy = new ViewModelCommand(Search);
            CheckDates = new ViewModelCommand(CheckForDates);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            Help = new ViewModelCommand(ShowHelp);
            CheckIfStillSuperGuest();
            CheckIfValidForSuperGuest();
            SendBookingDelaymentUpdate();

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

        private string debuger;
        public string Debuger
        {
            get { return debuger; }
            set
            {
                if (debuger != value)
                {
                    debuger = value;
                    OnPropertyChanged(nameof(Debuger));
                }
            }
        }

        private string inputName;
        public string InputName
        {
            get { return inputName; }
            set
            {
                if (inputName != value)
                {
                    inputName = value;
                    OnPropertyChanged(nameof(InputName));
                }
            }
        }

        private string inputType;
        public string InputType
        {
            get { return inputType; }
            set
            {
                if (inputType != value)
                {
                    inputType = value;
                    OnPropertyChanged(nameof(InputType));
                }
            }
        }

        private string inputCountry;
        public string InputCountry
        {
            get { return inputCountry; }
            set
            {
                if (inputCountry != value)
                {
                    inputCountry = value;
                    OnPropertyChanged(nameof(InputCountry));
                }
            }
        }

        private string inputCity;
        public string InputCity
        {
            get { return inputCity; }
            set
            {
                if (inputCity != value)
                {
                    inputCity = value;
                    OnPropertyChanged(nameof(InputCity));
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

        private string daysToStay;
        public string DaysToStay
        {
            get { return daysToStay; }
            set
            {
                if (daysToStay != value)
                {
                    daysToStay = value;
                    OnPropertyChanged(nameof(DaysToStay));
                }
            }
        }

        private DateTime startingDate;
        public DateTime StartingDate
        {
            get { return startingDate; }
            set
            {
                if (startingDate != value)
                {
                    startingDate = value;
                    OnPropertyChanged(nameof(StartingDate));
                }
            }
        }

        private DateTime endingDate;
        public DateTime EndingDate
        {
            get { return endingDate; }
            set
            {
                if (endingDate != value)
                {
                    endingDate = value;
                    OnPropertyChanged(nameof(EndingDate));
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

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.guestOneInterface.Left + (GuestOneStaticHelper.guestOneInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.guestOneInterface.Top + (GuestOneStaticHelper.guestOneInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.guestOneInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        public void ShowHelp(object sender)
        {
            if (isHelpOn)
            {
                HelpLand = string.Empty;
                HelpExit = string.Empty;
                HelpCheck = string.Empty;
                isHelpOn = false;
            }
            else
            {

                HelpLand = "Go through search parameters with Left and Right Shift.Then press TAB to access the list of accommodations";
                HelpCheck = "Once accommo-dation is selected, press SPACE to iterate through dates and number of guests input";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
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
            //SendBookingDelaymentUpdate(sender, e);
        }

        private void Search(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> byName = this.accommodationRepository.GetAllByName(InputName);
            List<int> byCountry = this.accommodationService.GetAllByCountry(InputCountry);
            List<int> byCity = this.accommodationRepository.GetAllByCity(InputCity);
            List<int> byType = this.accommodationRepository.GetAllByType(InputType);
            List<int> byGuests = new List<int>();
            List<int> byDays = new List<int>();
            if (InputGuests != null && InputGuests != string.Empty)
            {
                byGuests = this.accommodationRepository.GetAllByGuestsNumber(int.Parse(InputGuests));
            } else
            {
                byGuests = null; ;
            }
            if (InputDays != null && InputDays != string.Empty)
            {
                byDays = this.accommodationRepository.GetAllByMininumDays(int.Parse(InputDays));
            } else
            {
                byDays = null;
            }
            
            List<Accommodation> foundResults1 = accommodationService.GetMatching(byName, accommodations);
            List<Accommodation> foundResults2 = accommodationService.GetMatching(byCountry, foundResults1);
            List<Accommodation> foundResults3 = accommodationService.GetMatching(byCity, foundResults2);
            List<Accommodation> foundResults4 = accommodationService.GetMatching(byType, foundResults3);
            List<Accommodation> foundResults5 = accommodationService.GetMatching(byGuests, foundResults4);
            List<Accommodation> foundResults = accommodationService.GetMatching(byDays, foundResults5);

            List<AccommodationDTO> accommodationsDTO = new List<AccommodationDTO>();
            foreach (Accommodation accommodation in foundResults)
            {
                accommodationsDTO.Add(new AccommodationDTO(accommodation, accommodationService.GetAccommodationLocation(accommodation.id)));
            }
            AccommodationsGrid = new ObservableCollection<AccommodationDTO>(accommodationsDTO);
        }

        public void CheckIfStillSuperGuest()
        {
            DataBaseContext context = new DataBaseContext();
            UserService userService = new UserService();
            if (userService.IsSuperGuest() != null)
            {
                if (DateTime.Today.Subtract(userService.IsSuperGuest().titleAcquisition).Days >= 365)
                {
                    foreach (SuperGuest superGuest in context.SuperGuests.ToList())
                    {
                        if (superGuest.guestId == LoggedUser.id && superGuest.ifActive == 1)
                        {
                            superGuest.ifActive = 0;
                            context.Update(superGuest);
                            context.SaveChanges();
                        }
                        else if (superGuest.guestId == LoggedUser.id && superGuest.ifActive == 0)
                        {
                            context.Remove(superGuest);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public void CheckIfValidForSuperGuest()
        {
            DataBaseContext context = new DataBaseContext();
            UserService userService = new UserService();
            string temp = LoggedUser.id.ToString();
            int loggedId = int.Parse(temp);
            if (userService.IsSuperGuest() == null && userService.BookingsInLastYear() >= 10)
            {
                SuperGuest superGuest = new SuperGuest(loggedId, 5, DateTime.Today, 1);
                context.Attach(superGuest);
                context.SaveChanges();
            }
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
            BookAccommodationInterface bookAccommodationInterface = new BookAccommodationInterface();
            bookAccommodationInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            bookAccommodationInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            bookAccommodationInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            bookAccommodationInterface.Show();
            GuestOneStaticHelper.guestOneInterface.Hide();
        }

        private void GetBasicDatesProperties(object sender,out int daysToBook, out List<string> displayableDates)
        {
            AccommodationDTO accommodationDTO = SelectedAccommodation;
            Accommodation accommodation = accommodationService.GetById(accommodationDTO.accommodationId);
            GuestOneStaticHelper.id = accommodation.id;
            List<DateTime> dateLimits = GetDateLimits(sender);
            daysToBook = (int.Parse(DaysToStay));
            List<List<DateTime>> availableDates = accommodationService.GetAvailableDatePeriods(accommodation, daysToBook, dateLimits);
            displayableDates = BookingService.FormDisplayableDates(availableDates);
        }

        private List<DateTime> GetDateLimits(object sender)
        {
            DateTime startingDate = StartingDate;
            DateTime endingDate = EndingDate;
            List<DateTime> dateLimits = new List<DateTime>();
            dateLimits.Add(startingDate);
            dateLimits.Add(endingDate);
            return dateLimits;
        }

        public void SendBookingDelaymentUpdate()
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
            delaymentRequestUpdate.Left = GuestOneStaticHelper.guestOneInterface.Left + (GuestOneStaticHelper.guestOneInterface.Width - delaymentRequestUpdate.Width) / 2;
            delaymentRequestUpdate.Top = GuestOneStaticHelper.guestOneInterface.Top + (GuestOneStaticHelper.guestOneInterface.Height - delaymentRequestUpdate.Height) / 2;
            delaymentRequestUpdate.SetAttributes(bookingDelaymentRequest);
            delaymentRequestUpdate.Topmost = true;
            delaymentRequestUpdates.Add(delaymentRequestUpdate);
        }



    }
}
