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
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationAnnualStatisticsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;
        private AccommodationService accommodationService = new(new AccommodationRepository());
        
        public ObservableCollection<int> years { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<AccommodationsAnnualStatisticsDTO> accommodationsAnnualStatisticsDTOs { get; set; } = new ObservableCollection<AccommodationsAnnualStatisticsDTO>();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> YAxisLabelFormatter { get; set; }
        public AccommodationsAnnualStatisticsDTO SelectedAnnualStatistics { get; set; }

        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    yearComboBox_SelectionChanged();
                    OnPropertyChanged(nameof(SelectedYear));
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

        private string _accommodationNameTextBlock;
        public string AccommodationNameTextBlock
        {
            get { return _accommodationNameTextBlock; }
            set
            {
                if (_accommodationNameTextBlock != value)
                {
                    _accommodationNameTextBlock = value;
                    OnPropertyChanged(nameof(AccommodationNameTextBlock));
                }
            }
        }

        private string _accommodationLocation;
        public string AccommodationLocation
        {
            get { return _accommodationLocation; }
            set
            {
                if (_accommodationLocation != value)
                {
                    _accommodationLocation = value;
                    OnPropertyChanged(nameof(AccommodationLocation));
                }
            }
        }

        private int _maxNumOfGuests;
        public int MaxNumOfGuests
        {
            get { return _maxNumOfGuests; }
            set
            {
                if (_maxNumOfGuests != value)
                {
                    _maxNumOfGuests = value;
                    OnPropertyChanged(nameof(MaxNumOfGuests));
                }
            }
        }

        private string _accommodationType;
        public string AccommodationType
        {
            get { return _accommodationType; }
            set
            {
                if (_accommodationType != value)
                {
                    _accommodationType = value;
                    OnPropertyChanged(nameof(AccommodationType));
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

        private string _contentHintColor;
        public string ContentHintColor
        {
            get { return _contentHintColor; }
            set
            {
                _contentHintColor = value;
                OnPropertyChanged(nameof(ContentHintColor));
            }
        }

        private string _detailsButtonColor;
        public string DetailsButtonColor
        {
            get { return _detailsButtonColor; }
            set
            {
                _detailsButtonColor = value;
                OnPropertyChanged(nameof(DetailsButtonColor));
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

        public ViewModelCommand ShowMonthlyStatistics { get; private set; }
        public ViewModelCommand GenerateAnnualReport { get; private set; }

        public AccommodationAnnualStatisticsViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            ShowMonthlyStatistics = new ViewModelCommand(ExecuteShowMonthlyStatistics);
            GenerateAnnualReport = new ViewModelCommand(ExecuteGenerateAnnualReport);
            ShowTransferedAccommodationsStatistics();
            GetYearList();
            ShowAccommodationsDetails();

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";
            DetailsButtonColor = Mediator.GetCurrentIsChecked() ? "#718093" : "#2f3640";

            DataGridStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
            DetailsButtonColor = isChecked ? "#718093" : "#2f3640";

            DataGridStyle = isChecked ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = isChecked ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = isChecked ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = isChecked ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }


        private void ExecuteGenerateAnnualReport(object obj)
        {
            _mainViewModel.selectedYearForAnnualReport = SelectedYear;
            _mainViewModel.GenerateAnnualReport(null);
        }

        private void ExecuteShowMonthlyStatistics(object obj)
        {
            AccommodationsAnnualStatisticsDTO? selectedAccommodation = SelectedAnnualStatistics;
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
            _mainViewModel.ExecuteShowMonthlyStatistics(null);
        }

        public void GetYearList()
        {
            for (int year = 2015; year <= 2023; year++)
            {
                years.Add(year);
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
            foreach(AccommodationsAnnualStatisticsDTO asDto in dataToShow)
            {
                accommodationsAnnualStatisticsDTOs.Add(asDto);
            }
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

        public void ShowAccommodationsDetails()
        {
            DataBaseContext transferContext = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            AccommodationName = transferedAccommodation.accommodationName;
            AccommodationLocation = transferedAccommodation.location;
            MaxNumOfGuests = transferedAccommodation.maxNumOfGuests;
            Accommodation accommodation = this.accommodationService.GetById(transferedAccommodation.accommodationId);
            AccommodationType = accommodation.type.ToString();
            AccommodationNameTextBlock = transferedAccommodation.accommodationName;
        }

        private void yearComboBox_SelectionChanged()
        {
            BookingService bookingService = new(new BookingRepository());

            int selectedYear = (int)SelectedYear;
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

            foreach (AccommodationsAnnualStatisticsDTO asdto in dataToShow)
            {
                accommodationsAnnualStatisticsDTOs.Clear();
                accommodationsAnnualStatisticsDTOs.Add(dto);
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
    }
}
