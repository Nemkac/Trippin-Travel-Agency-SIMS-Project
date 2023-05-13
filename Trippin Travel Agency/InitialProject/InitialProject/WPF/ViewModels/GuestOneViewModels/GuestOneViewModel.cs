using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
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

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GuestOneViewModel : ViewModelBase
    {
        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        public ViewModelCommand RefreshGrid { get;set; }
        public ViewModelCommand SearchBy { get; set; }

        public GuestOneViewModel()
        {
            this.bookingService = new BookingService(new BookingRepository());
            this.accommodationService = new AccommodationService(new AccommodationRepository());
            this.accommodationRepository = new AccommodationRepository();
            RefreshGrid = new ViewModelCommand(Refresh);
            AccommodationsGrid = ShowAccommodations();
            SearchBy = new ViewModelCommand(Search);
        }

        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation
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

        public void Refresh(object sender)
        {

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

       
    }
}
