using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_TourStatisticsViewModel : ViewModelBase
    {
        public ViewModelCommand ShowToursCommand { get; private set; }
        public ViewModelCommand ShowDashboardCommand { get; private set; }
        private readonly TourGuide_MainViewModel _mainViewModel;
        private string? _tourName;
        public string? TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }
        private int? _tourVisistedBy;
        public int? TourVisitedBy
        {
            get { return _tourVisistedBy; }
            set
            {
                _tourVisistedBy = value;
                OnPropertyChanged(nameof(TourVisitedBy));
            }
        }
        private DateTime? _tourDate;
        public DateTime? TourDate
        {
            get { return _tourDate; }
            set
            {
                _tourDate = value;
                OnPropertyChanged(nameof(TourDate));
            }
        }
        public TourGuide_TourStatisticsViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowToursCommand = new ViewModelCommand(ShowTours);
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
            LoadTourStatistics();
        }

        private void LoadTourStatistics()
        {
            DataBaseContext tourStatisticsDto = new DataBaseContext();
            TourStatisticsDTO transferedTour = tourStatisticsDto.TourStatisticsTransfer.First();
            TourName = transferedTour.tourName;
            TourVisitedBy = transferedTour.numberOfGuests;
            TourDate = transferedTour.startDate;
        }

        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);
        }
        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }
    }
}
