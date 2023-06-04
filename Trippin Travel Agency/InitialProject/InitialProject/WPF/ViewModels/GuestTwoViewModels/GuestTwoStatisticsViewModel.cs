using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections;
using InitialProject.Repository;
using InitialProject.Service.TourServices;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoStatisticsViewModel : ViewModelBase
    {

        public ObservableCollection<string> YearComboBox { get; set; } = new ObservableCollection<string>();

        private readonly TourLocationService tourLocationService = new(new TourLocationRepository());

        private string usernameLabel;
        public string UsernameLabel
        {
            get { return usernameLabel; }
            set
            {
                if (usernameLabel != value)
                {
                    usernameLabel = value;
                    OnPropertyChanged(nameof(UsernameLabel));
                }
            }
        }

        private string usernameLabel2;
        public string UsernameLabel2
        {
            get { return usernameLabel2; }
            set
            {
                if (usernameLabel2 != value)
                {
                    usernameLabel2 = value;
                    OnPropertyChanged(nameof(UsernameLabel2));
                }
            }
        }

        private string accountType;
        public string AccountType
        {
            get { return accountType; }
            set
            {
                if (accountType != value)
                {
                    accountType = value;
                    OnPropertyChanged(nameof(AccountType));
                }
            }
        }

        private string nameAndLastname;
        public string NameAndLastname
        {
            get { return nameAndLastname; }
            set
            {
                if (nameAndLastname != value)
                {
                    nameAndLastname = value;
                    OnPropertyChanged(nameof(NameAndLastname));
                }
            }
        }

        private string acceptedTours;
        public string AcceptedTours
        {
            get { return acceptedTours; }
            set
            {
                if (acceptedTours != value)
                {
                    acceptedTours = value;
                    OnPropertyChanged(nameof(AcceptedTours));
                }
            }
        }
        private string averageNumberOfPeople;
        public string AverageNumberOfPeople
        {
            get { return averageNumberOfPeople; }
            set
            {
                if (averageNumberOfPeople != value)
                {
                    averageNumberOfPeople = value;
                    OnPropertyChanged(nameof(AverageNumberOfPeople));
                }
            }
        }
        private string declinedTours;
        public string DeclinedTours
        {
            get { return declinedTours; }
            set
            {
                if (declinedTours != value)
                {
                    declinedTours = value;
                    OnPropertyChanged(nameof(DeclinedTours));
                }
            }
        }
        
        private string selectedYear;
        public string SelectedYear
        {
            get { return selectedYear; }
            set
            {
                if (selectedYear != value)
                {
                    selectedYear = value;                    
                    OnPropertyChanged(nameof(SelectedYear));
                    LoadStatistics(SelectedYear);
                }
            }
        }

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                if (seriesCollection != value)
                {
                    seriesCollection = value;
                    OnPropertyChanged(nameof(SeriesCollection));
                }
            }
        }

        private string labels;
        public string Labels
        {
            get { return labels; }
            set
            {
                if (labels != value)
                {
                    labels = value;
                    OnPropertyChanged(nameof(Labels));
                }
            }
        }
        private Func<string, string> values;
        public Func<string, string> Values
        {
            get { return values; }
            set
            {
                if (values != value)
                {
                    values = value;
                    OnPropertyChanged(nameof(Values));
                }
            }
        }

        private SeriesCollection seriesCollection3;
        public SeriesCollection SeriesCollection3
        {
            get { return seriesCollection3; }
            set
            {
                if (seriesCollection3 != value)
                {
                    seriesCollection3 = value;
                    OnPropertyChanged(nameof(SeriesCollection3));
                }
            }
        }

        private string labels3;
        public string Labels3
        {
            get { return labels3; }
            set
            {
                if (labels3 != value)
                {
                    labels3= value;
                    OnPropertyChanged(nameof(Labels3));
                }
            }
        }
        private Func<string, string> values3;
        public Func<string, string> Values3
        {
            get { return values3; }
            set
            {
                if (values3 != value)
                {
                    values3 = value;
                    OnPropertyChanged(nameof(Values3));
                }
            }
        }
        private SeriesCollection seriesCollection2;
        public SeriesCollection SeriesCollection2
        {
            get { return seriesCollection2; }
            set
            {
                if (seriesCollection2 != value)
                {
                    seriesCollection2 = value;
                    OnPropertyChanged(nameof(SeriesCollection2));
                }
            }
        }

        private string labels2;
        public string Labels2
        {
            get { return labels2; }
            set
            {
                if (labels2 != value)
                {
                    labels2 = value;
                    OnPropertyChanged(nameof(Labels2));
                }
            }
        }
        private Func<string, string> values2;
        public Func<string, string> Values2
        {
            get { return values2; }
            set
            {
                if (values2 != value)
                {
                    values2 = value;
                    OnPropertyChanged(nameof(Values2));
                }
            }
        }
        private Func<double, string> formatter;
        public Func<double, string> Formatter
        {
            get { return formatter; }
            set
            {
                if (formatter != value)
                {
                    formatter = value;
                    OnPropertyChanged(nameof(Formatter));
                }
            }
        }

        private int numberOfCoupons;
        public int NumberOfCoupons
        {
            get { return numberOfCoupons; }
            set
            {
                if (numberOfCoupons != value)
                {
                    numberOfCoupons = value;
                    OnPropertyChanged(nameof(NumberOfCoupons));
                }
            }
        }

        private int numberOfVisitedTours;
        public int NumberOfVisitedTours
        {
            get { return numberOfVisitedTours; }
            set
            {
                if (numberOfVisitedTours != value)
                {
                    numberOfVisitedTours = value;
                    OnPropertyChanged(nameof(NumberOfVisitedTours));
                }
            }
        }
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public GuestTwoStatisticsViewModel()
        {
            LoadWindow();
            GetAllTourRequestYears();            
        }
        public void LoadWindow()
        {
            UsernameLabel = "Hello, " + LoggedUser.username + "!";
            NameAndLastname = LoggedUser.firstName + " " + LoggedUser.lastName;
            UsernameLabel2 = "@" + LoggedUser.username;
            AccountType = "Account type:  " + LoggedUser.role;
            FullName = "@" + LoggedUser.firstName + " " + LoggedUser.lastName;
            DataBaseContext context = new DataBaseContext();
            LoadData(context);
        }

        public int GetAllTourRequestsByLanguage(language lan, string selectedYear) 
        {
            int counter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (LoggedUser.id == tourRequest.guestId && lan == tourRequest.language && tourRequest.startDate.Year.ToString().Contains(yearToCompare)) 
                {
                    counter++;
                }
            }
            return counter;
        }

        public int GetAllTourRequestsByCity(string city, string selectedYear)
        {
            int counter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (LoggedUser.id == tourRequest.guestId && tourRequest.city.Equals(city) && tourRequest.startDate.Year.ToString().Contains(yearToCompare))
                {
                    counter++;
                }
            }
            return counter;
        }
        public int GetAllTourRequestsByCountry(string country, string selectedYear)
        {
            int counter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (LoggedUser.id == tourRequest.guestId && tourRequest.country.Equals(country) && tourRequest.startDate.Year.ToString().Contains(yearToCompare))
                {
                    counter++;
                }
            }
            return counter;
        }

        public void LoadStatistics(string selectedYear)
        {
            AcceptedTours = "Percentage of tours i suggested that are accepted:          " + GetAcceptedTourPercentage(selectedYear);
            AverageNumberOfPeople = "Average number of people in accepted tours:                     " + GetAverageNumberOfPeople(selectedYear);
            DeclinedTours = "Percentage of tours i suggested that are not accepted:   " + GetDeclinedTourPercentage(selectedYear);
            DrawGraphs(selectedYear);
        }
        public void DrawGraphs(string selectedYear) 
        {
            DrawLanguageGraph(selectedYear);
            DrawCityGraph(selectedYear);
            DrawCountryGraph(selectedYear);
        }
        public void DrawLanguageGraph(string selectedYear)
        {
            bool flag = false;
            foreach (language lang in Enum.GetValues(typeof(language)))
            {
                if (flag == false)
                {
                    SeriesCollection = new SeriesCollection()
                    {
                        new ColumnSeries{
                            Title = lang.ToString(),
                            DataLabels = true,
                            Values = new ChartValues<int> { GetAllTourRequestsByLanguage(lang,selectedYear)}
                        }
                    };
                }
                else
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = lang.ToString(),
                        DataLabels = true,
                        Values = new ChartValues<int> { GetAllTourRequestsByLanguage(lang, selectedYear) }
                    });
                }
                Formatter = Values => Math.Round(Values,2) + "";
                Labels = "";
                flag = true;
            }            
        }
        public void DrawCityGraph(string selectedYear)
        {
            bool flag = false;      
            List<string> cities = tourLocationService.GetAllCities();
            foreach (string city in cities)
            {

                if (flag == false)
                {
                    SeriesCollection2 = new SeriesCollection()
                    {
                        new ColumnSeries{
                            Title = city.ToString(),
                            DataLabels = true,
                            Values = new ChartValues<int> { GetAllTourRequestsByCity(city, selectedYear)}
                        }
                    };

                }
                else
                {
                    SeriesCollection2.Add(new ColumnSeries
                    {
                        Title = city.ToString(),
                        DataLabels = true,
                        Values = new ChartValues<int> { GetAllTourRequestsByCity(city, selectedYear) }
                    });
                }
                Formatter = Values => Math.Round(Values, 2) + "";
                Labels2 = "";
                flag = true;
            }
        }
        public void DrawCountryGraph(string selectedYear)
        {
            bool flag = false;
            List<string> countries = tourLocationService.GetAllCountries();
            foreach (string country in countries)
            {

                if (flag == false)
                {
                    SeriesCollection3 = new SeriesCollection()
                    {
                        new ColumnSeries{
                            Title = country.ToString(),
                            DataLabels = true,
                            Values = new ChartValues<int> { GetAllTourRequestsByCountry(country, selectedYear)}
                        }
                    };

                }
                else
                {
                    SeriesCollection3.Add(new ColumnSeries
                    {
                        Title = country.ToString(),
                        DataLabels = true,
                        Values = new ChartValues<int> { GetAllTourRequestsByCountry(country, selectedYear) }
                    });
                }
                Formatter = Values => Math.Round(Values, 2) + "";
                Labels3 = "";
                flag = true;
            }
        }

        public void GetAllTourRequestYears()
        {

            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (!YearComboBox.Contains(tourRequest.startDate.Year.ToString()))
                {                   
                    YearComboBox.Add(tourRequest.startDate.Year.ToString());
                }
            }
            YearComboBox.Add("All time");
        }

        public string GetAcceptedTourPercentage(string selectedYear)
        {
            string acceptedToursPercentage = "";
            int acceptedToursCounter = 0;
            int toursCounter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (tourRequest.guestId == LoggedUser.id && tourRequest.startDate.Year.ToString().Contains(yearToCompare))
                {
                    if (tourRequest.status == TourRequestStatus.Accepted)
                    {
                        acceptedToursCounter++;
                    }
                    toursCounter++;
                }
            }
            if (toursCounter > 0)
            {
                double percentage = Math.Round(((double)acceptedToursCounter / toursCounter) * 100, 2);
                acceptedToursPercentage += percentage.ToString() + "%";
            }
            return acceptedToursPercentage;
        }

        public string GetAverageNumberOfPeople(string selectedYear)
        {
            string averageNumberOfPeople = "";
            int numberOfPeopleInTour = 0;
            int acceptedToursCounter = 0;
            int tourCounter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (tourRequest.guestId == LoggedUser.id && tourRequest.startDate.Year.ToString().Contains(yearToCompare))
                {
                    if (tourRequest.status == TourRequestStatus.Accepted)
                    {
                        acceptedToursCounter++;
                        numberOfPeopleInTour += tourRequest.numberOfTourists;
                    }
                    tourCounter++;
                }
            }
            if (tourCounter > 0)
            {
                averageNumberOfPeople = Math.Round(((double)numberOfPeopleInTour / acceptedToursCounter), 2).ToString();
            }
            return averageNumberOfPeople;
        }

        public string GetDeclinedTourPercentage(string selectedYear)
        {
            string declinedToursPercentage = "";
            int declinedToursCounter = 0;
            int toursCounter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (tourRequest.guestId == LoggedUser.id && tourRequest.startDate.Year.ToString().Contains(yearToCompare))
                {
                    if (tourRequest.status == TourRequestStatus.OnHold || tourRequest.status == TourRequestStatus.Invalid)
                    {
                        declinedToursCounter++;
                    }
                    toursCounter++;
                }
            }
            if (toursCounter > 0)
            {
                double percentage = Math.Round(((double)declinedToursCounter / toursCounter) * 100, 2);
                declinedToursPercentage += percentage.ToString() + "%";
            }
            return declinedToursPercentage;
        }

        public void LoadData(DataBaseContext context)
        {
            foreach (Coupon coupon in context.Coupons.ToList())
            {
                if (coupon.userId == LoggedUser.id)
                {
                    NumberOfCoupons++;
                }
            }
            foreach (TourAttendance attendance in context.TourAttendances.ToList())
            {
                if (attendance.guestID == LoggedUser.id)
                {
                    NumberOfVisitedTours++;
                }
            }

        }
    }
}
