using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model.TransferModels;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using System.Windows.Controls;
using LiveCharts.Definitions.Charts;
using LiveCharts.Defaults;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationMonthlyStatisticsViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private BookingService bookingService = new(new BookingRepository());
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> YAxisLabelFormatter { get; set; }

        private string _selectedMonth;
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    monthComboBox_SelectionChanged();
                    OnPropertyChanged(nameof(SelectedMonth));
                }
            }
        }

        private string _accommodationName;
        public string AccommodationName
        {
            get { return _accommodationName; }
            set
            {
                if (_accommodationName != value)
                {
                    _accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }

        private Style _dataGridStyle;
        public Style DataGridStyle
        {
            get { return _dataGridStyle; }
            set
            {
                _dataGridStyle = value;
                OnPropertyChanged(nameof(DataGridStyle));
            }
        }

        private Style _dataGridCellStyle;
        public Style DataGridCellStyle
        {
            get { return _dataGridCellStyle; }
            set
            {
                _dataGridCellStyle = value;
                OnPropertyChanged(nameof(DataGridCellStyle));
            }
        }
        private Style _dataGridColumnHeaderStyle;
        public Style DataGridColumnHeaderStyle
        {
            get { return _dataGridColumnHeaderStyle; }
            set
            {
                _dataGridColumnHeaderStyle = value;
                OnPropertyChanged(nameof(DataGridColumnHeaderStyle));
            }
        }

        private Style _dataGridRowStyle;
        public Style DataGridRowStyle
        {
            get { return _dataGridRowStyle; }
            set
            {
                _dataGridRowStyle = value;
                OnPropertyChanged(nameof(DataGridRowStyle));
            }
        }

        private string _generateReportText;
        public string GenerateReportText
        {
            get { return _generateReportText; }
            set
            {
                _generateReportText = value;
                OnPropertyChanged(nameof(GenerateReportText));
            }
        }
        private string _chooseMonthText;
        public string ChooseMonthText
        {
            get { return _chooseMonthText; }
            set
            {
                _chooseMonthText = value;
                OnPropertyChanged(nameof(ChooseMonthText));
            }
        }
        private string _monthlyStatisticsText;
        public string MonthlyStatisticsText
        {
            get { return _monthlyStatisticsText; }
            set
            {
                _monthlyStatisticsText = value;
                OnPropertyChanged(nameof(MonthlyStatisticsText));
            }
        }

        public SeriesCollection PieSeriesCollection { get; set; }

        public ObservableCollection<AccommodationMonthlyStatisticsDTO> monthlyStatistics { get; set; } = new ObservableCollection<AccommodationMonthlyStatisticsDTO>();
        public ObservableCollection<string> monthsList { get; set; } = new ObservableCollection<string>();


        public AccommodationMonthlyStatisticsViewModel()
        {
            ShowMonthlyAccommodationStatistics();
            GetMonthList();

            DataBaseContext annualStatisticsContext = new DataBaseContext();
            MonthlyAccommodationTransfer transferedStatitic = annualStatisticsContext.AccommodationsMonthlyStatisticsTransfer.First();
            Accommodation accommodation = this.accommodationService.GetById(transferedStatitic.accommodationId);
            AccommodationName = accommodation.name;
            PieSeriesCollection = new SeriesCollection();
            DisplayPieChart(PieSeriesCollection);

            Mediator.IsCheckedChanged += OnIsCheckedChanged;
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            DataGridStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];
            DataGridCellStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];
            DataGridColumnHeaderStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];
            DataGridRowStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];

            GenerateReportText = Mediator.GetCurrentIsLanguageChecked() ? "Generisi izvestaj" : "Generate report";
            ChooseMonthText = Mediator.GetCurrentIsLanguageChecked() ? "Prikaz statistike za mesec" : "Show statistics for month";
            MonthlyStatisticsText = Mediator.GetCurrentIsLanguageChecked() ? "mesecna statistika" : "monthly statistics";
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            GenerateReportText = isChecked ? "Generisi izvestaj" : "Generate report";
            ChooseMonthText = isChecked ? "Prikaz statistike za mesec" : "Show statistics for month";
            MonthlyStatisticsText = isChecked ? "mesecna statistika" : "monthly statistics";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";

            DataGridStyle = isChecked ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];
            DataGridCellStyle = isChecked ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];
            DataGridColumnHeaderStyle = isChecked ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];
            DataGridRowStyle = isChecked ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        public void GetMonthList()
        {
            foreach(string month in months)
            {
                monthsList.Add(month);
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
            foreach(AccommodationMonthlyStatisticsDTO msdto in dataToShow)
            {
                monthlyStatistics.Add(msdto);
            }
        }

        private void monthComboBox_SelectionChanged()
        {
            BookingService bookingService = new(new BookingRepository());
            string selectedMonth = SelectedMonth;
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

            foreach(AccommodationMonthlyStatisticsDTO msdto in dataToShow)
            {
                monthlyStatistics.Clear();
                monthlyStatistics.Add(msdto);
            }

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

                BarLabels = new[] { "Number of: " };
                Formatter = value => value.ToString("N");
            }
            else
            {
                ((ColumnSeries)SeriesCollection[0]).Values[0] = numberOfBookings;
                ((ColumnSeries)SeriesCollection[1]).Values[0] = numberOfCancelations;
                ((ColumnSeries)SeriesCollection[2]).Values[0] = numberOfDelayments;
            }

            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(BarLabels));
            OnPropertyChanged(nameof(YAxisLabelFormatter));
            OnPropertyChanged(nameof(Formatter));
        }

        private void DisplayPieChart(SeriesCollection PieSeriesCollection)
        {
            var occupancyByMonth = new Dictionary<string, int>();
            List<Booking> bookings = this.bookingService.GetAllBookings();
            DataBaseContext transferedAccommodationContext = new DataBaseContext();
            MonthlyAccommodationTransfer transferedAccommodation = transferedAccommodationContext.AccommodationsMonthlyStatisticsTransfer.First();

            foreach (Booking booking in bookings)
            {
                if(booking.accommodationId == transferedAccommodation.accommodationId)
                {
                    DateTime currentDate = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                    DateTime departureDate = DateTime.ParseExact(booking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);
                    while (currentDate < departureDate)
                    {
                        string key = currentDate.ToString("MMMM");
                        if (occupancyByMonth.ContainsKey(key))
                        {
                            occupancyByMonth[key]++;
                        }
                        else
                        {
                            occupancyByMonth[key] = 1;
                        }
                        currentDate = currentDate.AddDays(1);
                    }
                }
            }

            foreach (string month in occupancyByMonth.Keys)
            {
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month);
                PieSeriesCollection.Add(new PieSeries
                {
                    Title = month,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(occupancyByMonth[month]) },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{occupancyByMonth[month]}/{daysInMonth} ({chartPoint.Participation:P})"
                });
            }
        }
    }
}
