using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class RateAccommodationViewModel : ViewModelBase
    {
        public ViewModelCommand RateAccommodation { get; set; }
        public ViewModelCommand AddImageUrl { get; set; }
        private AccommodationRateService accommodationRateService = new(new AccommodationRateRepository());

        public List<Model.Image> imageUrls = new List<Model.Image>();
        public int imageCounter = 0;
        public RateAccommodationViewModel()
        {
            RateAccommodation = new ViewModelCommand(Rate);
            AddImageUrl = new ViewModelCommand(GetImage);
        }

        private int cleannesSliderInput;
        public int CleannesSliderInput
        {
            get { return cleannesSliderInput; }
            set
            {
                if (cleannesSliderInput != value)
                {
                    cleannesSliderInput = value;
                    OnPropertyChanged(nameof(CleannesSliderInput));
                }
            }
        }

        private int ownerSliderInput;
        public int OwnerSliderInput
        {
            get { return ownerSliderInput; }
            set
            {
                if (ownerSliderInput != value)
                {
                    ownerSliderInput = value;
                    OnPropertyChanged(nameof(OwnerSliderInput));
                }
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (imageUrl != value)
                {
                    imageUrl = value;
                    OnPropertyChanged(nameof(ImageUrl));
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            } 
        }

        private string imageCounterMessage;
        public string ImageCounterMessage
        {
            get { return imageCounterMessage; }
            set
            {
                if (imageCounterMessage != value)
                {
                    imageCounterMessage = value;
                    OnPropertyChanged(nameof(ImageCounterMessage));
                }
            }
        }


        private void GetImage(object sender)
        {
            Model.Image image = new Model.Image(ImageUrl, null);
            imageUrls.Add(image);
            ImageUrl = string.Empty;
            imageCounter++;
            if (imageCounter == 1)
            {
                ImageCounterMessage = "You have added 1 image";
            }
            if (imageCounter > 1)
            {
                ImageCounterMessage = "You have added " + imageCounter + " images";
            }
        }
        private void Rate(object sender)
        {
            AccommodationRate accommodationRate = new AccommodationRate(GuestOneStaticHelper.selectedBookingIdToRate, cleannesSliderInput, OwnerSliderInput, Comment , imageUrls);
            this.accommodationRateService.Save(accommodationRate);
        }
    }
}
