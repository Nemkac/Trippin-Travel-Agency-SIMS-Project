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

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for CreateYourOwnTourView.xaml
    /// </summary>
    public partial class CreateYourOwnTourView : UserControl
    {
        private TourLocationService tourLocationService;
        public CreateYourOwnTourView()
        {
            InitializeComponent();
            this.tourLocationService = new(new TourLocationRepository());
            LoadInputs();
        }

        private void LoadInputs() {
            List<TourLocation> tourLocations = this.tourLocationService.GetAllTourLocations();
            List<string> countries = new List<string>();
            foreach (TourLocation location in tourLocations)
            {
                if (!CountryComboBox.Items.Contains(location.country))
                {
                    CountryComboBox.Items.Add(location.country);
                }
            }
            this.LanguageComboBox.ItemsSource = Enum.GetValues(typeof(language)).Cast<language>();
            
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CityComboBox.Items.Clear();
            string selectedCountry = CountryComboBox.SelectedValue.ToString();
            GetCitiesByCountry(selectedCountry);
        }

        private void GetCitiesByCountry(string selectedCountry)
        {
            DataBaseContext cityContext = new DataBaseContext();
            List<TourLocation> cityList = cityContext.TourLocation.ToList();

            foreach (TourLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!CityComboBox.Items.Contains(location.city))
                    {
                        CityComboBox.Items.Add(location.city);
                    }
                }
            }
        }
        private void CreateRegularTour(object sender, RoutedEventArgs e)
        {
            if (CountryComboBox.SelectedItem != null
                && CityComboBox.SelectedItem != null
                && StartDate.SelectedDate != null
                && EndDate.SelectedDate != null
                && GuestNumberInput.Value != null
                && LanguageComboBox.SelectedItem != null
                && DescriptionTextBox.Text != null) {

                if (StartDate.SelectedDate < EndDate.SelectedDate)
                {
                    TourRequest tourRequest = new TourRequest((string)CityComboBox.SelectedItem,
                                                              (string)CountryComboBox.SelectedItem,
                                                              (int)GuestNumberInput.Value,
                                                              (language)LanguageComboBox.SelectedItem,
                                                              (DateTime)StartDate.SelectedDate,
                                                              (DateTime)EndDate.SelectedDate,
                                                              (string)DescriptionTextBox.Text,
                                                              LoggedUser.id);
                    DataBaseContext context = new DataBaseContext();

                    bool cityFlag = false;
                    bool countryFlag = false;
                    bool languageFlag = false;

                    foreach (Tour tour in context.Tours.ToList()) 
                    {
                       TourLocation location =  this.tourLocationService.GetById(tour.location);
                        if ((string)CityComboBox.SelectedItem == location.city) 
                        {
                            cityFlag = true;
                        }

                        if ((string)CountryComboBox.SelectedItem == location.country)
                        {
                            countryFlag = true;
                        }
                        if ((language)LanguageComboBox.SelectedItem == tour.language) 
                        {
                            languageFlag = true;
                        }                        
                    }
                    if (!cityFlag) 
                    {
                        context.UnfulfilledTourCities.Add(new(LoggedUser.id,(string)CityComboBox.SelectedItem));
                    }
                    if (!countryFlag)
                    {
                        context.unfulfilledTourCountries.Add(new(LoggedUser.id, (string)CountryComboBox.SelectedItem));
                    }
                    if (!languageFlag)
                    {
                        context.UnfulfilledTourLanguages.Add(new(LoggedUser.id, (language)LanguageComboBox.SelectedItem));
                    }
                    context.TourRequests.Add(tourRequest);
                    context.SaveChanges();
                }
                else {
                    MessageBox.Show("Invalid dates!");
                }
            }
        }
    }
}
