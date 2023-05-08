using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class RateAccommodationViewModel : ViewModelBase
    {
        public ViewModelCommand RateAccommodation { get; set; }
        public ViewModelCommand AddImageUrl { get; set; }
        private AccommodationRateService accommodationRateService = new(new AccommodationRateRepository());
        public ViewModelCommand GoToPreviousWindow { get; set; }

        public List<Model.Image> imageUrls = new List<Model.Image>();
        public int imageCounter = 0;
        public RateAccommodationViewModel()
        {
            RateAccommodation = new ViewModelCommand(Rate);
            AddImageUrl = new ViewModelCommand(GetImage);
            GoToPreviousWindow = new ViewModelCommand(GoBack);

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

        private string warningText;
        public string WarningText
        {
            get { return warningText; }
            set
            {
                if (warningText != value)
                {
                    warningText = value;
                    OnPropertyChanged(nameof(WarningText));
                }
            }
        }

        private void GoBack(object sender)
        {
            GuestOneStaticHelper.pastBookingsInterface.Show();
            GuestOneStaticHelper.rateAccommodationInterface.Close();
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
            if (Comment != null && Comment != string.Empty)
            {
                AccommodationRate accommodationRate = new AccommodationRate(GuestOneStaticHelper.selectedBookingIdToRate, cleannesSliderInput, OwnerSliderInput, Comment, imageUrls);
                this.accommodationRateService.Save(accommodationRate);
                WarningText = string.Empty;

                RateAccommodationConfirmationInterface rateAccommodationConfirmationInterface = new RateAccommodationConfirmationInterface();
                rateAccommodationConfirmationInterface.Left = GuestOneStaticHelper.rateAccommodationInterface.Left + (GuestOneStaticHelper.rateAccommodationInterface.Width - rateAccommodationConfirmationInterface.Width) / 2;
                rateAccommodationConfirmationInterface.Top = GuestOneStaticHelper.rateAccommodationInterface.Top + (GuestOneStaticHelper.rateAccommodationInterface.Height - rateAccommodationConfirmationInterface.Height) / 2; ;
                GuestOneStaticHelper.rateAccommodationInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                rateAccommodationConfirmationInterface.Show();
            } else
            {
                WarningText = "Please fill the comment box";
            }
        }
    }
}
