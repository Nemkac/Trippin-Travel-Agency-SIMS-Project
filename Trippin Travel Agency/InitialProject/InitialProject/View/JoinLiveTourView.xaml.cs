using InitialProject.Context;
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
    /// Interaction logic for JoinLiveTourView.xaml
    /// </summary>
    public partial class JoinLiveTourView : UserControl
    {
        public JoinLiveTourView()
        {
            InitializeComponent();
            this.Loaded += LoadTourInfo;
        }

        public void LoadTourInfo(object sender, RoutedEventArgs e) { 
            
            DataBaseContext context = new DataBaseContext();
            TourService tourService = new TourService();    
            Tour activeTour = null;
            List<Tour> bookedTours = new List<Tour>();  

            foreach (TourReservation reservation in context.TourReservations.ToList()) {
                if (reservation.guestId == LoggedUser.id) {
                    bookedTours.Add(tourService.GetByID(reservation.tourId));
                }
            }
            foreach (Tour tour in bookedTours) {
                if (tour.active) {
                    activeTour = tour;
                    break;
                }
            }

            if (activeTour != null)
            {
                this.TourNameLabel.Content = activeTour.name;
                List<KeyPoint> keyPoints = tourService.GetKeyPoints(activeTour.id,context);
                var keyPointFilter = from keyPoint in keyPoints
                                     select new
                                     {
                                         keyPoint.name,
                                         keyPoint.visited
                                     };
                this.keyPointsGrid.ItemsSource = keyPointFilter;
                foreach (KeyPoint keyPoint in keyPoints)
                {
                    if (!keyPoint.visited) {
                        this.KeyPointLabel.Content =  "'" + keyPoint.name + "'";
                        break;
                    }
                }
            }
        }
    }
}
