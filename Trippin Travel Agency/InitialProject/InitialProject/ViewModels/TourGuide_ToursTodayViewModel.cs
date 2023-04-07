using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModels
{
    public class TourGuide_ToursTodayViewModel : ViewModelBase
    {
        private string _selectedTourName;

        public string SelectedTourName
        {
            get { return _selectedTourName; }
            set
            {
                _selectedTourName = value;
                OnPropertyChanged(nameof(SelectedTourName));
            }
        }

        public ViewModelCommand ShowToursCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursTodayViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowToursCommand = new ViewModelCommand(ShowTours);
        }

        public TourGuide_ToursTodayViewModel()
        {
        }

        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);
        }
    }

}
