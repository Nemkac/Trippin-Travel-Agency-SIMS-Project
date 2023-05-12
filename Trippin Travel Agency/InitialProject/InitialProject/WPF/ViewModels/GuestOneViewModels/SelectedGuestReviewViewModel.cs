using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class SelectedGuestReviewViewModel : ViewModelBase
    {
        private GuestRateService guestRateService;
        private BookingService bookingService;
        private AccommodationService accommodationService;
        public ViewModelCommand Ok { get; set; }
        public SelectedGuestReviewViewModel()
        {
            Ok = new ViewModelCommand(Continue);
            bookingService = new BookingService(new BookingRepository());
            accommodationService = new AccommodationService(new AccommodationRepository());
            guestRateService = new GuestRateService(new GuestRateRepository());
            AccommodationName = accommodationService.GetById(bookingService.GetById(GuestOneStaticHelper.guestRate.bookingId).accommodationId).name;
            ReviewInfoLabels = "Cleannes:\n\n Respecting rules:\n\nComment:\n\n";
            ReviewInfo = guestRateService.GetDisplayableRate(GuestOneStaticHelper.guestRate);
        }

        public void Continue(object sender)
        {
            GuestOneStaticHelper.guestsReviewsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            GuestOneStaticHelper.selectedGuestReviewInterface.Close();
        }

        private string reviewInfo;
        public string ReviewInfo
        {
            get { return reviewInfo; }
            set
            {
                if (reviewInfo != value)
                {
                    reviewInfo = value;
                    OnPropertyChanged(nameof(ReviewInfo));
                }
            }
        }

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private string reviewInfoLabels;
        public string ReviewInfoLabels
        {
            get { return reviewInfoLabels; }
            set
            {
                if (reviewInfoLabels != value)
                {
                    reviewInfoLabels = value;
                    OnPropertyChanged(nameof(ReviewInfoLabels));
                }
            }
        }
    }
}
