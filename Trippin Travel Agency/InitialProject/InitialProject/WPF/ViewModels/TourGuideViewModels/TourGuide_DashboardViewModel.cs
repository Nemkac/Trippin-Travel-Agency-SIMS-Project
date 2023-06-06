using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Context;
using InitialProject.Model.TransferModels;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_DashboardViewModel : ViewModelBase
    {
        public ViewModelCommand CreateTourByLocationCommand { get; private set; }
        public ViewModelCommand CreateTourByLanguageCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private string _locationRequestNumber;
        public string LocationRequestNumber
        {
            get { return _locationRequestNumber; }
            set
            {
                _locationRequestNumber = value;
                OnPropertyChanged(nameof(LocationRequestNumber));
            }
        }

        private string _languageRequestNumber;
        public string LanguageRequestNumber
        {
            get { return _languageRequestNumber; }
            set
            {
                _languageRequestNumber = value;
                OnPropertyChanged(nameof(LanguageRequestNumber));
            }
        }

        public TourGuide_DashboardViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel; 
            CreateTourByLocationCommand = new ViewModelCommand(CreateTourByLocation);
            CreateTourByLanguageCommand = new ViewModelCommand(CreateTourByLanguage);
            LoadMostRequestedData();
        }

        private void LoadMostRequestedData()
        {
            List<TourRequest> tourRequests = FetchTourRequestsFromDatabase();

            (string location, int locationCount) = GetMostRequestedLocation(tourRequests);
            (string language, int languageCount) = GetMostRequestedLanguage(tourRequests);

            if(locationCount  > 0)
            {
                Location = location;
            }
            else
            {
                Location = "No requests"; 
            }
            if(languageCount > 0)
            {
                Language = language;
            }
            else
            {
                Language = "No requests";
            }
            LocationRequestNumber = $"{locationCount}";
            LanguageRequestNumber = $"{languageCount}";
            Username = LoggedUser.firstName; 
        }


        private (string, int) GetMostRequestedLocation(List<TourRequest> tourRequests)
        {
            var mostRequestedLocation = tourRequests.GroupBy(t => new { t.country, t.city })
                                                    .OrderByDescending(g => g.Count())
                                                    .Select(g => new
                                                    {
                                                        Location = $"{g.Key.country}, {g.Key.city}",
                                                        Count = g.Count()
                                                    })
                                                    .FirstOrDefault();

            return mostRequestedLocation != null ? (mostRequestedLocation.Location, mostRequestedLocation.Count) : ("", 0);
        }

        private (string, int) GetMostRequestedLanguage(List<TourRequest> tourRequests)
        {
            var mostRequestedLanguage = tourRequests.GroupBy(t => t.language.ToString())
                                                    .OrderByDescending(g => g.Count())
                                                    .Select(g => new
                                                    {
                                                        Language = g.Key,
                                                        Count = g.Count()
                                                    })
                                                    .FirstOrDefault();

            return mostRequestedLanguage != null ? (mostRequestedLanguage.Language, mostRequestedLanguage.Count) : ("", 0);
        }

        private List<TourRequest> FetchTourRequestsFromDatabase()
        {
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            using (DataBaseContext context = new DataBaseContext())
            {
                var tourRequests = context.TourRequests
                                          .Where(tr => tr.startDate >= oneYearAgo)
                                          .ToList();

                return tourRequests;
            }
        }

        private string[] CreateNamesByLocation(string location)
        {
            string[] locationParts = location.Split(", ");

            return locationParts;
        }

        public void CreateTourByLocation(object obj)
        {
            if (int.TryParse(LocationRequestNumber, out int requestCount) && requestCount > 0)
            {
                string location = Location;
                string[] locationParts = CreateNamesByLocation(location);
                string country = locationParts[0];
                string city = locationParts[1];
                DataBaseContext dataBaseContext = new DataBaseContext();
                TourLocationTransfer tourLocationTransfer = new TourLocationTransfer(country, city);
                TourFlagTransfer tourFlagTransfer = new TourFlagTransfer(0);
                dataBaseContext.TourLocationTransfers.Add(tourLocationTransfer);
                dataBaseContext.TourFlagTransfers.Add(tourFlagTransfer);
                dataBaseContext.SaveChanges();
                _mainViewModel.ExecuteShowTourGuideCreateTourViewCommand(null);
            }
            else
            {
                MessageBox.Show(LocationRequestNumber);
                MessageBox.Show("There are no recommended location requests."); 
            }
        }

        public void CreateTourByLanguage(object obj)
        {
            if (int.TryParse(LanguageRequestNumber, out int requestCount) && requestCount > 0)
            {
                string language = Language;
                DataBaseContext dataBaseContext = new DataBaseContext();
                TourLanguageTransfer tourLanguageTransfer = new TourLanguageTransfer(language);
                TourFlagTransfer tourFlagTransfer = new TourFlagTransfer(1);
                dataBaseContext.TourLanguageTransfers.Add(tourLanguageTransfer);
                dataBaseContext.TourFlagTransfers.Add(tourFlagTransfer);
                dataBaseContext.SaveChanges();
                _mainViewModel.ExecuteShowTourGuideCreateTourViewCommand(null);
            }
            else
            {
                MessageBox.Show(LanguageRequestNumber);
                MessageBox.Show("There are no recommended language requests.");
            }
        }
    }
}
