using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.View.GuestTwoViews;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    
    public class DetailedTourViewModel : ViewModelBase
    {

        private readonly TourService tourService = new(new TourRepository());
        private readonly TourLocationService tourLocationService = new(new TourLocationRepository());   

        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        private string tourName;
        public string TourName
        {
            get { return tourName; }
            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }

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


        private string keyPointNames;
        public string KeyPointNames
        {
            get { return keyPointNames; }
            set
            {
                if (keyPointNames != value)
                {
                    keyPointNames = value;
                    OnPropertyChanged(nameof(KeyPointNames));
                }
            }
        }

        private string numberOfSpots;
        public string NumberOfSpots
        {
            get { return numberOfSpots; }
            set
            {
                if (numberOfSpots != value)
                {
                    numberOfSpots = value;
                    OnPropertyChanged(nameof(NumberOfSpots));
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

        
        private string reservationNumber;
        public string ReservationNumber
        {
            get { return reservationNumber; }
            set
            {
                if (reservationNumber != value)
                {
                    reservationNumber = value;
                    OnPropertyChanged(nameof(ReservationNumber));
                }
            }
        }

        public ViewModelCommand BookingViewCommand { get; private set; }
        private readonly GuestTwoInterfaceViewModel _mainViewModel;
        public int tourId { get; set; }

        public DetailedTourViewModel() 
        {
            _mainViewModel = LoggedUser.GuestTwoInterfaceViewModel;
            BookingViewCommand = new ViewModelCommand(ShowBookingConfirmation);
            WindowLoaded();            
        }

        public void ShowBookingConfirmation(object obj) 
        {
            _mainViewModel.ExecuteTourViewCommand(null);
        }

      

        public void WindowLoaded()
        {
            if (TourDisplayViewModel.DetailedId != -1)
            {
                Id = TourDisplayViewModel.DetailedId.ToString();                
                tourId = int.Parse(Id);
                LoadData();
                TourDisplayViewModel.DetailedId = -1;
            }

            if (GuestTwoMesagesViewModel.tourIdTransfer != -1)
            {
                Id = GuestTwoMesagesViewModel.tourIdTransfer.ToString();
                tourId = int.Parse(Id);
                LoadData();
                GuestTwoMesagesViewModel.tourIdTransfer = -1;
            }          
        }


        //Jedan veliki vatafak
        public void LoadData() 
        {
            Tour tour  = tourService.GetById(tourId);
            TourLocation location = tourLocationService.GetById(tour.location);
            TourName = tour.name;
            CityName = location.city;
            CountryName = location.country;
            Description = tour.description;
            NumberOfSpots = tour.touristLimit.ToString();
            Duration = tour.hoursDuration.ToString();

            DataBaseContext context = new DataBaseContext();    
            List<KeyPoint> keyPoints = this.tourService.GetKeyPoints(tour.id, context);
            foreach (KeyPoint keyPoint in keyPoints)
            {               
                KeyPointNames += "'" + keyPoint.name + "'" + '\n';                                    
            }

        }
    }
}
