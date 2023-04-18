using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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
using LiveCharts.Definitions.Charts;

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AccommodationsMonthlyStatisticsView.xaml
    /// </summary>
    public partial class AccommodationsMonthlyStatisticsView : UserControl
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> YAxisLabelFormatter { get; set; }
        public AccommodationsMonthlyStatisticsView()
        {
            InitializeComponent();
            ShowMonthlyAccommodationStatistics();
            DataBaseContext annualStatisticsContext = new DataBaseContext();
            MonthlyAccommodationTransfer transferedStatitic = annualStatisticsContext.AccommodationsMonthlyStatisticsTransfer.First();
            Accommodation accommodation = this.accommodationService.GetById(transferedStatitic.accommodationId);
            accommodationNameTextBlock.Text = accommodation.name;
            //this.SeriesCollection = new SeriesCollection();
            foreach(string month in months)
            {
                monthComboBox.Items.Add(month);
            }
        }

        private void ShowMonthlyAccommodationStatistics()
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
            List<AccommodationMonthlyStatisticsDTO> dataToShow = new List<AccommodationMonthlyStatisticsDTO>();

            foreach (string month in this.months)
            {
                int numberOfBookings = 0;
                int numberOfCancelations = 0;
                int numberOfDelayments = 0;

                foreach (Booking booking in bookings)
                {
                    DateTime arrivalDate = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                    int arrivalMonth = arrivalDate.Month;
                    if (month == CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(arrivalMonth) && booking.accommodationId == transferedAccommodation.accommodationId)
                    {
                        numberOfBookings++;
                    }
                }

                foreach (CanceledBooking canceledBooking in canceledBookings)
                {
                    if (month == CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(canceledBooking.plannedArrival.Month) && canceledBooking.accommodationId == transferedAccommodation.accommodationId)
                    {
                        numberOfCancelations++;
                    }
                }

                foreach (DelayedBookings delayedBooking in delayedBookings)
                {
                    if (month == CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(delayedBooking.previousArrival.Month) && delayedBooking.accommodationId == transferedAccommodation.accommodationId)
                    {
                        numberOfDelayments++;
                    }
                }

                AccommodationMonthlyStatisticsDTO dto = new AccommodationMonthlyStatisticsDTO(month, numberOfBookings, numberOfCancelations, numberOfDelayments);
                dataToShow.Add(dto);
            }
            monthlyStatisticsDataGrid.ItemsSource = dataToShow;
        }

        private void monthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingService bookingService = new(new BookingRepository());
            string selectedMonth = (string)monthComboBox.SelectedItem;
            int numberOfBookings = 0;
            int numberOfCancelations = 0;
            int numberOfDelayments = 0;

            DataBaseContext transferContext = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            List<Booking> bookings = bookingService.GetAllBookings();
            List<CanceledBooking> canceledBookings = bookingService.GetAllCanceledBookings();
            List<DelayedBookings> delayedBookings = bookingService.GetAllDelayedBookings();
            List<AccommodationMonthlyStatisticsDTO> dataToShow = new List<AccommodationMonthlyStatisticsDTO>();

            foreach (DelayedBookings delayedBooking in delayedBookings)
            {
                if (selectedMonth == CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(delayedBooking.previousArrival.Month) && delayedBooking.accommodationId == transferedAccommodation.accommodationId)
                {
                   numberOfDelayments++;
                }
            }
            

            foreach (CanceledBooking canceledBooking in canceledBookings)
            {
                if (selectedMonth == CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(canceledBooking.plannedArrival.Month) && canceledBooking.accommodationId == transferedAccommodation.accommodationId)
                {
                    numberOfCancelations++;
                }
            }

            
            foreach (Booking booking in bookings)
            {
                DateTime arrivalDate = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                int arrivalMonth = arrivalDate.Month;
                if (selectedMonth == CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(arrivalMonth) && booking.accommodationId == transferedAccommodation.accommodationId)
                {
                    numberOfBookings++;
                }
            }

            AccommodationMonthlyStatisticsDTO dto = new AccommodationMonthlyStatisticsDTO(selectedMonth, numberOfBookings, numberOfCancelations, numberOfDelayments);
            dataToShow.Add(dto);
            monthlyStatisticsDataGrid.ItemsSource = dataToShow;

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

            BarLabels = new[] { selectedMonth };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
