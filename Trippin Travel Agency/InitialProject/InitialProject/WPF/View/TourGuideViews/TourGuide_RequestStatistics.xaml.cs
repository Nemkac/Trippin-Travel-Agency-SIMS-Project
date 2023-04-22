using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
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

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_RequestStatistics : UserControl
    {
        private TourService tourService;
        public TourGuide_RequestStatistics()
        {
            InitializeComponent();
            this.tourService = new(new TourRepository());
            FillCountryComboBox();
            FillYearComboBox();
        }
        private void FillCountryComboBox()
        {
            DataBaseContext countryToursContext = new DataBaseContext();

            List<TourLocation> countryList = countryToursContext.TourLocation.ToList();
            foreach (TourLocation location in countryList.ToList())
            {
                if (!requestCountryComboBox.Items.Contains(location.country))
                {
                    requestCountryComboBox.Items.Add(location.country);
                }
            }
        }
        private void requestCountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (requestCountryComboBox.SelectedItem != null)
            {
                languageComboBox.SelectedItem = null;
                requestCityComboBox.SelectedItem = null;
                requestCityComboBox.Items.Clear();
                string selectedCountry = requestCountryComboBox.SelectedValue.ToString();
                GetCitiesByCountry(selectedCountry);
            }
        }
        private void GetCitiesByCountry(string selectedCountry)
        {
            DataBaseContext cityContext = new DataBaseContext();
            List<TourLocation> cityList = cityContext.TourLocation.ToList();

            foreach (TourLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!requestCityComboBox.Items.Contains(location.city))
                    {
                        requestCityComboBox.Items.Add(location.city);
                    }
                }
            }
        }
        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (languageComboBox.SelectedItem != null)
            {
                requestCountryComboBox.SelectedItem = null;
                requestCityComboBox.SelectedItem = null;
                requestCityComboBox.Items.Clear();
            }
        }
        private void FillYearComboBox()
        {
            for (int year = 2015; year <= 2023; year++)
            {
                yearComboBox.Items.Add(year.ToString());
            }
            yearComboBox.Items.Add("All time");
        }

    }
}
