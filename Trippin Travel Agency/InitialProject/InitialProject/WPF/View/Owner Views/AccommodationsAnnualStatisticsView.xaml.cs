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
using InitialProject.WPF.ViewModels.OwnerViewModels;

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AccommodationsAnnualStatisticsView.xaml
    /// </summary>
    public partial class AccommodationsAnnualStatisticsView : UserControl
    {
        //private BookingService bookingService;

        /*public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> YAxisLabelFormatter { get; set; }*/

        public AccommodationsAnnualStatisticsView()
        {
            InitializeComponent();
            this.DataContext = new AccommodationAnnualStatisticsViewModel();
            LoggedUser.accommodationsAnnualStatisticsView = this;
            //this.bookingService = new(new BookingRepository());
        }

        /*private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingService bookingService = new(new BookingRepository());

            if (yearComboBox.SelectedItem.ToString() != "All Time")
            {
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

                numberOfBookings = bookingService.CountAccommodationsBookingsForYear(selectedYear, numberOfBookings, transferedAccommodation, bookings);

                numberOfCancelations = bookingService.CountAccommodationsCanceledBookingsForYear(selectedYear, numberOfCancelations, transferedAccommodation, canceledBookings);

                numberOfDelayments = bookingService.CountAccommodationsDelayedBookingsForYear(selectedYear, numberOfDelayments, transferedAccommodation, delayedBookings);

                AccommodationsAnnualStatisticsDTO dto = new AccommodationsAnnualStatisticsDTO(selectedYear, numberOfBookings, numberOfCancelations, numberOfDelayments);
                dataToShow.Add(dto);
                annualStatisticsDataGrid.ItemsSource = dataToShow;

                YAxisLabelFormatter = value => value.ToString("#.##");

                if (SeriesCollection == null)
                {
                    SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title="Bookings",
                        Values = new ChartValues<int>{numberOfBookings}
                    },
                    new ColumnSeries
                    {
                        Title = "Cancelations",
                        Values = new ChartValues<int> { numberOfCancelations }
                    },
                    new ColumnSeries
                    {
                        Title = "Delayments",
                        Values = new ChartValues<int> { numberOfDelayments }
                    },
                };

                    DataContext = this;
                }
                else
                {
                    ((ColumnSeries)SeriesCollection[0]).Values[0] = numberOfBookings;
                    ((ColumnSeries)SeriesCollection[1]).Values[0] = numberOfCancelations;
                    ((ColumnSeries)SeriesCollection[2]).Values[0] = numberOfDelayments;
                }

                BarLabels = new[] { "Number of: " };
                Formatter = value => value.ToString("N");

                DataContext = this;

            }
            else
            {
                //ShowTransferedAccommodationsStatistics();
                return;
            }
        }*/
    }
}
