using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for GuestTwoStatisticsView.xaml
    /// </summary>
    public partial class GuestTwoStatisticsView : UserControl
    {
        public GuestTwoStatisticsView()
        {
            InitializeComponent();
            LoadWindow();
            GetAllTourRequestYears();
        }

        public void LoadWindow() {
            this.UsernameLabel.Content = "Hello, " + LoggedUser.username + "!";
            this.NameLastName.Content = LoggedUser.firstName + " "+ LoggedUser.lastName;
            this.UsernameLabel2.Content = "@" + LoggedUser.username;
            this.AccountTypeLabel.Content = "Account type:  " + LoggedUser.role;

        }

        public void LoadStatistics(string selectedYear) 
        {
            this.AcceptedToursLabel.Content =         "Percentage of tours i suggested that are accepted:          " + GetAcceptedTourPercentage(selectedYear);
            this.AverageNumberOfPeopleLabel.Content = "Average number of people in accepted tours:                     " + GetAverageNumberOfPeople(selectedYear);
            this.DeclinedToursLabel.Content =         "Percentage of tours i suggested that are not accepted:   " + GetDeclinedTourPercentage(selectedYear);
        }

        public void ComboBoxSelectedYear(object sender, SelectionChangedEventArgs e) 
        {
            LoadStatistics(this.YearsComboBox.SelectedItem.ToString());
        }


        public string GetAcceptedTourPercentage(string selectedYear)
        {
            string acceptedToursPercentage = "";
            int acceptedToursCounter = 0;
            int toursCounter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time")) {
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
                double percentage = ((double)acceptedToursCounter / toursCounter) * 100;
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
                averageNumberOfPeople = ((double)numberOfPeopleInTour / acceptedToursCounter).ToString();
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
                double percentage = ((double)declinedToursCounter / toursCounter) * 100;
                declinedToursPercentage += percentage.ToString() + "%";
            }
            return declinedToursPercentage;
        }

        public void GetAllTourRequestYears() 
        {
            DataBaseContext context = new DataBaseContext();
            foreach (TourRequest tourRequest in context.TourRequests.ToList()) 
            {
                if (!this.YearsComboBox.Items.Contains(tourRequest.startDate.Year))
                {
                    this.YearsComboBox.Items.Add(tourRequest.startDate.Year);
                }
            }
            this.YearsComboBox.Items.Add("All time");
        }
    }
}
