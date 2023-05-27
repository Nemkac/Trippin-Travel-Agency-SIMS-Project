using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoCouponsViewModel : ViewModelBase
    {
        public ObservableCollection<CouponDTO> couponDTOs { get; set; } = new ObservableCollection<CouponDTO>();       

        private string usernameLabel;
        public string UsernameLabel
        {
            get { return usernameLabel; }
            set
            {
                if (usernameLabel != value)
                {
                    usernameLabel = value;
                    OnPropertyChanged(nameof(UsernameLabel));
                }
            }
        }

        private string usernameLabel2;
        public string UsernameLabel2
        {
            get { return usernameLabel2; }
            set
            {
                if (usernameLabel2 != value)
                {
                    usernameLabel2 = value;
                    OnPropertyChanged(nameof(UsernameLabel2));
                }
            }
        }

        private string accountType;
        public string AccountType
        {
            get { return accountType; }
            set
            {
                if (accountType != value)
                {
                    accountType = value;
                    OnPropertyChanged(nameof(AccountType));
                }
            }
        }

        private int toursVisitedLabel;
        public int ToursVisitedLabel
        {
            get { return toursVisitedLabel; }
            set
            {
                if (toursVisitedLabel != value)
                {
                    toursVisitedLabel = value;
                    OnPropertyChanged(nameof(ToursVisitedLabel));
                }
            }
        }
        private int numberOfCouponsLabel;
        public int NumberOfCouponsLabel
        {
            get { return numberOfCouponsLabel; }
            set
            {
                if (numberOfCouponsLabel != value)
                {
                    numberOfCouponsLabel = value;
                    OnPropertyChanged(nameof(NumberOfCouponsLabel));
                }
            }
        }

       
        public GuestTwoCouponsViewModel() {
            WindowLoaded();
        }
        public void WindowLoaded()
        {
            UsernameLabel = "Hello, " + LoggedUser.username + "!";
            UsernameLabel2 = "@" + LoggedUser.username;
            AccountType = "Account type:  " + LoggedUser.role;

            DataBaseContext context = new DataBaseContext();
            LoadCoupons(context);
            LoadData(context);
        }

        public void LoadCoupons(DataBaseContext context)
        {
            List<Coupon> coupons = context.Coupons.ToList(); 
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
        public void LoadData(DataBaseContext context)
        {
            foreach (Coupon coupon in context.Coupons.ToList())
            {
                if (coupon.userId == LoggedUser.id)
                {
                    NumberOfCouponsLabel++;
                }
            }
            foreach (TourAttendance attendance in context.TourAttendances.ToList())
            {
                if (attendance.guestID == LoggedUser.id)
                {
                    ToursVisitedLabel++;
                }
            }

        }

    }
}
