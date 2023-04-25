using InitialProject.Model;
using InitialProject.WPF.ViewModels;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Context;
using System;

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
            DataContext = new TourGuide_DashboardViewModel();
            this.usernameTextBlock.Text = LoggedUser.username;

            // Load the most requested location and language
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
            // Get the current date and subtract a year to get the date one year ago
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            using (DataBaseContext context = new DataBaseContext())
            {
                // Fetch tour requests from the database that are no longer than a year old
                var tourRequests = context.TourRequests
                                          .Where(tr => tr.startDate >= oneYearAgo)
                                          .ToList();

                return tourRequests;
            }
        }

    }
}
