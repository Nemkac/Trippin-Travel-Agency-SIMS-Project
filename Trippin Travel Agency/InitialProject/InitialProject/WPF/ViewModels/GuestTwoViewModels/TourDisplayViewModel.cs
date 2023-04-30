using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{

    public class TourDisplayViewModel : ViewModelBase
    {
        public static bool CanExecute { get; set; }

        public ObservableCollection<language> languages { get; set; } = new ObservableCollection<language>();
        public ObservableCollection<TourDTO> tourDTOs { get; set; } = new ObservableCollection<TourDTO>();  

        private readonly TourService tourService = new(new TourRepository());

        public static int DetailedId = -1;
        
        private string cityName;
        public string CityName
        {
            get { return cityName; }
            set
            {
                if (cityName != value)
                {
                    cityName = value;
                    OnPropertyChanged(nameof(CityName));
                }
            }
        }

        private string countryName;
        public string CountryName
        {
            get { return countryName; }
            set
            {
                if (countryName != value)
                {
                    countryName = value;
                    OnPropertyChanged(nameof(CountryName));
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
        
        private string touristLimit;
        public string TouristLimit
        {
            get { return touristLimit; }
            set
            {
                if (touristLimit != value)
                {
                    touristLimit = value;
                    OnPropertyChanged(nameof(TouristLimit));
                }
            }
        }

        private language? selectedLanguage;
        public language? SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }
        private TourDTO selectedTour;
        public TourDTO SelectedTour
        {
            get { return selectedTour; }
            set
            {
                if (selectedTour != value)
                {
                    selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private string numberOfTourists;
        public string NumberOfTourists
        {
            get { return numberOfTourists; }
            set
            {
                if (numberOfTourists != value)
                {
                    numberOfTourists = value;
                    OnPropertyChanged(nameof(numberOfTourists));
                }
            }
        }
        private string textBlockText;
        public string TextBlockText
        {
            get { return textBlockText; }
            set
            {
                if (textBlockText != value)
                {
                    textBlockText = value;
                    OnPropertyChanged(nameof(TextBlockText));
                }
            }
        }

        public ViewModelCommand DetailedTourViewCommand { get; private set; }
        public ViewModelCommand BookingConfirmationViewCommand { get; private set; }

        private readonly GuestTwoInterfaceViewModel _mainViewModel;
        
        public ViewModelCommand ApplyFiltersCommand { get; private set; }

        public TourDisplayViewModel()
        {
            _mainViewModel = LoggedUser.GuestTwoInterfaceViewModel;
            DetailedTourViewCommand = new ViewModelCommand(ShowDetailedTourView);
            BookingConfirmationViewCommand = new ViewModelCommand(ShowBookingConfirmation);                       
            WindowLoaded();
            ApplyFiltersCommand = new ViewModelCommand(ApplyFilters);
            DetailedId = -1;
            SelectedLanguage = null;
        }

        public void ShowDetailedTourView(object obj)
        {
            _mainViewModel.ExecuteShowDetailedTourView(null);
            TourDTO? tour = SelectedTour;
            DetailedId = tour.id;
        }

        public void ShowBookingConfirmation(object obj)
        {
            TourDTO selectedTour = SelectedTour;
            List<TourDTO> tourDTOS = new List<TourDTO>();
            int index = selectedTour.id;
            int numberOfGuests = Int32.Parse(NumberOfTourists);
            int flag = this.tourService.Book(index, numberOfGuests);
            TourDisplayViewModel.CanExecute = false;

            if (flag == 0)
            {
                TextBlockText = "You have booked the selected tour.";
                TourBookingTransfer tourBookingTransfer = new TourBookingTransfer(
                    selectedTour.id,
                    selectedTour.name,
                    selectedTour.description,
                    selectedTour.cityLocation,
                    selectedTour.countryLocation,
                    selectedTour.keypoints,
                    selectedTour.language,
                    selectedTour.touristLimit,
                    selectedTour.startDates,
                    selectedTour.touristLimit,
                    numberOfGuests
            );
                DataBaseContext context = new DataBaseContext();
                context.tourBookingTransfers.Add(tourBookingTransfer);
                context.SaveChanges();
                TourDisplayViewModel.CanExecute = true;
            }
            else if (flag == 1)
            {
                TextBlockText = "Not enough room for desired number of tourists. Number of spots left for this tour: " + selectedTour.touristLimit;
                tourDTOs.Clear();
                tourDTOS = this.tourService.GetPreviouslySelected(selectedTour.id);
                foreach (TourDTO tourDTO in tourDTOS)
                {
                    tourDTOs.Add(tourDTO);
                }
                TourDisplayViewModel.CanExecute = false;
            }
            else if (flag == -1)
            {
                TextBlockText = "This tour is full. Here are some other tours in the same location.";
                tourDTOs.Clear();
                List<TourDTO> dtos = this.tourService.GetBookableTours(selectedTour.cityLocation, selectedTour.name);
                foreach (TourDTO tourDTO in dtos)
                {
                    tourDTOs.Add(tourDTO);
                }
                TourDisplayViewModel.CanExecute = false;
            }
         
            if (CanExecute == true)
            {
                _mainViewModel.ExecuteShowBookingConfirmation(null);
            }
                
        }

        private void WindowLoaded() 
        {
            foreach (language lan in Enum.GetValues(typeof(language)))
            { 
                languages.Add(lan); 
            }

            DataBaseContext context = new DataBaseContext();
            ObservableCollection<TourDTO> dataList = new ObservableCollection<TourDTO>();
            TourDTO dto = new TourDTO();

            foreach (Tour tour in context.Tours.ToList())
            {
                //dto = tourService.CreateDTO(tour);
                dataList.Add(this.tourService.CreateDTO(tour));
            }

            tourDTOs = dataList;
        }

        private void ApplyFilters(object obj)
        {
            DataBaseContext context = new DataBaseContext();
            
            if (CityName != null)
            {
                tourDTOs.Clear();                
                List<TourDTO> tourDTOsByCityName = this.tourService.GetAllByCity(CityName);
                foreach (TourDTO tourDTO in tourDTOsByCityName) 
                {
                    tourDTOs.Add(tourDTO);
                }
                CityName = null;
            }
            else if (CountryName != null)
            {
                tourDTOs.Clear();
                List<TourDTO> tourDTOsByCountryName = this.tourService.GetAllByCountry(CountryName);
                foreach (TourDTO tourDTO in tourDTOsByCountryName) 
                {
                    tourDTOs.Add(tourDTO);
                }
                CountryName = null;
            }
            else if (Duration != null)
            {
                tourDTOs.Clear();
                List<TourDTO> tourDTOsByDuration = this.tourService.GetAllByDuration(Duration);
                foreach (TourDTO tourDTO in tourDTOsByDuration) 
                {
                    tourDTOs.Add(tourDTO);
                }
                Duration = null;
            }
            else if (TouristLimit != null)
            {
                tourDTOs.Clear();
                List<TourDTO> tourDTOsByTouristLimit = this.tourService.GetAllByTouristLimit(TouristLimit);
                foreach (TourDTO tourDTO in tourDTOsByTouristLimit)
                {
                    tourDTOs.Add(tourDTO);
                }
                TouristLimit = null;
            }
            else if (SelectedLanguage != null)
            {
                tourDTOs.Clear();
                List<TourDTO> tourDTOsByLanguage = this.tourService.GetAllByLanguage((language)SelectedLanguage);               
                foreach (TourDTO tourDTO in tourDTOsByLanguage)
                {
                    tourDTOs.Add(tourDTO);
                }
                SelectedLanguage = null;
            }
            else
            {
                tourDTOs.Clear();
                List<TourDTO> dataList = GetTourDtos(this.tourService, context);
                foreach (TourDTO tourDTO in dataList) 
                {
                    tourDTOs.Add(tourDTO);
                }
            }
        }


        private static List<TourDTO> GetTourDtos(TourService tourService, DataBaseContext context)
        {
            List<TourDTO> dataList = new List<TourDTO>();
            TourDTO dto = new TourDTO();

            foreach (Tour tour in context.Tours.ToList())
            {
                dto = tourService.CreateDTO(tour);
                dataList.Add(dto);
            }

            return dataList;
        }



    }
}
