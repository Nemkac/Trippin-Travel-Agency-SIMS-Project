using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using LiveCharts.Wpf;
using LiveCharts;
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
using System.Globalization;

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
            UpdateTourRequestsChart();
        }
        private void requestCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTourRequestsChart();
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
                UpdateTourRequestsChart();

            }
        }
        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTourRequestsChart();
        }

        private void FillYearComboBox()
        {
            for (int year = 2015; year <= 2023; year++)
            {
                yearComboBox.Items.Add(year.ToString());
            }
            yearComboBox.Items.Add("All time");
        }
        private Dictionary<string, int> GetTourRequestsData(string language, string country, string city, string year)
        {
            using var dbContext = new DataBaseContext();
            IQueryable<TourRequest> tourRequests = dbContext.TourRequests;

            // Filter by language if provided
            if (!string.IsNullOrEmpty(language))
            {
                int languageValue = (int)Enum.Parse(typeof(language), language);
                tourRequests = tourRequests.Where(tr => (int)tr.language == languageValue);
            }

            // Filter by country if provided
            if (!string.IsNullOrEmpty(country))
            {
                tourRequests = tourRequests.Where(tr => tr.country == country);
            }

            // Filter by city if provided
            if (!string.IsNullOrEmpty(city))
            {
                tourRequests = tourRequests.Where(tr => tr.city == city);
            }

            Dictionary<string, int> tourRequestsData;

            if (year == "All time")
            {
                // Group tour requests by year
                tourRequestsData = tourRequests
                    .GroupBy(tr => tr.startDate.Year)
                    .ToDictionary(g => g.Key.ToString(), g => g.Count());
            }
            else
            {
                // Group tour requests by month for the selected year
                int selectedYear = int.Parse(year);
                tourRequests = tourRequests.Where(tr => tr.startDate.Year == selectedYear);

                tourRequestsData = tourRequests
                    .GroupBy(tr => tr.startDate.Month)
                    .ToDictionary(g => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key), g => g.Count());
            }

            return tourRequestsData;
        }



        private void UpdateTourRequestsChart()
        {
            if (languageComboBox.SelectedItem == null && requestCountryComboBox.SelectedItem == null && requestCityComboBox.SelectedItem == null)
                return;

            string language = languageComboBox.SelectedItem?.ToString();
            string country = requestCountryComboBox.SelectedItem?.ToString();
            string city = requestCityComboBox.SelectedItem?.ToString();
            string year = yearComboBox.SelectedItem?.ToString();

            Dictionary<string, int> tourRequestsData = GetTourRequestsData(language, country, city, year);

            // Clear the existing series from the chart
            cartesianChart.Series.Clear();

            // Create a new ColumnSeries to display the tour requests data
            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = "Tour Requests",
                Values = new ChartValues<int>(tourRequestsData.Values)
            };

            // Set the labels for the X-Axis
            cartesianChart.AxisX[0].Labels = tourRequestsData.Keys.ToList();

            // Add the ColumnSeries to the chart
            cartesianChart.Series.Add(columnSeries);
        }


    }
}
