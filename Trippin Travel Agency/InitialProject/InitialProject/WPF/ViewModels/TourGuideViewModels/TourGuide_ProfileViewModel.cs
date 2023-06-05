using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_ProfileViewModel : ViewModelBase
    {
        public string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public string userLastName;
        public string UserLastName
        {
            get { return userLastName; }
            set
            {
                if (userLastName != value)
                {
                    userLastName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        private Window parentWindow;
        public Window ParentWindow
        {
            get { return parentWindow; }
            set
            {
                if (parentWindow != value)
                {
                    parentWindow = value;
                    OnPropertyChanged(nameof(ParentWindow));
                }
            }
        }
        private string guideStatus;
        public string GuideStatus
        {
            get { return guideStatus; }
            set
            {
                if (guideStatus != value)
                {
                    guideStatus = value;
                    OnPropertyChanged(nameof(GuideStatus));
                }
            }
        }
        private string averageRating;
        public string AverageRating
        {
            get { return averageRating; }
            set
            {
                if (averageRating != value)
                {
                    averageRating = value;
                    OnPropertyChanged(nameof(AverageRating));
                }
            }
        }
        private string howMuchOnLanguage;
        public string HowMuchOnLanguage
        {
            get { return howMuchOnLanguage; }
            set
            {
                if (howMuchOnLanguage != value)
                {
                    howMuchOnLanguage = value;
                    OnPropertyChanged(nameof(HowMuchOnLanguage));
                }
            }
        }
        private string superLanguages;
        public string SuperLanguages
        {
            get { return superLanguages; }
            set
            {
                if (superLanguages != value)
                {
                    superLanguages = value;
                    OnPropertyChanged(nameof(SuperLanguages));
                }
            }
        }

        public ViewModelCommand ShowDashboardCommand { get; private set; }
        public ViewModelCommand CancelAllToursCommand { get; private set; }
        public ViewModelCommand LogoutCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ProfileViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
            CancelAllToursCommand = new ViewModelCommand(CancelAllTours);
            LogoutCommand = new ViewModelCommand(Logout);
            userName = LoggedUser.firstName; 
            userLastName = LoggedUser.lastName;
            UpdateGuideStatus();
        }
        private void SetSuperGuideStatus(bool isSuperGuide)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                var user = dataBaseContext.Users.Find(LoggedUser.id);
                if (user != null)
                {
                    user.super = isSuperGuide;
                    dataBaseContext.SaveChanges();
                }
            }
        }
        private void SetProperties(Dictionary<language, int> languageTourCounts, KeyValuePair<language, int>? languageWithMaxTours, double averageRating, double roundedAverage)
        {
            if (averageRating > 4.0)
            {
                GuideStatus = $"Super guide for language: {languageWithMaxTours.Value.Key}";
                AverageRating = roundedAverage.ToString();
                HowMuchOnLanguage = $"{languageTourCounts[languageWithMaxTours.Value.Key]} tours on {languageWithMaxTours.Value.Key}";
                SetSuperGuideStatus(true);
            }
            else
            {
                GuideStatus = "Regular guide";
                AverageRating = "N/A";
                HowMuchOnLanguage = "Keep working";
                SetSuperGuideStatus(false);
            }
        }
        private void UpdateGuideStatus()
        {
            DateTime oneYearAgo = DateTime.Now.Date.AddYears(-1);
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Dictionary<language, int> languageTourCounts = TourLanguageCount(oneYearAgo, dataBaseContext);

                KeyValuePair<language, int>? languageWithMaxTours = null;
                languageWithMaxTours = MaximumLanguage(languageTourCounts, languageWithMaxTours);

                if (languageWithMaxTours == null)
                {
                    GuideStatus = "Regular guide";
                    AverageRating = "N/A";
                    HowMuchOnLanguage = "Keep working";
                    SetSuperGuideStatus(false);
                    return;
                }

                double averageRating, roundedAverage;
                CountAverages(oneYearAgo, dataBaseContext, languageWithMaxTours, out averageRating, out roundedAverage);
                SetProperties(languageTourCounts, languageWithMaxTours, averageRating, roundedAverage);
            }
        }
        private static void CountAverages(DateTime oneYearAgo, DataBaseContext dataBaseContext, KeyValuePair<language, int>? languageWithMaxTours, out double averageRating, out double roundedAverage)
        {
            var tourIdsWithLanguage = dataBaseContext.Tours
                            .Where(tour => tour.guideId == LoggedUser.id && tour.startDates >= oneYearAgo && tour.language == languageWithMaxTours.Value.Key)
                            .Select(tour => tour.id)
                            .ToList();

            var languageRatings = dataBaseContext.TourAndGuideRates
                .Where(rate => tourIdsWithLanguage.Contains(rate.tourId))
                .Select(rate => (rate.guideKnowledgeRating + rate.guideLanguageUsageRating + rate.contentRating) / 3.0)
                .ToList();

            averageRating = languageRatings.DefaultIfEmpty(0).Average();
            roundedAverage = Math.Round(averageRating, 2);
        }
        private static KeyValuePair<language, int>? MaximumLanguage(Dictionary<language, int> languageTourCounts, KeyValuePair<language, int>? languageWithMaxTours)
        {
            if (languageTourCounts.Any(pair => pair.Value >= 20))
            {
                languageWithMaxTours = languageTourCounts
                    .Where(pair => pair.Value >= 20)
                    .OrderByDescending(pair => pair.Value)
                    .FirstOrDefault();
            }

            return languageWithMaxTours;
        }
        private static Dictionary<language, int> TourLanguageCount(DateTime oneYearAgo, DataBaseContext dataBaseContext)
        {
            return dataBaseContext.Tours
                                .Where(tour => tour.guideId == LoggedUser.id && tour.startDates >= oneYearAgo)
                                .GroupBy(tour => tour.language)
                                .ToDictionary(g => g.Key, g => g.Count());
        }
        public void CancelAllTours(object parameter)
        {
            if (LoggedUser.id != null)
            {
                using (DataBaseContext dataBaseContext = new DataBaseContext())
                {
                    DateTime currentDate = DateTime.Now.Date;
                    List<Tour> toursToDelete = dataBaseContext.Tours
                        .Where(t => t.guideId == LoggedUser.id && t.startDates >= currentDate)
                        .ToList();
                    if (toursToDelete.Count > 0)
                    {
                        foreach (Tour tourToDelete in toursToDelete)
                        {
                            // Remove related entities
                            dataBaseContext.Images.RemoveRange(dataBaseContext.Images.Where(i => i.tourId == tourToDelete.id));
                            dataBaseContext.KeyPoints.RemoveRange(dataBaseContext.KeyPoints.Where(k => k.tourId == tourToDelete.id));
                            dataBaseContext.TourAttendances.RemoveRange(dataBaseContext.TourAttendances.Where(ta => ta.tourId == tourToDelete.id));
                            dataBaseContext.TourLiveViewTransfers.RemoveRange(dataBaseContext.TourLiveViewTransfers.Where(tlt => tlt.tourId == tourToDelete.id));
                            dataBaseContext.TourMessages.RemoveRange(dataBaseContext.TourMessages.Where(tm => tm.tourId == tourToDelete.id));
                            dataBaseContext.TourReservations.RemoveRange(dataBaseContext.TourReservations.Where(tr => tr.tourId == tourToDelete.id));

                            dataBaseContext.Tours.Remove(tourToDelete);

                            foreach (TourReservation tr in dataBaseContext.TourReservations.Where(tr => tr.tourId == tourToDelete.id).ToList())
                            {
                                Coupon coupon = new Coupon(tr.guestId, DateTime.Now.AddYears(2));
                                dataBaseContext.Coupons.Add(coupon);
                            }
                            var user = dataBaseContext.Users.SingleOrDefault(u => u.id == LoggedUser.id);
                            if (user != null)
                            {
                                user.resigned = true;
                            }
                            dataBaseContext.SaveChanges();
                        }
                    }
                    else
                    {
                        MessageBox.Show("There are no tours to be canceled for the resigning tour guide. Wish you all the best!");
                        var user = dataBaseContext.Users.SingleOrDefault(u => u.id == LoggedUser.id);
                        if (user != null)
                        {
                            user.resigned = true;
                        }
                        dataBaseContext.SaveChanges();
                    }
                    Logout(parameter); 
                }
            }
        }
        public void Logout(object parameter)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window?.Close();
            SignInForm sf = new SignInForm();
            sf.Show();
        }
        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }

    }
}
