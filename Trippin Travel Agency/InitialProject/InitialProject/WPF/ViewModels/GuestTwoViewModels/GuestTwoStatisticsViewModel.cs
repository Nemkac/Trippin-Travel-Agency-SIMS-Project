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

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoStatisticsViewModel : ViewModelBase
    {

        public ObservableCollection<string> YearComboBox { get; set; } = new ObservableCollection<string>(); 

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


        public GuestTwoStatisticsViewModel() 
        {           
            LoadWindow();
            GetAllTourRequestYears();
            SeriesCollection = new SeriesCollection()
            {
                new ColumnSeries{
                 Values = new ChartValues<double> {GetAllTourRequests()}
                }
            };
            SeriesCollection.Add(new ColumnSeries
            {
                Values = new ChartValues<double> { }
            });
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; } 
        public Func<string,string> Values { get; set; }

        public void LoadWindow()
        {
            UsernameLabel = "Hello, " + LoggedUser.username + "!";
            NameAndLastname = LoggedUser.firstName + " " + LoggedUser.lastName;
            UsernameLabel2 = "@" + LoggedUser.username;
            AccountType = "Account type:  " + LoggedUser.role;

        }

        public int GetAllTourRequests() 
        {
            int counter = 0;
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (LoggedUser.id == tourRequest.guestId) 
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

    }
}
