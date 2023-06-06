using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_SpecificTourPartViewModel : ViewModelBase
    {
        public ViewModelCommand ShowTourPartsCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_SpecificTourPartViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowTourPartsCommand = new ViewModelCommand(ShowTourParts);
        }

        public void ShowTourParts(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideRequestTimeSlotsViewCommand(null);
        }

    }
}
