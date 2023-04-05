using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : UserControl
    {
        public static int DetailedId { get; set; }
        public TourView()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.languageBox.ItemsSource = Enum.GetValues(typeof(language)).Cast<language>();
            TourService tourService = new TourService();
            DataBaseContext context = new DataBaseContext();
            List<TourDTO> dataList = new List<TourDTO>();
            TourDTO dto = new TourDTO();

            foreach (Tour tour in context.Tours.ToList())
            {
                //dto = tourService.CreateDTO(tour);
                dataList.Add(tourService.CreateDTO(tour));
            }

            this.dataGrid.ItemsSource = dataList;
        }
        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            DataBaseContext context = new DataBaseContext();

            if (!cityName.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByCityName = tourService.GetByCity(cityName.Text);
                this.dataGrid.ItemsSource = tourDTOsByCityName;

            }
            else if (!countryName.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByCountryName = tourService.GetAllByCountry(countryName.Text);
                this.dataGrid.ItemsSource = tourDTOsByCountryName;
            }
            else if (!duration.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByDuration = tourService.GetByDuration(duration.Text);
                this.dataGrid.ItemsSource = tourDTOsByDuration;
            }
            else if (!touristLimit.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByTouristLimit = tourService.GetByTouristLimit(touristLimit.Text);
                this.dataGrid.ItemsSource = tourDTOsByTouristLimit;
            }
            else if (languageBox.SelectedIndex > -1)
            {
                List<TourDTO> tourDTOsByLanguage = tourService.GetByLanguage((language)languageBox.Items[languageBox.SelectedIndex]);
                languageBox.SelectedIndex = -1;
                this.dataGrid.ItemsSource = tourDTOsByLanguage;
            }
            else
            {
                List<TourDTO> dataList = GetTourDtos(tourService, context);
                this.dataGrid.ItemsSource = dataList;
            }
        }

        private static List<TourDTO> GetTourDtos(TourService tourService, DataBaseContext context)
        {
            List<TourDTO> dataList = new List<TourDTO>();
            TourDTO dto = new TourDTO();

            foreach (Tour tour in context.Tours.ToList())
            {
                dto = tourService.CreateDTO(tour);
                dataList.Add(dto);
            }

            return dataList;
        }

        private void ReserveTour(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            TourDTO selectedTour = (TourDTO)this.dataGrid.SelectedItem;
            List<TourDTO> tourDTOs = new List<TourDTO>();
            int index = selectedTour.id;
            int numberOfGuests = Int32.Parse(NumberOfTourists.Text);
            int flag = tourService.Book(index, numberOfGuests);

            if (flag == 0)
            {
                this.TextBlock.Text = "You have booked the selected tour.";
            }
            else if (flag == 1)
            {
                this.TextBlock.Text = "Not enough room for desired number of tourists. Number of spots left for this tour: " + selectedTour.touristLimit;
                tourDTOs = tourService.GetPreviouslySelected(selectedTour.id);
                this.dataGrid.ItemsSource = tourDTOs;
            }
            else if (flag == -1)
            {
                this.TextBlock.Text = "This tour is full. Here are some other tours in the same location.";
                tourDTOs = tourService.GetBookableTours(selectedTour.cityLocation, selectedTour.name);
                this.dataGrid.ItemsSource = tourDTOs;
            }
        }

        private void ShowTourDetailedView(object sender, RoutedEventArgs e)
        {
            DetailedTourView detailedTourView = new DetailedTourView();
            TourDTO? tour = this.dataGrid.SelectedItem as TourDTO;
            DetailedId = tour.id;
        }
    }
}
