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
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            this.dataGrid.ItemsSource = dataList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationService accommodationService = new AccommodationService();
            //List<int> byName = accommodationService.GetByName(input_name.Text);
            int byName = accommodationService.GetByName(input_name.Text);
            /*List<int> byCity = accommodationService.GetByCity(input_country.Text);
            List<int> byType = accommodationService.GetByCountry(input_city.Text);
            List<int> byGuests = accommodationService.GetByGuestsNumber(int.Parse(input_guests.Text));*/
            //debuger.Text = "name: " + acc.name + " country: " + acc.location.Country + " city: " + acc.location.City + " type: " + acc.type + " guests:" + acc.guestLimit + " days: " + acc.minDaysBooked;
            debuger.Text = byName.ToString();
        }
    }
}
