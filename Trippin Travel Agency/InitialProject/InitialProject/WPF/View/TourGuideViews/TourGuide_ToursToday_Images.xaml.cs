using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
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
using System.Windows.Shapes;

namespace InitialProject.WPF.View.TourGuideViews
{
    public partial class TourGuide_ToursToday_Images : UserControl
    {
        private readonly TourService tourService;
        //private TourService tourService;
        public TourGuide_ToursToday_Images()
        {
            InitializeComponent();
            this.Loaded += tourDataLoaded;
            this.tourService = new(new TourRepository());

        }
        public void tourDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            this.tourName.Content = tour.name; 
        }

        private void GetExact(out DataBaseContext context, out Tour tour)
        {
            context = new DataBaseContext();
            List<TourTodayImagesTransfer> tours = context.TourTodayImagesTransfers.ToList();
            tour = this.tourService.GetById(tours.Last().tourId);
        }
    }
}
