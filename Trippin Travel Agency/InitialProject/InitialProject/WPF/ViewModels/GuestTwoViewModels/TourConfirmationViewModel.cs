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

        private readonly GuestTwoInterfaceViewModel _mainViewModel;
        public ViewModelCommand CancelBookingCommand { get; private set; }
        public ViewModelCommand TourViewCommand { get; private set; }

        public TourConfirmationViewModel()
        {
            _mainViewModel = LoggedUser.GuestTwoInterfaceViewModel;
            WindowLoaded();
            FinishDisabled = true;
            CancelDisabled = true;
            TourViewCommand = new ViewModelCommand(ShowDetailedTourView);
            CancelBookingCommand = new ViewModelCommand(CancelBooking);
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

            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.First();
            Tour tour = context.Tours.SingleOrDefault(t => t.id == tourBookingTransfer.id);
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
            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.First();
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
            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.First();
            TourName = tourBookingTransfer.name;
            CityName = tourBookingTransfer.cityLocation;
            CountryName = tourBookingTransfer.countryLocation;
            KeyPoints = tourBookingTransfer.keypoints;
            GuestNumber = tourBookingTransfer.numberOfGuests;
            Duration = tourBookingTransfer.hoursDuration;

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
    }
}
