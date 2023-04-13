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
using InitialProject.Model;
using System.Diagnostics;
using InitialProject.Context;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for RateAccommodationInterface.xaml
    /// </summary>
    public partial class RateAccommodationInterface : Window
    {

        public List<Model.Image> imageUrls = new List<Model.Image>();
        public int imageCounter = 0;

        public int bookingId;
        private AccommodationRateService accommodationRateService;

        public RateAccommodationInterface()
        {
            InitializeComponent();
            this.accommodationRateService = new AccommodationRateService(new AccommodationRateRepository());
        }

        public void SetAttributes(int bookingId)
        {
            this.bookingId = bookingId;
        }

        private void GetImageUrlInput(object sender, RoutedEventArgs e)
        {
            Model.Image image = new Model.Image(imageBlock.Text, null);
            imageUrls.Add(image);
            imageBlock.Clear();
            imageCounter++;
            if (imageCounter == 1)
            {
                imageCounterBlock.Text = "You have added 1 image";
            }
            if (imageCounter > 1)
            {
                imageCounterBlock.Text = "You have added " + imageCounter + " images";
            }
        }

        private void LeaveReview(object sender, RoutedEventArgs e)
        {
            AccommodationRate accommodationRate = new AccommodationRate(bookingId, Convert.ToInt32(cleannesInput.Value), Convert.ToInt32(ownerRateInput.Value), commentInput.Text, imageUrls);
            this.accommodationRateService.Save(accommodationRate);
        }
    }
}
