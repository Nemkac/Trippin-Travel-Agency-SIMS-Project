using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Service;
using InitialProject.ViewModels;
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
using System.Windows.Shapes;

namespace InitialProject.View
{
    public partial class TourGuide_ToursToday : UserControl
    {
        public TourGuide_ToursToday()
        {
            InitializeComponent();
            TourService tourService = new TourService();
            List<Tour> toursToday = tourService.GetToursToday();
            List<ToursTodayDTO> tourDtosToday = new List<ToursTodayDTO>();

            foreach (Tour t in toursToday)
            {
                tourDtosToday.Add(tourService.createToursTodayDTO(t));
            }
            tourDataGrid.ItemsSource = tourDtosToday;
        }
        public void startTourClick(object sender, RoutedEventArgs e)
        {
            ToursTodayDTO tourData = tourDataGrid.SelectedItem as ToursTodayDTO;
            DataBaseContext dataBaseContext = new DataBaseContext();
            TourLiveViewTransfer tourLiveViewTransfer = new TourLiveViewTransfer(tourData.id); 
            dataBaseContext.TourLiveViewTransfers.Add(tourLiveViewTransfer);
            dataBaseContext.SaveChanges(); 
        }
    }
}
