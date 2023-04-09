﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModels
{
    public class TourGuide_ToursViewModel : ViewModelBase
    {
        public ViewModelCommand ShowDashboardCommand { get; private set; }
        public ViewModelCommand CreateTourCommand { get; private set; }
        public ViewModelCommand ToursTodayCommand { get; private set; }
        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
            CreateTourCommand = new ViewModelCommand(CreateTour);
            ToursTodayCommand = new ViewModelCommand(ToursToday); 
        }

        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }

        public void CreateTour(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideCreateTourViewCommand(null);
        }

        public void ToursToday(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursTodayViewCommand(null);
        }
    }
}