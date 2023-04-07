using InitialProject.View;
using System;
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

        public TourGuide_MainViewModel()
        {
            ShowTourGuideDashboardViewCommand = new ViewModelCommand(ExecuteShowTourGuideDashboardViewCommand);
            ShowTourGuideToursViewCommand = new ViewModelCommand(ExecuteShowTourGuideToursViewCommand);
            ShowTourGuideCreateTourViewCommand = new ViewModelCommand(ExecuteShowTourGuideCreateTourViewCommand);
            ShowTourGuideToursTodayViewCommand = new ViewModelCommand(ExecuteShowTourGuideToursTodayViewCommand);
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
    }
}
