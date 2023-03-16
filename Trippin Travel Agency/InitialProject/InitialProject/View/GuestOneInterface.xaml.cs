using InitialProject.Context;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
using Dapper;
using InitialProject.Model;
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestOneInterface.xaml
    /// </summary>
    public partial class GuestOneInterface : Window
    {   
        public AccommodationLocation location { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int guestsNumber { get; set; }
        public int daysNumber { get; set; }


        public GuestOneInterface()
        {
            InitializeComponent();
            this.Loaded += showAccommodations;
        }

        private void showAccommodations(object sender, RoutedEventArgs e)
        {   
            AccommodationService accommodationService = new AccommodationService();
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            
            this.dataGrid.ItemsSource = dataList;
        }

        private void GetByName(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byName = accommodationService.GetByName(input_name.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byName.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byName[i]));
            }
            this.dataGrid.ItemsSource = foundResults;

        }

        private void GetByCountry(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            AccommodationLocationService locationService = new AccommodationLocationService();
            List<int> byCountry = accommodationService.GetByCountry(input_country.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byCountry.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byCountry[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByCity(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            AccommodationLocationService locationService = new AccommodationLocationService();
            List<int> byCity = accommodationService.GetByCity(input_city.Text);
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byCity.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byCity[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByType(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byType = accommodationService.GetByType(input_type.Text); 
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byType.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byType[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByGuests(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byGuests = accommodationService.GetByGuestsNumber(int.Parse(input_guests.Text));
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byGuests.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byGuests[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void GetByDays(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            List<int> byDays = accommodationService.GetByMininumDays(int.Parse(input_days.Text));
            List<Accommodation> foundResults = new List<Accommodation>();
            for (int i = 0; i < byDays.Count(); i++)
            {
                foundResults.Add(accommodationService.GetById(byDays[i]));
            }
            this.dataGrid.ItemsSource = foundResults;
        }

        private void BookAccommodation_Click(object sender, RoutedEventArgs e)
        {
            // rezervacija selektovanog smestaja
            Accommodation accommodation = (Accommodation)dataGrid.SelectedItem;
            List<DateTime> dateLimits = GetDateLimits(sender, e);
            this.dataGrid.ItemsSource = dateLimits;

        }

        private List<DateTime> GetDateLimits(object sender, RoutedEventArgs e)
        {   
            DateTime startingDate = input_starting_date.SelectedDate.Value;
            DateTime endingDate = input_ending_date.SelectedDate.Value;
            List<DateTime> dateLimits = new List<DateTime>();
            dateLimits.Add(startingDate);
            dateLimits.Add(endingDate);
            return dateLimits;
        }

    }
}
