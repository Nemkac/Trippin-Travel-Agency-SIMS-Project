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
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help { get; set; }

        public List<Model.Image> imageUrls = new List<Model.Image>();
        public int imageCounter = 0;
        bool isHelpOn = false;

        public RateAccommodationViewModel()
        {
            RateAccommodation = new ViewModelCommand(Rate);
            AddImageUrl = new ViewModelCommand(GetImage);
            GoToPreviousWindow = new ViewModelCommand(GoBack);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            Help = new ViewModelCommand(ShowHelp);

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

        private string helpExit;
        public string HelpExit
        {
            get { return helpExit; }
            set
            {
                if (helpExit != value)
                {
                    helpExit = value;
                    OnPropertyChanged(nameof(helpExit));
                }
            }
        }

        private string helpReview;
        public string HelpReview
        {
            get { return helpReview; }
            set
            {
                if (helpReview != value)
                {
                    helpReview = value;
                    OnPropertyChanged(nameof(HelpReview));
                }
            }
        }

        private string helpSlide;
        public string HelpSlide
        {
            get { return helpSlide; }
            set
            {
                if (helpSlide != value)
                {
                    helpSlide = value;
                    OnPropertyChanged(nameof(HelpSlide));
                }
            }
        }

        private string helpImage;
        public string HelpImage
        {
            get { return helpImage; }
            set
            {
                if (helpImage != value)
                {
                    helpImage = value;
                    OnPropertyChanged(nameof(HelpImage));
                }
            }
        }

        public void ShowHelp(object sender)
        {
            if(isHelpOn)
            {
                HelpImage = string.Empty;
                HelpSlide = string.Empty; 
                HelpReview = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpImage = "When image URL is entered, press CTRL+S to confirm it, you can do it as many times as you want";
                HelpSlide = "When cleannes or owner scale is selected, adjust the rate with LEFT and RIGHT arrows";
                HelpReview = "This is a place where you can rate your staying.\n You can go through review attributes by pressing TAB.\n *Note that comment is required so please do not leave it blank" ;
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private void GoBack(object sender)
        {
            GuestOneStaticHelper.pastBookingsInterface.Show();
            GuestOneStaticHelper.rateAccommodationInterface.Close();
        }

        private void ShowNavigator(object sender)
        {
            GuestOneStaticHelper.InterfaceToGoBack = GuestOneStaticHelper.rateAccommodationInterface;
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.rateAccommodationInterface.Left + (GuestOneStaticHelper.rateAccommodationInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.rateAccommodationInterface.Top + (GuestOneStaticHelper.rateAccommodationInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.rateAccommodationInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
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

            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            bool? response = fileDialog.ShowDialog();
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
