using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Context;
using InitialProject.Model.TransferModels;
using System.Windows;
using InitialProject.WPF.ViewModels;
using InitialProject.DTO;
using InitialProject.Model;
using System.Collections.ObjectModel;
using InitialProject.WPF.View.GuestTwoViews;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class TourConfirmationViewModel : ViewModelBase
    {
        public ObservableCollection<CouponDTO> couponDTOs { get; set; } = new ObservableCollection<CouponDTO>();
        private string tourName;
        public string TourName
        {
            get { return tourName; }
            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }
        private string cityName;
        public string CityName
        {
            get { return cityName; }
            set
            {
                if (cityName != value)
                {
                    cityName = value;
                    OnPropertyChanged(nameof(CityName));
                }
            }
        }

        private string countryName;
        public string CountryName
        {
            get { return countryName; }
            set
            {
                if (countryName != value)
                {
                    countryName = value;
                    OnPropertyChanged(nameof(CountryName));
                }
            }
        }
        private string keyPoints;
        public string KeyPoints
        {
            get { return keyPoints; }
            set
            {
                if (keyPoints != value)
                {
                    keyPoints = value;
                    OnPropertyChanged(nameof(KeyPoints));
                }
            }
        }

        private int guestNumber;
        public int GuestNumber
        {
            get { return guestNumber; }
            set
            {
                if (guestNumber != value)
                {
                    guestNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }

        private int duration;
        public int Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        private CouponDTO selectedCoupon;
        public CouponDTO SelectedCoupon
        {
            get { return selectedCoupon; }
            set
            {
                if (selectedCoupon != value)
                {
                    selectedCoupon = value;
                    OnPropertyChanged(nameof(SelectedCoupon));
                }
            }
        }

        private bool finishDisabled;
        public bool FinishDisabled
        {
            get { return finishDisabled; }
            set
            {
                if (finishDisabled != value)
                {
                    finishDisabled = value;
                    OnPropertyChanged(nameof(FinishDisabled));
                }
            }
        }

        private bool cancelDisabled;
        public bool CancelDisabled
        {
            get { return cancelDisabled; }
            set
            {
                if (cancelDisabled != value)
                {
                    cancelDisabled = value;
                    OnPropertyChanged(nameof(CancelDisabled));
                }
            }
        }

        private string imageSlider;
        public string ImageSlider
        {
            get { return imageSlider; }
            set
            {
                if (imageSlider != value)
                {
                    imageSlider = value;
                    OnPropertyChanged(nameof(ImageSlider));
                }
            }
        }

        public int imagecounter = 1;

        private readonly GuestTwoInterfaceViewModel _mainViewModel;
        public ViewModelCommand CancelBookingCommand { get; private set; }
        public ViewModelCommand TourViewCommand { get; private set; }
        public ViewModelCommand NextPhoto { get; private set; }
        public ViewModelCommand PreviousPhoto { get; private set; }
        public ViewModelCommand CreateTourReport { get; private set; }

        public TourConfirmationViewModel()
        {
            _mainViewModel = LoggedUser.GuestTwoInterfaceViewModel;
            WindowLoaded();
            FinishDisabled = true;
            CancelDisabled = true;
            TourViewCommand = new ViewModelCommand(ShowDetailedTourView);
            CancelBookingCommand = new ViewModelCommand(CancelBooking);
            NextPhoto = new ViewModelCommand(GetNextPhoto);
            PreviousPhoto = new ViewModelCommand(GetPreviousPhoto);
            CreateTourReport = new ViewModelCommand(ExecuteCreateTourReport);
        }

        public void ShowDetailedTourView(object obj)
        {
            DataBaseContext context = new DataBaseContext();
            CouponDTO? selectedCoupon = SelectedCoupon;

            foreach (Coupon coupon in context.Coupons.ToList())
            {
                if (selectedCoupon != null && coupon.id == selectedCoupon.id)
                {
                    context.Coupons.Remove(coupon);
                }
            }

            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.ToList().Last();
            Tour tour = context.Tours.SingleOrDefault(t => t.id == tourBookingTransfer.tourId);
            tour.touristLimit -= tourBookingTransfer.numberOfGuests;
            TourReservation tourReservation = new TourReservation(LoggedUser.id, tour.id, tourBookingTransfer.numberOfGuests);
            if (selectedCoupon != null)
            {
                tourReservation.withVoucher = true;
            }


            context.TourReservations.Add(tourReservation);
            context.SaveChanges();
            context.tourBookingTransfers.Remove(tourBookingTransfer);
            context.SaveChanges();
             
            // ShowCoupons();
            // CancelDisabled = false;
            // FinishDisabled = false;

            _mainViewModel.ExecuteTourViewCommand(null);
        }

        public void CancelBooking(object obj) 
        {
            DataBaseContext context = new DataBaseContext();
            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.ToList().Last();
            context.tourBookingTransfers.Remove(tourBookingTransfer);
            context.SaveChanges();
            // CancelDisabled = false;
            // FinishDisabled = false;
            _mainViewModel.ExecuteTourViewCommand(null);
        }

        public void WindowLoaded()
        {

            ShowCoupons();
            DataBaseContext context = new DataBaseContext();
            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.ToList().Last();
            TourName = tourBookingTransfer.name;
            CityName = tourBookingTransfer.cityLocation;
            CountryName = tourBookingTransfer.countryLocation;
            KeyPoints = tourBookingTransfer.keypoints;
            GuestNumber = tourBookingTransfer.numberOfGuests;
            Duration = tourBookingTransfer.hoursDuration;
            ImageSlider = "pack://application:,,,/Assets/Existing Assets/" + tourBookingTransfer.cityLocation + imagecounter.ToString()+".jpg";

        }

        public void ShowCoupons()
        {

            DataBaseContext context = new DataBaseContext();
            List<Coupon> coupons = context.Coupons.ToList();

            List<CouponDTO> dataList = new List<CouponDTO>();
            int counter = 0;
            foreach (Coupon coup in coupons)
            {
                if (coup.userId == LoggedUser.id)
                {
                    counter += 1;
                    couponDTOs.Add(new CouponDTO(coup.id, "Coupon" + counter, coup.exiresOn));
                }
            }            
        }

        public void GetNextPhoto(object obj) 
        {
            if (imagecounter < 4)
            {
                imagecounter += 1;
                ImageSlider = "pack://application:,,,/Assets/Existing Assets/" + CityName + imagecounter.ToString() + ".jpg";
            }
        }

        public void GetPreviousPhoto(object obj)
        {
            if (imagecounter > 1)
            {
                imagecounter -= 1;
                ImageSlider = "pack://application:,,,/Assets/Existing Assets/" + CityName + imagecounter.ToString() + ".jpg";
            }
        }
        public void ExecuteCreateTourReport(object obj)
        {
            _mainViewModel.TourNameReport = TourName;
            _mainViewModel.CityNameReport = CityName;
            _mainViewModel.CityNameReport = CountryName;
            _mainViewModel.KeyPointsReport = KeyPoints;
            _mainViewModel.Duration = Duration;
            _mainViewModel.GuestNumberReport = GuestNumber;
            _mainViewModel.ExecuteGenerateReport(null);
        }
    }
}
