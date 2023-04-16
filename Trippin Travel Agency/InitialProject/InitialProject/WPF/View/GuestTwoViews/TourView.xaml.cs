using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels.GuestTwoViewModels;
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

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : UserControl
    {
        public static int DetailedId { get; set; }
        private TourService tourService;
        public TourView()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.tourService = new(new TourRepository());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.languageBox.ItemsSource = Enum.GetValues(typeof(language)).Cast<language>();
            DataBaseContext context = new DataBaseContext();
            List<TourDTO> dataList = new List<TourDTO>();
            TourDTO dto = new TourDTO();

            foreach (Tour tour in context.Tours.ToList())
            {
                //dto = tourService.CreateDTO(tour);
                dataList.Add(this.tourService.CreateDTO(tour));
            }

            this.dataGrid.ItemsSource = dataList;
        }
        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();

            if (!cityName.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByCityName = this.tourService.GetAllByCity(cityName.Text);
                this.dataGrid.ItemsSource = tourDTOsByCityName;

            }
            else if (!countryName.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByCountryName = this.tourService.GetAllByCountry(countryName.Text);
                this.dataGrid.ItemsSource = tourDTOsByCountryName;
            }
            else if (!duration.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByDuration = this.tourService.GetAllByDuration(duration.Text);
                this.dataGrid.ItemsSource = tourDTOsByDuration;
            }
            else if (!touristLimit.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByTouristLimit = this.tourService.GetAllByTouristLimit(touristLimit.Text);
                this.dataGrid.ItemsSource = tourDTOsByTouristLimit;
            }
            else if (languageBox.SelectedIndex > -1)
            {
                List<TourDTO> tourDTOsByLanguage = this.tourService.GetAllByLanguage((language)languageBox.Items[languageBox.SelectedIndex]);
                languageBox.SelectedIndex = -1;
                this.dataGrid.ItemsSource = tourDTOsByLanguage;
            }
            else
            {
                List<TourDTO> dataList = GetTourDtos(this.tourService, context);
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
            TourDTO selectedTour = (TourDTO)this.dataGrid.SelectedItem;
            List<TourDTO> tourDTOs = new List<TourDTO>();
            int index = selectedTour.id;
            int numberOfGuests = Int32.Parse(NumberOfTourists.Text);
            int flag = this.tourService.Book(index, numberOfGuests);
            TourDisplayViewModel.CanExecute = false;

            if (flag == 0)
            {
                this.TextBlock.Text = "You have booked the selected tour.";
                TourBookingTransfer tourBookingTransfer = new TourBookingTransfer(
                 selectedTour.id,
                 selectedTour.name,
                 selectedTour.description,
                 selectedTour.cityLocation,
                 selectedTour.countryLocation,
                 selectedTour.keypoints,
                 selectedTour.language,
                 selectedTour.touristLimit,
                 selectedTour.startDates,
                 selectedTour.touristLimit,
                 numberOfGuests
                );
                DataBaseContext context = new DataBaseContext();
                context.tourBookingTransfers.Add(tourBookingTransfer);
                context.SaveChanges();
                TourDisplayViewModel.CanExecute = true;
            }
            else if (flag == 1)
            {
                this.TextBlock.Text = "Not enough room for desired number of tourists. Number of spots left for this tour: " + selectedTour.touristLimit;
                tourDTOs = this.tourService.GetPreviouslySelected(selectedTour.id);
                this.dataGrid.ItemsSource = tourDTOs;
                TourDisplayViewModel.CanExecute = false;
            }
            else if (flag == -1)
            {
                this.TextBlock.Text = "This tour is full. Here are some other tours in the same location.";
                tourDTOs = this.tourService.GetBookableTours(selectedTour.cityLocation, selectedTour.name);
                this.dataGrid.ItemsSource = tourDTOs;
                TourDisplayViewModel.CanExecute = false;
            }
            
        }

        private void ShowTourDetailedView(object sender, RoutedEventArgs e)
        {
            
            TourDTO? tour = this.dataGrid.SelectedItem as TourDTO;      
            DataBaseContext context = new DataBaseContext();
            DetailedTourViewTransfer detailedTourViewTransfer = new DetailedTourViewTransfer(tour.id);
            context.detailedTourViewTransfers.Add(detailedTourViewTransfer);
            context.SaveChanges();
            //DetailedTourView detailedTourView = new DetailedTourView(); OVO NISAM PROVERIO MOZDA NE RADI ZBOG OVOGA AKO NESTO NE RADI.

        }
    }
}
