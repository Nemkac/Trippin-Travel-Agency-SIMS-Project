using InitialProject.Model;
using InitialProject.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class TourGuide_MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public ICommand ShowTourGuideDashboardViewCommand { get; }
        public ICommand ShowTourGuideToursViewCommand { get; }
        public ICommand ShowTourGuideCreateTourViewCommand { get; }
        public ICommand ShowTourGuideToursTodayViewCommand { get; }
        public ICommand ShowTourGuideTourLiveViewCommand { get; }
        public ICommand ShowTourGuideFutureToursViewCommand { get; }
        public ICommand ShowTourGuideFinishedToursViewCommand { get; }
        public ICommand ShowTourGuideTourDataViewCommand { get; }
        public ICommand ShowTourGuideTourStatisticsViewCommand { get; }

        public TourGuide_MainViewModel()
        {
            ShowTourGuideDashboardViewCommand = new ViewModelCommand(ExecuteShowTourGuideDashboardViewCommand);
            ShowTourGuideToursViewCommand = new ViewModelCommand(ExecuteShowTourGuideToursViewCommand);
            ShowTourGuideCreateTourViewCommand = new ViewModelCommand(ExecuteShowTourGuideCreateTourViewCommand);
            ShowTourGuideToursTodayViewCommand = new ViewModelCommand(ExecuteShowTourGuideToursTodayViewCommand);
            ShowTourGuideTourLiveViewCommand = new ViewModelCommand(ExecuteShowTourGuideTourLiveViewCommand);
            ShowTourGuideFutureToursViewCommand = new ViewModelCommand(ExecuteShowTourGuideFutureToursViewCommand);
            ShowTourGuideFinishedToursViewCommand = new ViewModelCommand(ExecuteShowTourGuideFinishedToursViewCommand);
            ShowTourGuideTourDataViewCommand = new ViewModelCommand(ExecuteShowTourGuideTourDataViewCommand);
            ShowTourGuideTourStatisticsViewCommand = new ViewModelCommand(ExecuteShowTourGuideTourStatisticsViewCommand);
            ExecuteShowTourGuideDashboardViewCommand(null);
        }

        public void ExecuteShowTourGuideDashboardViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_DashboardViewModel();
        }
        public void ExecuteShowTourGuideToursViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_ToursViewModel(this);
        }
        public void ExecuteShowTourGuideCreateTourViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_CreateTourViewModel(this);
        }
        public void ExecuteShowTourGuideToursTodayViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_ToursTodayViewModel(this);
        }
        public void ExecuteShowTourGuideTourLiveViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_TourLiveViewModel(this);
        }
        public void ExecuteShowTourGuideFutureToursViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_FutureToursViewModel(this);
        }
        public void ExecuteShowTourGuideFinishedToursViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_FinishedToursViewModel(this);
        }
        
        public void ExecuteShowTourGuideTourDataViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_FinishedTourDataViewModel(this);
        }
        public void ExecuteShowTourGuideTourStatisticsViewCommand(object obj)
        {
            CurrentChildView = new TourGuide_TourStatisticsViewModel(this);
        }


    }
}
