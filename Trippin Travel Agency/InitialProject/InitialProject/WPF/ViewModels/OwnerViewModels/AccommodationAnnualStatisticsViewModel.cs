﻿using InitialProject.Context;
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

        public ViewModelCommand ShowMonthlyStatistics { get; private set; }

        public AccommodationAnnualStatisticsViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            ShowMonthlyStatistics = new ViewModelCommand(ExecuteShowMonthlyStatistics);
            ShowTransferedAccommodationsStatistics();
            GetYearList();
            ShowAccommodationsDetails();
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

            foreach(AccommodationsAnnualStatisticsDTO asdto in dataToShow)
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

                //DataContext = this;
                LoggedUser.accommodationsAnnualStatisticsView.DataContext = this;
            }
            else
            {
                ((ColumnSeries)SeriesCollection[0]).Values[0] = numberOfBookings;
                ((ColumnSeries)SeriesCollection[1]).Values[0] = numberOfCancelations;
                ((ColumnSeries)SeriesCollection[2]).Values[0] = numberOfDelayments;
            }

            BarLabels = new[] { "Number of: " };
            Formatter = value => value.ToString("N");

            //DataContext = this;
            LoggedUser.accommodationsAnnualStatisticsView.DataContext = this;
        }
    }
}
