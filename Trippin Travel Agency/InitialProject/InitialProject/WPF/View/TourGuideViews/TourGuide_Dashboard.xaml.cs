using InitialProject.Model;
using InitialProject.WPF.ViewModels;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Context;
using System;
using InitialProject.DTO;
using InitialProject.Model.TransferModels;
using System.Windows;

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Dashboard.xaml
    /// </summary>
    public partial class TourGuide_Dashboard : UserControl
    {
        public TourGuide_Dashboard()
        {
            InitializeComponent();
            //DataContext = new TourGuide_DashboardViewModel();
            this.usernameTextBlock.Text = LoggedUser.username;
            LoadMostRequestedData();
        }

        private void LoadMostRequestedData()
        {
            List<TourRequest> tourRequests = FetchTourRequestsFromDatabase();

            (string location, int locationCount) = GetMostRequestedLocation(tourRequests);
            (string language, int languageCount) = GetMostRequestedLanguage(tourRequests);

            locationTextBlock.Text = location;
            languageTextBlock.Text = language;
            locationRequestNumberTextBlock.Text = $"{locationCount} requests";
            languageRequestNumberTextBlock.Text = $"{languageCount} requests";
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
        public void createTourByLocationButton_Click(object sender, RoutedEventArgs e)
        {
            string location = locationTextBlock.Text;
            string[] locationParts = CreateNamesByLocation(location);
            string country = locationParts[0];
            string city = locationParts[1];
            DataBaseContext dataBaseContext = new DataBaseContext();
            TourLocationTransfer tourLocationTransfer = new TourLocationTransfer(country, city);
            TourFlagTransfer tourFlagTransfer = new TourFlagTransfer(0);
            dataBaseContext.TourLocationTransfers.Add(tourLocationTransfer);
            dataBaseContext.TourFlagTransfers.Add(tourFlagTransfer);
            dataBaseContext.SaveChanges();
        }

        public void createTourByLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            string language = languageTextBlock.Text;
            DataBaseContext dataBaseContext = new DataBaseContext();
            TourLanguageTransfer tourLanguageTransfer = new TourLanguageTransfer(language);
            TourFlagTransfer tourFlagTransfer = new TourFlagTransfer(1);
            dataBaseContext.TourLanguageTransfers.Add(tourLanguageTransfer);
            dataBaseContext.TourFlagTransfers.Add(tourFlagTransfer);
            dataBaseContext.SaveChanges();
        }
    }
}
