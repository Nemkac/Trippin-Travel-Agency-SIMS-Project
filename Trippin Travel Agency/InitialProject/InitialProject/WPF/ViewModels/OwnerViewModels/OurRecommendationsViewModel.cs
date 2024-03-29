﻿using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class OurRecommendationsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;

        private readonly BookingService bookingService = new(new BookingRepository());
        private readonly AccommodationService accommodationService = new(new AccommodationRepository());

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

        private string _mostPopularCountry;
        public string MostPopularCountry
        {
            get { return _mostPopularCountry; }
            set
            {
                _mostPopularCountry = value;
                OnPropertyChanged(nameof(MostPopularCountry));
            }
        }

        private string _leastPopularCity;
        public string LeastPopularCity
        {
            get { return _leastPopularCity; }
            set
            {
                _leastPopularCity = value;
                OnPropertyChanged(nameof(LeastPopularCity));
            }
        }

        private string _leastPopularCountry;
        public string LeastPopularCountry
        {
            get { return _leastPopularCountry; }
            set
            {
                _leastPopularCountry = value;
                OnPropertyChanged(nameof(LeastPopularCountry));
            }
        }

        private string _mostPopularCity;
        public string MostPopularCity
        {
            get { return _mostPopularCity; }
            set
            {
                _mostPopularCity = value;
                OnPropertyChanged(nameof(MostPopularCity));
            }
        }

        private string _mostPopularLocationImage;

        public string MostPopularLocationImage
        {
            get { return _mostPopularLocationImage; }
            set
            {
                _mostPopularLocationImage = value;
                OnPropertyChanged(nameof(MostPopularLocationImage));
            }
        }

        private string _leastPopularLocationImage;

        public string LeastPopularLocationImage
        {
            get { return _leastPopularLocationImage; }
            set
            {
                _leastPopularLocationImage = value;
                OnPropertyChanged(nameof(LeastPopularLocationImage));
            }
        }


        private string _ourRecommendationsText;

        public string OurRecommendationsText
        {
            get { return _ourRecommendationsText; }
            set
            {
                _ourRecommendationsText = value;
                OnPropertyChanged(nameof(OurRecommendationsText));
            }
        }

        private string _basedOnMostAndLeastPopularLocationsText;

        public string BasedOnMostAndLeastPopularLocationsText
        {
            get { return _basedOnMostAndLeastPopularLocationsText; }
            set
            {
                _basedOnMostAndLeastPopularLocationsText = value;
                OnPropertyChanged(nameof(BasedOnMostAndLeastPopularLocationsText));
            }
        }

        private string _mostPopularLocationText;

        public string MostPopularLocationText
        {
            get { return _mostPopularLocationText; }
            set
            {
                _mostPopularLocationText = value;
                OnPropertyChanged(nameof(MostPopularLocationText));
            }
        }

        private string _leastPopularLocationText;

        public string LeastPopularLocationText
        {
            get { return _leastPopularLocationText; }
            set
            {
                _leastPopularLocationText = value;
                OnPropertyChanged(nameof(LeastPopularLocationText));
            }
        }

        private string _openNewAccommodationText;

        public string OpenNewAccommodationText
        {
            get { return _openNewAccommodationText; }
            set
            {
                _openNewAccommodationText = value;
                OnPropertyChanged(nameof(OpenNewAccommodationText));
            }
        }

        private string _closeExistingAccommodationText;

        public string CloseExistingAccommodationText
        {
            get { return _closeExistingAccommodationText; }
            set
            {
                _closeExistingAccommodationText = value;
                OnPropertyChanged(nameof(CloseExistingAccommodationText));
            }
        }


        public ICommand OpenNewAccommodation { get; }
        public ICommand CloseAccommodation { get; }

        public OurRecommendationsViewModel() 
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            Mediator.IsCheckedChanged += OnIsCheckedChanged;
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;
            GetMostAndLeastPopularLocation();

            OpenNewAccommodation = new ViewModelCommand(ExecuteOpenNewAccommodation);
            CloseAccommodation = new ViewModelCommand(ExecuteCloseExistingAccommodation);

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";

            OurRecommendationsText = Mediator.GetCurrentIsLanguageChecked() ? "Nase Preporuke" : "Our Recommendations";
            BasedOnMostAndLeastPopularLocationsText = Mediator.GetCurrentIsLanguageChecked() ? "Bazirano na najpopularnijoj i najnepopularnijoj lokaciji" : "Based on most and least popular location";
            MostPopularLocationText = Mediator.GetCurrentIsLanguageChecked() ? "NAJPOPULARNIJA LOKACIJA" : "MOST POPULAR LOCATION";
            LeastPopularLocationText = Mediator.GetCurrentIsLanguageChecked() ? "NAJNEPOPULARNIJA LOKACIJA" : "LEAST POPULAR LOCATION";
            OpenNewAccommodationText = Mediator.GetCurrentIsLanguageChecked() ? "Otvori novi smestaj" : "Open new accommodation";
            CloseExistingAccommodationText = Mediator.GetCurrentIsLanguageChecked() ? "Zatvori postojeci smestaj" : "Close existing accommodation";
        }

        private void ExecuteOpenNewAccommodation(object obj)
        {
            this._mainViewModel.ExecuteCreateNewAccommodationFromRecommendationViewCommand(null);
        }

        private void ExecuteCloseExistingAccommodation(object obj)
        {
            this._mainViewModel.ExecuteCloseExistingAccommodationCommand(null);
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            OurRecommendationsText = isChecked ? "Nase Preporuke" : "Our Recommendations";
            BasedOnMostAndLeastPopularLocationsText = isChecked ? "Bazirano na najpopularnijoj i najnepopularnijoj lokaciji" : "Based on most and least popular location";
            MostPopularLocationText = isChecked ? "NAJPOPULARNIJA LOKACIJA" : "MOST POPULAR LOCATION";
            LeastPopularLocationText = isChecked ? "NAJNEPOPULARNIJA LOKACIJA" : "LEAST POPULAR LOCATION";
            OpenNewAccommodationText = isChecked ? "Otvori novi smestaj" : "Open new accommodation";
            CloseExistingAccommodationText = isChecked ? "Zatvori postojeci smestaj" : "Close existing accommodation";
        }

        public void GetMostAndLeastPopularLocation()
        {
            Dictionary<string, int> numberOfBookingsPerLocation = new Dictionary<string, int>();
            List<Booking> bookings = this.bookingService.GetAllBookings();
            List<Accommodation> ownersAccommodations = this.accommodationService.GetAccommodationsByOwnerId(1);

            GetNumberOfBookingsPerLocation(numberOfBookingsPerLocation, bookings, ownersAccommodations);

            var sortedDict = numberOfBookingsPerLocation.OrderByDescending(x => x.Value);
            string mostPopularLocation = sortedDict.FirstOrDefault().Key;
            string leastPopularLocation = sortedDict.LastOrDefault().Key;

            if (mostPopularLocation != null && leastPopularLocation != null)
            {
                UpdateImageAndLocation(mostPopularLocation, leastPopularLocation);
            }
        }

        private void GetNumberOfBookingsPerLocation(Dictionary<string, int> numberOfBookingsPerLocation, List<Booking> bookings, List<Accommodation> ownersAccommodations)
        {
            foreach (Booking booking in bookings)
            {
                foreach (Accommodation accommodation in ownersAccommodations)
                {
                    if (booking.accommodationId == accommodation.id)
                    {
                        List<string> locationToParse = this.accommodationService.GetAccommodationLocation(accommodation.id);
                        string city = locationToParse[1] + "_" + locationToParse[0];

                        if (numberOfBookingsPerLocation.ContainsKey(city))
                        {
                            numberOfBookingsPerLocation[city]++;
                        }
                        else numberOfBookingsPerLocation[city] = 1;

                    }
                }
            }
        }

        private void UpdateImageAndLocation(string mostPopularLocation, string leastPopularLocation)
        {
            string[] countryAndCity = mostPopularLocation.Split("_");
            MostPopularCountry = countryAndCity[1] + ", ";
            MostPopularCity = countryAndCity[0];

            LoggedUser.MostPopularCity = MostPopularCity;
            LoggedUser.MostPopularCountry = MostPopularCountry.Replace(", ", "");

            string formatedLocationName = mostPopularLocation.ToLower().Replace(" ", "");

            MostPopularLocationImage = "pack://application:,,,/Assets/Existing Assets/" + formatedLocationName + ".jpg";


            string[] leastPopularCountryAndCity = leastPopularLocation.Split("_");
            LeastPopularCountry = leastPopularCountryAndCity[1] + ", ";
            LeastPopularCity = leastPopularCountryAndCity[0];

            LoggedUser.LeastPopularCity = LeastPopularCity;
            LoggedUser.LeastPopularCountry = LeastPopularCountry.Replace(", ", "");

            string formatedLeastPopularLocationName = leastPopularLocation.ToLower().Replace(" ", "");

            LeastPopularLocationImage = "pack://application:,,,/Assets/Existing Assets/" + formatedLeastPopularLocationName + ".jpg";
        }
    }
}
