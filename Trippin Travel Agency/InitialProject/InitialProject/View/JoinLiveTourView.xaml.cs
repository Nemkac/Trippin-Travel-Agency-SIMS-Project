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
            Tour activeTour = tourService.GetActiveTour(context);

            this.SubmitButton.IsEnabled = false;
            this.CommentBox.IsEnabled = false;
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
            CheckForAttendanceConfirmation(context, activeTour);
            }            
        }
        

        private void JoinTour(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            TourService tourService = new TourService();
            Tour activeTour = tourService.GetActiveTour(context);

            foreach (TourReservation tourReservation in context.TourReservations.ToList()) {
                if (tourReservation.tourId == activeTour.id)
                { 
                    tourReservation.guestJoined = true;
                    context.SaveChanges();
                }
            }
        }

        public void CheckForAttendanceConfirmation(DataBaseContext context, Tour activeTour)
        {
            foreach (TourReservation reservation in context.TourReservations.ToList())
            {
                if (activeTour.id == reservation.tourId && reservation.guideConfirmed == true) { 
                    this.SubmitButton.IsEnabled = true;
                    this.UploadPhotoButton.IsEnabled = true;
                    this.CommentBox.IsEnabled = true;
                }
            }
        }

        public void SubmitRating(object sender, RoutedEventArgs e)
        {

            int guideKnowledge = -1;
            int contentRating = -1;
            int guideLanguageUsage = -1;
            string comment = string.Empty;

            if ((bool)this.Knowledge1.IsChecked) {
                guideKnowledge = 1;
            } 
            else if ((bool)this.Knowledge2.IsChecked) 
            {
                guideKnowledge = 2;
            }
            else if ((bool)this.Knowledge3.IsChecked)
            {
                guideKnowledge = 3;
            }
            else if ((bool)this.Knowledge4.IsChecked)
            {
                guideKnowledge = 4;
            }
            else if ((bool)this.Knowledge5.IsChecked)
            {
                guideKnowledge = 5;
            }

            if ((bool)this.Content1.IsChecked)
            {
                contentRating = 1;
            }
            else if ((bool)this.Content2.IsChecked)
            {
                contentRating = 2;
            }
            else if ((bool)this.Content3.IsChecked)
            {
                contentRating = 3;
            }
            else if ((bool)this.Content4.IsChecked)
            {
                contentRating = 4;
            }
            else if ((bool)this.Content5.IsChecked)
            {
                contentRating = 5;
            }

            if ((bool)this.Translation1.IsChecked)
            {
                guideLanguageUsage = 1;
            }
            else if ((bool)this.Translation2.IsChecked)
            {
                guideLanguageUsage = 2;
            }
            else if ((bool)this.Translation3.IsChecked)
            {
                guideLanguageUsage = 3;
            }
            else if ((bool)this.Translation4.IsChecked)
            {
                guideLanguageUsage = 4;
            }
            else if ((bool)this.Translation5.IsChecked)
            {
                guideLanguageUsage = 5;
            }

            comment = this.CommentBox.Text;

            DataBaseContext context = new DataBaseContext();
            TourService tourService = new TourService();
            Tour activeTour = tourService.GetActiveTour(context);
            TourAndGuideRate tourAndGuideRate = new TourAndGuideRate(LoggedUser.id,activeTour.id,guideKnowledge,guideLanguageUsage,contentRating,comment,activeTour.guideId);
            context.TourAndGuideRates.Add(tourAndGuideRate);
            context.SaveChanges();
            this.SubmitButton.IsEnabled = false;
            this.CommentBox.IsEnabled = false;
            //Kada nam asistent sa hci pokaze kako je najbolje da se radi sa slikama tada cu da ubacim da moze slika da se uploadjuje. Do tada, samo 3 ocene
        }

        private void UploadPhoto(object sender, RoutedEventArgs e)
        {

        }
    }
}
