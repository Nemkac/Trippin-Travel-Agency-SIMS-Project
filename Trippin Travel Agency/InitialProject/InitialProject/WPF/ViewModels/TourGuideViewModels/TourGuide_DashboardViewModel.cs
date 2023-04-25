using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_DashboardViewModel : ViewModelBase
    {
        /*private string _username;
        public string username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public TourGuide_DashboardViewModel(string username)
        {
            this.username = username;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
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
