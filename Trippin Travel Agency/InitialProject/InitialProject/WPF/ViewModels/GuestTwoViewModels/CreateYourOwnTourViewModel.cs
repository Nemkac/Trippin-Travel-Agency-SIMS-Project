﻿using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                }
            }
        }

        public ViewModelCommand CreateRegularTourCommand { get; private set; }

    public CreateYourOwnTourViewModel()
        {
            CreateRegularTourCommand = new ViewModelCommand(ExecuteCreateRegularTour);
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
                && DescriptionBox != null)
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
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Invalid dates!");
                }
            }
        }
    }
}
