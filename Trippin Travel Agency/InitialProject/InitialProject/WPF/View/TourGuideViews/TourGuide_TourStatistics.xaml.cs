using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_TourStatistics : UserControl
    {
        public TourGuide_TourStatistics()
        {
            InitializeComponent();
            DataBaseContext tourStatisticsDto = new DataBaseContext();
            TourStatisticsDTO transferedTour = tourStatisticsDto.TourStatisticsTransfer.First();
            statisticsTourName.Content = transferedTour.tourName;
            statisticsVisitedBy.Content = transferedTour.numberOfGuests;
            statisticsDate.Content = transferedTour.startDate;
        }
    }
}
