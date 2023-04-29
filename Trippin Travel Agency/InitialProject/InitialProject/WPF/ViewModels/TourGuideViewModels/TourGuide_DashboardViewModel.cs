using InitialProject.DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_DashboardViewModel : ViewModelBase
    {
        public ViewModelCommand CreateTourCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_DashboardViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            CreateTourCommand = new ViewModelCommand(CreateTour);
        }

        public void CreateTour(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideCreateTourViewCommand(null);
        }
    }
}
