﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_FullTourRequestsViewModel : ViewModelBase
    {
        public ViewModelCommand ShowRequestsCommand { get; private set; }
        
        public ViewModelCommand ShowAcceptedTourRequestCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_FullTourRequestsViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowRequestsCommand = new ViewModelCommand(ShowRequests);
            ShowAcceptedTourRequestCommand = new ViewModelCommand(ShowAcceptedRequest);
        }

        public void ShowRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideRequestsViewCommand(null);
        }


        public void ShowAcceptedRequest(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideAcceptedTourRequestViewCommand(null);
        }

    }
}
