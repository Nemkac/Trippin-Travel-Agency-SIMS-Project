using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationAnnualStatisticsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ViewModelCommand ShowMonthlyStatistics { get; private set; }

        public AccommodationAnnualStatisticsViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            ShowMonthlyStatistics = new ViewModelCommand(ExecuteShowMonthlyStatistics);
        }

        private void ExecuteShowMonthlyStatistics(object obj)
        {
            _mainViewModel.ExecuteShowMonthlyStatistics(null);
        }
    }
}
