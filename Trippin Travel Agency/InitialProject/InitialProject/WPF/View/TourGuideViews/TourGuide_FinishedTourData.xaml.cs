using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;
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
    public partial class TourGuide_FinishedTourData : UserControl
    {
        private readonly TourService tourService;
        private readonly MessageService messageService = new MessageService();

        private readonly TourReviewService tourReviewService = new TourReviewService();

        public TourGuide_FinishedTourData()
        {
            InitializeComponent();
            this.Loaded += tourDataLoaded;
            this.tourService = new(new TourRepository());
        }

        public void tourDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = this.tourService.GetById(requests.Last().tourId);
            TourMessage message = messageService.GetByTourId(tour.id);
            this.headerTextBlock.Text = tour.name;
            List<TourAndGuideRateDTO> finishedTourReviewDtos = new List<TourAndGuideRateDTO>();
            //MessageBox.Show(message.keyPointId.ToString()); 

            List<TourAndGuideRate> reviews = tourService.GetTourRatingsById(tour.id);
            foreach (TourAndGuideRate tr in reviews)
            {
                finishedTourReviewDtos.Add(tourReviewService.transformTourReviewToDTO(tr, message.keyPointId));
            }
            this.reviewsDataGrid.ItemsSource = finishedTourReviewDtos;
        }


        public void report_ButtonClick(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = this.tourService.GetById(requests.Last().tourId);
            var selectedReview = (TourAndGuideRateDTO)reviewsDataGrid.SelectedItem;
            TourAndGuideRate tagr = tourReviewService.GetById(selectedReview.id);
            tagr.valid = false; 
            context.TourAndGuideRates.Update(tagr);
            context.SaveChanges();
            RefreshReviews(tour);
        }

        private void RefreshReviews(Tour tour)
        {
            List<TourAndGuideRateDTO> dtos = new List<TourAndGuideRateDTO>();
            TourMessage message = messageService.GetByTourId(tour.id);
            List<TourAndGuideRate> reviews = tourReviewService.GetReviewsById(tour.id);
            foreach (TourAndGuideRate tr in reviews)
            {
                dtos.Add(tourReviewService.transformTourReviewToDTO(tr, message.keyPointId));
            }
            reviewsDataGrid.ItemsSource = dtos;
        }



    }
}
