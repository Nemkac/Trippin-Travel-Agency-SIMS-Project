using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class CreateYourOwnTourViewModel : ViewModelBase
    {
        private TourLocationService tourLocationService = new(new TourLocationRepository());
        public ObservableCollection<language> languages { get; set; } = new ObservableCollection<language>();
        public ObservableCollection<string> CountryComboBox { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> CityComboBox { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<TourRequestDTO> requests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public List<TourRequest> temporaryRequests { get; set; } = new List<TourRequest>();
       
        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged(nameof(Country));
                    FeedbackMessage = "";
                    countryComboBox_SelectionChanged();
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                    FeedbackMessage = "";
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                    FeedbackMessage = "";
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                    FeedbackMessage = "";
                }
            }
        }

        private string descriptionBox;
        public string DescriptionBox
        {
            get { return descriptionBox; }
            set
            {
                if (descriptionBox != value)
                {
                    descriptionBox = value;
                    OnPropertyChanged(nameof(DescriptionBox));
                    FeedbackMessage = "";
                }
            }
        }

        private int guestNumberInput;
        public int GuestNumberInput
        {
            get { return guestNumberInput; }
            set
            {
                if (guestNumberInput != value)
                {
                    guestNumberInput = value;
                    OnPropertyChanged(nameof(GuestNumberInput));
                    FeedbackMessage = "";
                }
            }
        }

        private language languageComboBox;
        public language LanguageComboBox
        {
            get { return languageComboBox; }
            set
            {
                if (languageComboBox != value)
                {
                    languageComboBox = value;
                    OnPropertyChanged(nameof(LanguageComboBox));
                    FeedbackMessage = "";
                }
            }
        }
       
        private string feedbackMessage;
        public string FeedbackMessage
        {
            get { return feedbackMessage; }
            set
            {
                if (feedbackMessage != value)
                {
                    feedbackMessage = value;
                    OnPropertyChanged(nameof(FeedbackMessage));                    
                }
            }
        }
        
        private string responseColor;
        public string ResponseColor
        {
            get { return responseColor; }
            set
            {
                if (responseColor != value)
                {
                    responseColor = value;
                    OnPropertyChanged(nameof(ResponseColor));

                }
            }
        }

        public ViewModelCommand CreateRegularTourCommand { get; private set; }
        public ViewModelCommand CreateComplexTourCommand { get; private set; }
        public ViewModelCommand ConfirmComplexTour { get; private set; }

        

        public CreateYourOwnTourViewModel()
        {
            CreateRegularTourCommand = new ViewModelCommand(ExecuteCreateRegularTour);
            CreateComplexTourCommand = new ViewModelCommand(ExecuteCreateComplexTour);
            ConfirmComplexTour = new ViewModelCommand(ExecuteConfirmComplexTour);
            LoadInputs();
            
        }

        private void LoadInputs()
        {
            List<TourLocation> tourLocations = this.tourLocationService.GetAllTourLocations();
            foreach (TourLocation location in tourLocations)
            {
                if (!CountryComboBox.Contains(location.country))
                {
                    CountryComboBox.Add(location.country);
                }
            }
            foreach (language lan in Enum.GetValues(typeof(language)))
            {
                languages.Add(lan);

            }
        }

        public void countryComboBox_SelectionChanged()
        {
            CityComboBox.Clear();
            string selectedCountry = Country;
            GetCitiesByCountry(selectedCountry);
        }

        private void GetCitiesByCountry(string selectedCountry)
        {
            DataBaseContext cityContext = new DataBaseContext();
            List<TourLocation> cityList = cityContext.TourLocation.ToList();

            foreach (TourLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!CityComboBox.Contains(location.city))
                    {
                        CityComboBox.Add(location.city);
                    }
                }
            }
        }

        private void ExecuteCreateRegularTour(object obj)
        {
            if (Country != null
                && City != null
                && StartDate != null
                && EndDate != null
                && GuestNumberInput != null
                && LanguageComboBox != null
                && DescriptionBox != null
                && GuestNumberInput > 0)
            {

                if (StartDate < EndDate)
                {
                    TourRequest tourRequest = new TourRequest(City,
                                                              Country,
                                                              GuestNumberInput,
                                                              LanguageComboBox,
                                                              StartDate,
                                                              EndDate,
                                                              DescriptionBox,
                                                              LoggedUser.id);
                    DataBaseContext context = new DataBaseContext();

                    bool cityFlag = false;
                    bool countryFlag = false;
                    bool languageFlag = false;

                    foreach (Tour tour in context.Tours.ToList())
                    {
                        TourLocation location = this.tourLocationService.GetById(tour.location);
                        if (City == location.city)
                        {
                            cityFlag = true;
                        }

                        if (Country == location.country)
                        {
                            countryFlag = true;
                        }
                        if (LanguageComboBox == tour.language)
                        {
                            languageFlag = true;
                        }
                    }
                    if (!cityFlag)
                    {
                        context.UnfulfilledTourCities.Add(new(LoggedUser.id, City));
                    }
                    if (!countryFlag)
                    {
                        context.unfulfilledTourCountries.Add(new(LoggedUser.id, Country));
                    }
                    if (!languageFlag)
                    {
                        context.UnfulfilledTourLanguages.Add(new(LoggedUser.id, LanguageComboBox));
                    }
                    context.TourRequests.Add(tourRequest);
                    ResponseColor = "#4cd137";
                    FeedbackMessage = "Request sent!";
                    context.SaveChanges();
                                       
                }
                else
                {
                    ResponseColor = "#e84118";
                    FeedbackMessage = "Invalid dates!";
                    
                }
            }
            else 
            {
                ResponseColor = "#e84118";
                FeedbackMessage = "Please enter complete request information";
               
            }
        }

        private void ExecuteCreateComplexTour(object obj)
        {

            if (Country != null
                && City != null
                && StartDate != null
                && EndDate != null
                && GuestNumberInput != null
                && LanguageComboBox != null
                && DescriptionBox != null
                && GuestNumberInput > 0)
            {

                if (StartDate < EndDate)
                {
                    TourRequest tourRequest = new TourRequest(City,
                                                              Country,
                                                              GuestNumberInput,
                                                              LanguageComboBox,
                                                              StartDate,
                                                              EndDate,
                                                              DescriptionBox,
                                                              LoggedUser.id);
                    DataBaseContext context = new DataBaseContext();                   
                    temporaryRequests.Add(tourRequest);
                    requests.Add(new TourRequestDTO(City, Country,LanguageComboBox,StartDate.ToShortDateString(),EndDate.ToShortDateString(),TourRequestStatus.OnHold));                    
                }
                else
                {
                    ResponseColor = "#e84118";
                    FeedbackMessage = "Invalid dates!";
                }
            }
            else
            {
                ResponseColor = "#e84118";
                FeedbackMessage = "Please enter complete request information";
            }
        }
        private void ExecuteConfirmComplexTour(object obj)
        {
            if (temporaryRequests.Count > 0)
            {
                ComplexTourRequest complexTourRequest = new ComplexTourRequest(temporaryRequests);
                DataBaseContext context = new DataBaseContext();
                foreach (TourRequest request in temporaryRequests)
                {
                    bool cityFlag = false;
                    bool countryFlag = false;
                    bool languageFlag = false;

                    foreach (Tour tour in context.Tours.ToList())
                    {
                        TourLocation location = this.tourLocationService.GetById(tour.location);
                        if (City == location.city)
                        {
                            cityFlag = true;
                        }

                        if (Country == location.country)
                        {
                            countryFlag = true;
                        }
                        if (LanguageComboBox == tour.language)
                        {
                            languageFlag = true;
                        }
                    }
                    if (!cityFlag)
                    {
                        context.UnfulfilledTourCities.Add(new(LoggedUser.id, request.city));
                    }
                    if (!countryFlag)
                    {
                        context.unfulfilledTourCountries.Add(new(LoggedUser.id, request.country));
                    }
                    if (!languageFlag)
                    {
                        context.UnfulfilledTourLanguages.Add(new(LoggedUser.id, request.language));
                    }
                }
                context.ComplexTourRequests.Add(complexTourRequest);
                context.SaveChanges();
                requests.Clear();
            }
        }
    }
}
