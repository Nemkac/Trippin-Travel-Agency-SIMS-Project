using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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
using System.Windows.Shapes;
using System.Xml;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationInterface.xaml
    /// </summary>
    public partial class AccommodationRegistrationInterface : Window
    {
        public AccommodationRegistrationInterface()
        {
            InitializeComponent();
            DataBaseContext countryContext = new DataBaseContext();
            List<AccommodationLocation> countryList  = countryContext.LocationsOfAccommodations.ToList();
            foreach(AccommodationLocation location in countryList.ToList())
            {
                if (!countryComboBox.Items.Contains(location.Country))
                {
                    countryComboBox.Items.Add(location.Country);
                }    
            }
        }

        private void SaveAccommodation(object sender, RoutedEventArgs e)
        {
            string name = accommodationNameTB.Text;
            string country = countryComboBox.SelectedValue.ToString();
            string city = cityComboBox.SelectedValue.ToString();
            AccommodationLocation location = AccommodationService.findLocation(country, city);
            string guestLimitInput = guestLimitTB.Text;
            int guestLimit = int.Parse(guestLimitInput);
            string minDaysBookedInput = minDaysBookedTB.Text;
            int minDaysBooked = int.Parse(minDaysBookedInput);
            string bookingCancelPeriodInput = bookingCancelPeriodTB.Text;
            int bookingCancelPeriod = int.Parse(bookingCancelPeriodInput);

            Model.Type type;
            if(houseRadioButton.IsChecked == true)
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

            Accommodation accommodation = new Accommodation(name, location, guestLimit, minDaysBooked, bookingCancelPeriod, type);
            AccommodationService.Save(accommodation);
            clearInputs();
            OwnerInterface ownerInterface = new OwnerInterface();
            ownerInterface.Show();
            this.Close();
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityComboBox.Items.Clear();
            string selectedCountry = countryComboBox.SelectedValue.ToString();

            DataBaseContext cityContext = new DataBaseContext();
            List<AccommodationLocation> cityList = cityContext.LocationsOfAccommodations.ToList();

            foreach(AccommodationLocation location in  cityList.ToList()) 
            { 
                if(location.Country.ToString() == selectedCountry)
                {
                    if (!cityComboBox.Items.Contains(location.City))
                    {
                        cityComboBox.Items.Add(location.City);
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
