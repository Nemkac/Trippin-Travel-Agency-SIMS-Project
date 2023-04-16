using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;
        private ViewModelBase _secondChildView;
        public ViewModelBase SecondChildView
        {
            get
            {
                return _secondChildView;
            }

            set
            {
                _secondChildView = value;
                OnPropertyChanged(nameof(SecondChildView));
            }
        }

        public ICommand ShowOurRecommendationsViewCommand { get; private set; }
        public ViewModelCommand ShowAnnualStatisticsViewCommand { get; private set; }

        public AccommodationStatisticsViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowOurRecommendationsViewCommand = new ViewModelCommand(ExecuteShowOurRecommendationsViewCommand);
            ShowAnnualStatisticsViewCommand = new ViewModelCommand(ShowAnnualStatistics);
            ExecuteShowOurRecommendationsViewCommand(null);
        }

        public void ExecuteShowOurRecommendationsViewCommand(object obj)
        {
            SecondChildView = new OurRecommendationsViewModel();
        }

        public void ShowAnnualStatistics(object obj)
        { 
            _mainViewModel.ExecuteShowAnnualStatisticsCommand(null);
        }
    }
}
