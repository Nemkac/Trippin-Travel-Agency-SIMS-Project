﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_RequestStatisticsViewModel : ViewModelBase
    {
        public ViewModelCommand ShowRequestsCommand { get; private set; }
        public ViewModelCommand ShowDashboardCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_RequestStatisticsViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowRequestsCommand = new ViewModelCommand(ShowRequests);
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
        }

        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }

        public void ShowRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideRequestsViewCommand(null);
        }

    }
}
