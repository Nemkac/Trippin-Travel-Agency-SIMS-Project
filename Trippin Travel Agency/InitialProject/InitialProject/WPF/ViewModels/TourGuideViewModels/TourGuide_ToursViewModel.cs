using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_ToursViewModel : ViewModelBase
    {
        public ViewModelCommand ShowDashboardCommand { get; private set; }
        public ViewModelCommand CreateTourCommand { get; private set; }
        public ViewModelCommand ToursTodayCommand { get; private set; }
        public ViewModelCommand FutureToursCommand { get; private set; }
        public ViewModelCommand ShowFinishedToursCommand { get; private set; }
        public ViewModelCommand TourStatisticsCommand { get; private set; }
        public ObservableCollection<string> Years { get; private set; } = new ObservableCollection<string>();
        public string SelectedYear { get; set; }
        public ViewModelCommand ShowTourStatisticsCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
            CreateTourCommand = new ViewModelCommand(CreateTour);
            ToursTodayCommand = new ViewModelCommand(ToursToday);
            FutureToursCommand = new ViewModelCommand(FutureTours);
            ShowFinishedToursCommand = new ViewModelCommand(FinishedTours);
            TourStatisticsCommand = new ViewModelCommand(TourStatistics);

            Years = new ObservableCollection<string>(Enumerable.Range(2015, 2023 - 2015 + 1).Select(y => y.ToString()));
            Years.Add("All time");

            SelectedYear = "All time";
        }

        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }
        public void CreateTour(object obj)
        {
            DataBaseContext context = new DataBaseContext();
            TourFlagTransfer tourFlagTransfer = new TourFlagTransfer(2);
            context.TourFlagTransfers.Add(tourFlagTransfer);
            context.SaveChanges();
            _mainViewModel.ExecuteShowTourGuideCreateTourViewCommand(null);
        }
        public void ToursToday(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursTodayViewCommand(null);
        }
        public void FutureTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideFutureToursViewCommand(null);
        }
        public void FinishedTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideFinishedToursViewCommand(null);
        }
        private void TourStatistics(object obj)
        {
            if (SelectedYear == null)
            {
                MessageBox.Show("Please select a year for tour statistics.");
                return;
            }

            using (DataBaseContext transferContext = RemoveTransferedTours())
            {
                List<int> mostVisitedTours = new List<int>();
                TourStatisticsDTO statisticsToShow = new TourStatisticsDTO();

                using (DataBaseContext context = new DataBaseContext())
                {
                    List<Tour> tours = context.Tours.ToList();
                    List<TourAttendance> attendances = context.TourAttendances.ToList();

                    if (SelectedYear != "All time")
                    {
                        int selectedYear = int.Parse(SelectedYear);
                        bool hasTourInSelectedYear = false;

                        foreach (Tour tour in tours)
                        {
                            if (tour.startDates.Year == selectedYear)
                            {
                                hasTourInSelectedYear = true;

                                foreach (TourAttendance att in attendances)
                                {
                                    if (tour.id == att.tourId)
                                    {
                                        mostVisitedTours.Add(att.numberOfGuests);
                                    }
                                }
                            }
                        }

                        if (!hasTourInSelectedYear)
                        {
                            MessageBox.Show("There is no tour for the selected year.");
                            return;
                        }
                    }
                    else
                    {
                        foreach (Tour tour in tours)
                        {
                            foreach (TourAttendance att in attendances)
                            {
                                mostVisitedTours.Add(att.numberOfGuests);
                            }
                        }
                    }

                    int maxAttendance = mostVisitedTours.Max();
                    int maxTourId;

                    statisticsToShow.numberOfGuests = maxAttendance;

                    foreach (TourAttendance attendance in attendances)
                    {
                        if (attendance.numberOfGuests == maxAttendance)
                        {
                            maxTourId = attendance.tourId;
                            statisticsToShow.tourId = maxTourId;
                            foreach (Tour tour in tours)
                            {
                                if (tour.id == maxTourId)
                                {
                                    statisticsToShow.tourName = tour.name;
                                    statisticsToShow.startDate = tour.startDates;
                                }
                            }
                        }
                    }
                }

                transferContext.TourStatisticsTransfer.Add(statisticsToShow);
                transferContext.SaveChanges();
            }

            _mainViewModel.ExecuteShowTourGuideTourStatisticsViewCommand(null);
        }


        private static DataBaseContext RemoveTransferedTours()
        {
            DataBaseContext transferContext = new DataBaseContext();
            List<TourStatisticsDTO> transfertableData = transferContext.TourStatisticsTransfer.ToList();
            transferContext.TourStatisticsTransfer.RemoveRange(transfertableData);
            transferContext.SaveChanges();
            return transferContext;
        }
    }
}
