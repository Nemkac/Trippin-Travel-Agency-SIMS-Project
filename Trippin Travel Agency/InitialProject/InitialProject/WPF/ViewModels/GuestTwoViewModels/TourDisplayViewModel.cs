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
        private readonly TourLocationService tourLocationService = new(new TourLocationRepository());

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

        private string recommendedTourName1;
        public string RecommendedTourName1
        {
            get { return recommendedTourName1; }
            set
            {
                if (recommendedTourName1 != value)
                {
                    recommendedTourName1 = value;
                    OnPropertyChanged(nameof(RecommendedTourName1));
                }
            }
        }
        private string recommendedTourName2;
        public string RecommendedTourName2
        {
            get { return recommendedTourName2; }
            set
            {
                if (recommendedTourName2 != value)
                {
                    recommendedTourName2 = value;
                    OnPropertyChanged(nameof(RecommendedTourName2));
                }
            }
        }
        private string recommendedTourName3;
        public string RecommendedTourName3
        {
            get { return recommendedTourName3; }
            set
            {
                if (recommendedTourName3 != value)
                {
                    recommendedTourName3 = value;
                    OnPropertyChanged(nameof(RecommendedTourName3));
                }
            }
        }

        private string recommendedTourImage1;
        public string RecommendedTourImage1
        {
            get { return recommendedTourImage1; }
            set
            {
                if (recommendedTourImage1 != value)
                {
                    recommendedTourImage1 = value;
                    OnPropertyChanged(nameof(RecommendedTourImage1));
                }
            }
        }
        private string recommendedTourImage2;
        public string RecommendedTourImage2
        {
            get { return recommendedTourImage2; }
            set
            {
                if (recommendedTourImage2 != value)
                {
                    recommendedTourImage2= value;
                    OnPropertyChanged(nameof(RecommendedTourImage2));
                }
            }
        }

        private string recommendedTourImage3;
        public string RecommendedTourImage3
        {
            get { return recommendedTourImage3; }
            set
            {
                if (recommendedTourImage3 != value)
                {
                    recommendedTourImage3 = value;
                    OnPropertyChanged(nameof(RecommendedTourImage3));
                }
            }
        }

        public ViewModelCommand DetailedTourViewCommand { get; private set; }
        public ViewModelCommand BookingConfirmationViewCommand { get; private set; }

        private readonly GuestTwoInterfaceViewModel _mainViewModel;
        public ViewModelCommand DetailedRecommendedView1 {  get; private set; }
        public ViewModelCommand DetailedRecommendedView2 { get; private set; }
        public ViewModelCommand DetailedRecommendedView3 { get; private set; }

        public ViewModelCommand ApplyFiltersCommand { get; private set; }

        public TourDisplayViewModel()
        {
            _mainViewModel = LoggedUser.GuestTwoInterfaceViewModel;
            DetailedTourViewCommand = new ViewModelCommand(ShowDetailedTourView);
            BookingConfirmationViewCommand = new ViewModelCommand(ShowBookingConfirmation);
            DetailedRecommendedView1 = new ViewModelCommand(ShowDetailedTourFromRecommendation1);
            DetailedRecommendedView2 = new ViewModelCommand(ShowDetailedTourFromRecommendation2);
            DetailedRecommendedView3 = new ViewModelCommand(ShowDetailedTourFromRecommendation3);

            WindowLoaded();
            LoadRecommendations();
            ApplyFiltersCommand = new ViewModelCommand(ApplyFilters);
            DetailedId = -1;
            SelectedLanguage = null;
        }

        public void ShowDetailedTourView(object obj)
        {
            TourDTO? tour = SelectedTour;
            if(tour != null) 
            {                
                _mainViewModel.ExecuteShowDetailedTourView(null);
                DetailedId = tour.id;
            }
        }

        public void ShowDetailedTourFromRecommendation1(object obj) 
        {
            Tour tour = tourService.GetByName(RecommendedTourName1);
            _mainViewModel.ExecuteShowDetailedTourView(null);
            DetailedId = tour.id;
        }
        public void ShowDetailedTourFromRecommendation2(object obj)
        {
            Tour tour = tourService.GetByName(RecommendedTourName2);
            _mainViewModel.ExecuteShowDetailedTourView(null);
            DetailedId = tour.id;
        }
        public void ShowDetailedTourFromRecommendation3(object obj)
        {
            Tour tour = tourService.GetByName(RecommendedTourName3);
            _mainViewModel.ExecuteShowDetailedTourView(null);
            DetailedId = tour.id;
        }
        public void ShowBookingConfirmation(object obj)
        {
            TourDTO selectedTour = SelectedTour;
            List<TourDTO> tourDTOS = new List<TourDTO>();
            if (selectedTour != null)
            {
                int index = selectedTour.id;
                int numberOfGuests;
                bool parsing = int.TryParse(NumberOfTourists, out numberOfGuests);
                if (parsing == false)
                {
                    return;
                }
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
            else 
            {
                TextBlockText = "Please select a tour!";
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

        public void LoadRecommendations() 
        {
            DataBaseContext context = new DataBaseContext();
            Dictionary<int,int> indexes = new Dictionary<int,int>();

            foreach(TourAttendance attendance in context.TourAttendances.ToList()) 
            {
                if (!indexes.ContainsKey(attendance.tourId))
                {
                    indexes.Add(attendance.tourId, 1);
                }
                else {
                    indexes[attendance.tourId]++;
                }
            }

            var sortedIndexes = from entry in indexes orderby entry.Value descending select entry;
            var firstThreeTours = sortedIndexes.Take(3);
            var toursToDict = firstThreeTours.ToDictionary(x => x.Key, x=>x.Value);

            int counter = 0; 
            foreach (KeyValuePair<int, int> entry in toursToDict) 
            {
                //3 dvojke 2 keca 2 trojke
                //MessageBox.Show(entry.Key.ToString(), entry.Value.ToString());
                Tour tour = tourService.GetByID(entry.Key);
                TourLocation location = tourLocationService.GetById(tour.location);              
                if (counter == 0) 
                {                    
                    RecommendedTourName1 = tour.name;
                    RecommendedTourImage1 = "pack://application:,,,/Assets/Existing Assets/"+location.city+"1.jpg";
                }
                if (counter == 1) 
                {                    
                    RecommendedTourName2 = tour.name;
                    RecommendedTourImage2 = "pack://application:,,,/Assets/Existing Assets/" +location.city +"1.jpg";
                }
                if (counter == 2)
                {                    
                    RecommendedTourName3 = tour.name;
                    RecommendedTourImage3 = "pack://application:,,,/Assets/Existing Assets/" +location.city +"1.jpg";
                }
                counter++;                
            }
        }
    }
}
