using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_FinishedTourDataViewModel : ViewModelBase
    {
        public ViewModelCommand ShowFinishedToursCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_FinishedTourDataViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowFinishedToursCommand = new ViewModelCommand(ShowFinishedTours);
        }

        public void ShowFinishedTours(object obj)
        {
            MessageBox.Show("back");
            _mainViewModel.ExecuteShowTourGuideFinishedToursViewCommand(null);
        }
    }
}
