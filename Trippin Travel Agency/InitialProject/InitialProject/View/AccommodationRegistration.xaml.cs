using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Services;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistration.xaml
    /// </summary>
    public partial class AccommodationRegistration : UserControl
    {
        public AccommodationRegistration()
        {
            InitializeComponent();
            DataBaseContext countryContext = new DataBaseContext();
            List<AccommodationLocation> countryList = countryContext.LocationsOfAccommodations.ToList();
            foreach (AccommodationLocation location in countryList.ToList())
            {
                if (!countryComboBox.Items.Contains(location.country))
                {
                    countryComboBox.Items.Add(location.country);
                }
            }
        }

        private void SaveAccommodation(object sender, RoutedEventArgs e)
        {
            string name = accommodationNameTB.Text;
            string country = countryComboBox.SelectedValue.ToString();
            string city = cityComboBox.SelectedValue.ToString();
            AccommodationLocation location = AccommodationService.GetLocation(country, city);
            string guestLimitInput = guestLimitTB.Text;
            int guestLimit = int.Parse(guestLimitInput);
            string minDaysBookedInput = minDaysBookedTB.Text;
            int minDaysBooked = int.Parse(minDaysBookedInput);
            string bookingCancelPeriodInput = bookingCancelPeriodTB.Text;
            int bookingCancelPeriod = int.Parse(bookingCancelPeriodInput);
            Model.Type type = GetRadioButtonInput();

            Accommodation accommodation = new Accommodation(name, location, guestLimit, minDaysBooked, bookingCancelPeriod, type);
            AccommodationService.Save(accommodation);
            clearInputs();
        }

        private Model.Type GetRadioButtonInput()
        {
            Model.Type type;
            if (houseRadioButton.IsChecked == true)
            {
                type = 0;
            }
            else if (apartmentRadioButton.IsChecked == true)
            {
                type = (Model.Type)1;
            }
            else
            {
                type = (Model.Type)2;
            }

            return type;
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityComboBox.Items.Clear();
            string selectedCountry = countryComboBox.SelectedValue.ToString();

            DataBaseContext cityContext = new DataBaseContext();
            List<AccommodationLocation> cityList = cityContext.LocationsOfAccommodations.ToList();

            foreach (AccommodationLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!cityComboBox.Items.Contains(location.city))
                    {
                        cityComboBox.Items.Add(location.city);
                    }
                }
            }
        }

        private void clearInputs()
        {
            accommodationNameTB.Clear();
            guestLimitTB.Clear();
            minDaysBookedTB.Clear();
            bookingCancelPeriodTB.Clear();
            houseRadioButton.IsChecked = false;
            hutRadioButton.IsChecked = false;
            apartmentRadioButton.IsChecked = false;
        }
    }
}
