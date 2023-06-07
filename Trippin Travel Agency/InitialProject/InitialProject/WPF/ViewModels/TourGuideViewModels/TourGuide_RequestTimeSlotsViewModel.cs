using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_RequestTimeSlotsViewModel : ViewModelBase
    {

        private readonly TourGuide_MainViewModel _mainViewModel;
        public ViewModelCommand ShowTourPartRequestsCommand { get; private set; }
        public ViewModelCommand ShowSpecificTourPartCommand { get; private set; }


        public TourGuide_RequestTimeSlotsViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowTourPartRequestsCommand = new ViewModelCommand(ShowTourPartRequests);
            ShowSpecificTourPartCommand = new ViewModelCommand(ShowSpecificTourPart);

        }
        public void ShowTourPartRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideTourPartRequestsViewCommand(null);
        }
        public void ShowSpecificTourPart(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideSpecificTourPartViewCommand(null);
        }
    }
}
