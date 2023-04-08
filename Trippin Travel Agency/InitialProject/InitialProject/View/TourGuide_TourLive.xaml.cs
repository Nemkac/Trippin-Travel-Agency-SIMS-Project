using InitialProject.ViewModels;
using System.Windows.Controls;

namespace InitialProject.View
{
    public partial class TourGuide_TourLive : UserControl
    {
        public TourGuide_TourLive(TourGuide_MainViewModel mainViewModel, int tourId)
        {
            InitializeComponent();
            DataContext = new TourGuide_TourLiveViewModel(mainViewModel, tourId);
        }

    }
}
