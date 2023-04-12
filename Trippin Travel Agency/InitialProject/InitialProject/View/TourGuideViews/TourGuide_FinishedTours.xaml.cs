using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_FinishedTours : UserControl
    {
        public TourGuide_FinishedTours()
        {
            InitializeComponent();
            TourService tourService = new TourService();
            List<Tour> finishedTours = tourService.GetFinishedTours();
            List<FinishedTourDTO> finishedToursDtos = new List<FinishedTourDTO>();
            
            foreach(Tour t in finishedTours)
            {
                finishedToursDtos.Add(tourService.createFinishedToursDTO(t)); 
            }
            finishedToursDataGrid.ItemsSource = finishedToursDtos; 
        }


        public void showData_ButtonClick(object sender, RoutedEventArgs e)
        {
            FinishedTourDTO tourData = finishedToursDataGrid.SelectedItem as FinishedTourDTO;
            DataBaseContext dataBaseContext = new DataBaseContext();
            TourLiveViewTransfer tourLiveViewTransfer = new TourLiveViewTransfer(tourData.id);
            dataBaseContext.TourLiveViewTransfers.Add(tourLiveViewTransfer);
            dataBaseContext.SaveChanges();
        }

    }
}
