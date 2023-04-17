using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AccommodationsAnnualStatisticsView.xaml
    /// </summary>
    public partial class AccommodationsAnnualStatisticsView : UserControl
    {
        private BookingService bookingService;
        private AnnualAccommodationTransfer transferedAccommodation;
        private AccommodationService accommodationService = new(new AccommodationRepository());

        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        
        public AccommodationsAnnualStatisticsView()
        {
            InitializeComponent();
            FillYearComboBox();
            ShowTransferedAccommodationsStatistics();
            ShowAccommodationsDetails();
            DataBaseContext transferContext = new DataBaseContext();
            this.transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            this.accommodationNameTextBlock.Text = transferedAccommodation.accommodationName;
            this.bookingService = new(new BookingRepository());
        }

        private void FillYearComboBox()
        {
            for (int year = 2015; year <= 2023; year++)
            {
                yearComboBox.Items.Add(year);
            }
        }

        private void ShowTransferedAccommodationsStatistics()
        {
            BookingService bookingService = new(new BookingRepository());

            List<int> yearList = new List<int>();
            for (int year = 2023; year >= 2015; year -= 1)
            {
                yearList.Add(year);
            }

            DataBaseContext transferContext = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            List<Booking> bookings = bookingService.GetAllBookings();
            List<CanceledBooking> canceledBookings = bookingService.GetAllCanceledBookings();
            List<DelayedBookings> delayedBookings = bookingService.GetAllDelayedBookings();
            List<AccommodationsAnnualStatisticsDTO> dataToShow = new List<AccommodationsAnnualStatisticsDTO>();

            GetStatisticsForYearRange(yearList, transferedAccommodation, bookings, canceledBookings, delayedBookings, dataToShow);
            annualStatisticsDataGrid.ItemsSource = dataToShow;
        }

        private void GetStatisticsForYearRange(List<int> yearList, AnnualAccommodationTransfer transferedAccommodation, List<Booking> bookings, List<CanceledBooking> canceledBookings, List<DelayedBookings> delayedBookings, List<AccommodationsAnnualStatisticsDTO> dataToShow)
        {
            BookingService bookingService = new(new BookingRepository());
            foreach (int year in yearList)
            {
                int numberOfBookings = 0;
                int numberOfCancelations = 0;
                int numberOfDelayments = 0;
                numberOfBookings = bookingService.GetNumberOfBookingsInYearRange(transferedAccommodation, bookings, year, numberOfBookings);

                numberOfCancelations = bookingService.GetNumberOfCanceledBookingsInYearRange(transferedAccommodation, canceledBookings, year, numberOfCancelations);

                numberOfDelayments = bookingService.GetNumberOfDelayedBookingsInYearRange(transferedAccommodation, delayedBookings, year, numberOfDelayments);

                AccommodationsAnnualStatisticsDTO dto = new AccommodationsAnnualStatisticsDTO(year, numberOfBookings, numberOfCancelations, numberOfDelayments);
                dataToShow.Add(dto);
            }
        }

        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingService bookingService = new(new BookingRepository());
            int selectedYear = (int)yearComboBox.SelectedItem;
            int numberOfBookings = 0;
            int numberOfCancelations = 0;
            int numberOfDelayments = 0;

            DataBaseContext transferContext = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            List<Booking> bookings = bookingService.GetAllBookings();
            List<CanceledBooking> canceledBookings = bookingService.GetAllCanceledBookings();
            List<DelayedBookings> delayedBookings = bookingService.GetAllDelayedBookings();
            List<AccommodationsAnnualStatisticsDTO> dataToShow = new List<AccommodationsAnnualStatisticsDTO>();

            numberOfBookings = bookingService.CountAccommodationsBookingsPerYear(selectedYear, numberOfBookings, transferedAccommodation, bookings);

            numberOfCancelations = bookingService.CountAccommodationsCanceledBookingsForYear(selectedYear, numberOfCancelations, transferedAccommodation, canceledBookings);

            numberOfDelayments = bookingService.CountAccommodationsDelayedBookingsForYear(selectedYear, numberOfDelayments, transferedAccommodation, delayedBookings);

            AccommodationsAnnualStatisticsDTO dto = new AccommodationsAnnualStatisticsDTO(selectedYear, numberOfBookings, numberOfCancelations, numberOfDelayments);
            dataToShow.Add(dto);
            annualStatisticsDataGrid.ItemsSource = dataToShow;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Bookings",
                    Values = new ChartValues<int>{numberOfBookings}
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Cancelations",
                Values = new ChartValues<int> {numberOfCancelations}
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Delayments",
                Values = new ChartValues<int> { numberOfDelayments }
            });

            BarLabels = new[] { "Number of:" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public void ShowAccommodationsDetails()
        {
            DataBaseContext transferContext = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            accommodationDetailsNameTextBlock.Text = transferedAccommodation.accommodationName;
            accommodationDetailsLocationTextBlock.Text = transferedAccommodation.location;
            accommodationDetailsGuestLimitTextBlock.Text = transferedAccommodation.maxNumOfGuests.ToString();
            Accommodation accommodation = this.accommodationService.GetById(transferedAccommodation.accommodationId);
            accommodationDetailsTypeTextBlock.Text = accommodation.type.ToString();
        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {
            AccommodationsAnnualStatisticsDTO? selectedAccommodation = annualStatisticsDataGrid.SelectedItem as AccommodationsAnnualStatisticsDTO;
            DataBaseContext monthlyTransferContext = new DataBaseContext();
            DataBaseContext transferContext = new DataBaseContext();
            DataBaseContext transferedAnnualAccommodation = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = transferedAnnualAccommodation.AccommodationAnnualStatisticsTransfer.First();

            var transfers = transferContext.AccommodationsMonthlyStatisticsTransfer.ToList();
            transferContext.AccommodationsMonthlyStatisticsTransfer.RemoveRange(transfers);
            transferContext.SaveChanges();

            MonthlyAccommodationTransfer monthlyAccommodationTransfer = new MonthlyAccommodationTransfer(selectedAccommodation.year, transferedAccommodation.accommodationId);
            monthlyTransferContext.AccommodationsMonthlyStatisticsTransfer.Add(monthlyAccommodationTransfer);
            monthlyTransferContext.SaveChanges();
            MessageBox.Show(monthlyAccommodationTransfer.accommodationId.ToString());
        }
    }
}
