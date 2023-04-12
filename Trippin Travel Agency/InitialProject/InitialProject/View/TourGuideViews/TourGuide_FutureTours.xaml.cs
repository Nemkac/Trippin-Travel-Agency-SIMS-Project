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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_FutureTours : UserControl
    {
        public TourGuide_FutureTours()
        {
            InitializeComponent();
            TourService tourService = new TourService();
            List<Tour> futureTours = tourService.GetFutureTours();
            List<FutureToursDTO> futureToursDtos = new List<FutureToursDTO>();
            
            foreach (Tour tour in futureTours)
            {
                futureToursDtos.Add(tourService.createFutureToursDTO(tour)); 
            }
            futureToursDataGrid.ItemsSource = futureToursDtos;
        }

        /*public void cancelTour_ButtonClick(object sender, RoutedEventArgs e)
        {
            FutureToursDTO futureToursData = futureToursDataGrid.SelectedItem as FutureToursDTO;
            DataBaseContext dataBaseContext = new DataBaseContext(); 
        }*/
        public void cancelTour_ButtonClick(object sender, RoutedEventArgs e)
        {
            FutureToursDTO selectedFutureTourDto = futureToursDataGrid.SelectedItem as FutureToursDTO;
            if (selectedFutureTourDto == null)
            {
                MessageBox.Show("Please select a tour to cancel.");
                return;
            }

            DeleteTour(selectedFutureTourDto);
        }

        private void DeleteTour(FutureToursDTO selectedFutureTourDto)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Tour tourToDelete = dataBaseContext.Tours.FirstOrDefault(t => t.id == selectedFutureTourDto.id);

                if (tourToDelete != null)
                {
                    TimeSpan timeUntilTourStart = tourToDelete.startDates - DateTime.Now;

                    if (timeUntilTourStart.TotalHours > 48)
                    {
                        dataBaseContext.Images.RemoveRange(dataBaseContext.Images.Where(i => i.tourId == tourToDelete.id));
                        dataBaseContext.KeyPoints.RemoveRange(dataBaseContext.KeyPoints.Where(k => k.tourId == tourToDelete.id));
                        dataBaseContext.TourAttendances.RemoveRange(dataBaseContext.TourAttendances.Where(ta => ta.tourId == tourToDelete.id));
                        dataBaseContext.TourLiveViewTransfers.RemoveRange(dataBaseContext.TourLiveViewTransfers.Where(tlt => tlt.tourId == tourToDelete.id));
                        dataBaseContext.TourMessages.RemoveRange(dataBaseContext.TourMessages.Where(tm => tm.tourId == tourToDelete.id));
                        dataBaseContext.TourReservations.RemoveRange(dataBaseContext.TourReservations.Where(tr => tr.tourId == tourToDelete.id));

                        dataBaseContext.Tours.Remove(tourToDelete);
                        
                        foreach(TourReservation tr in dataBaseContext.TourReservations.ToList())
                        {
                            if(tr.id == tourToDelete.id)
                            {
                                Coupon coupon = new Coupon(tr.guestId, DateTime.Now.AddYears(1));
                                dataBaseContext.Coupons.Add(coupon);
                                dataBaseContext.SaveChanges();
                            }
                        }

                        MessageBox.Show("Tour has been cancelled.");

                        RefreshFutureToursDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("You cannot cancel tours that are less than 48 hours away.");
                    }
                }
                else
                {
                    MessageBox.Show("Error: The selected tour was not found in the database.");
                }
            }
        }

        private void RefreshFutureToursDataGrid()
        {
            TourService tourService = new TourService();
            List<Tour> futureTours = tourService.GetFutureTours();
            List<FutureToursDTO> futureToursDtos = new List<FutureToursDTO>();

            foreach (Tour tour in futureTours)
            {
                futureToursDtos.Add(tourService.createFutureToursDTO(tour));
            }
            futureToursDataGrid.ItemsSource = futureToursDtos;
        }

    }
}
