using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
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

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            GuestRateService guestRateService = new GuestRateService(); 
            this.UsernameTextBlock.Text = LoggedUser.username;
            this.FirstNameTextBlock.Text = LoggedUser.firstName;
            this.LastNameTextBlock.Text = LoggedUser.lastName;
            this.EmailTextBlock.Text = LoggedUser.email;

            List<AccommodationRate> availableRates = GetAvailableRates();

            decimal totalRating = guestRateService.CalculateTotalRating(availableRates);
            TotalRateTextBlock.Text = totalRating.ToString();

            DisplayOwnerType(totalRating);
        }

        private void DisplayOwnerType(decimal totalRating)
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            int numOfRates = accommodationRates.Count;

            if (numOfRates >= 50)
            {
                if (totalRating < (decimal)9.5)
                {
                    OwnerTypeTextBlock.Text = "Owner";
                }
                else
                {
                    OwnerTypeTextBlock.Text = "Super - Owner";
                }
            }
            else
            {
                OwnerTypeTextBlock.Text = "Owner";
            }
        }

        private static List<AccommodationRate> GetAvailableRates()
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            DataBaseContext guestRateContext = new DataBaseContext();
            GuestRateService guestRateService = new GuestRateService();

            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            List<GuestRate> guestRates = guestRateContext.GuestRate.ToList();

            List<AccommodationRate> ratesForDisplay = new List<AccommodationRate>();
            guestRateService.FormDisplayableRates(accommodationRates, guestRates, ratesForDisplay);

            return ratesForDisplay;
        }
    }
}
