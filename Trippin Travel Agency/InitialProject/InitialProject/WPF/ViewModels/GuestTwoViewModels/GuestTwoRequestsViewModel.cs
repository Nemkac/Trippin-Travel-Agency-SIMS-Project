using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.WPF.View.Owner_Views;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoRequestsViewModel : ViewModelBase
    {
        public ObservableCollection<TourRequestDTO> requests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ObservableCollection<TourRequestDTO> complexRequests { get; set; } = new ObservableCollection<TourRequestDTO>();


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

        private int numberOfCoupons;
        public int NumberOfCoupons
        {
            get { return numberOfCoupons; }
            set
            {
                if (numberOfCoupons != value)
                {
                    numberOfCoupons = value;
                    OnPropertyChanged(nameof(NumberOfCoupons));
                }
            }
        }

        private int numberOfVisitedTours;
        public int NumberOfVisitedTours
        {
            get { return numberOfVisitedTours; }
            set
            {
                if (numberOfVisitedTours != value)
                {
                    numberOfVisitedTours = value;
                    OnPropertyChanged(nameof(NumberOfVisitedTours));
                }
            }
        }


        public GuestTwoRequestsViewModel()
        { 
            WindowLoaded();
        }
        public void WindowLoaded()
        {
            UsernameLabel = "Hello, " + LoggedUser.username + "!";
            UsernameLabel2 = "@" + LoggedUser.username;
            AccountType = "Account type:  " + LoggedUser.role;

            DataBaseContext context = new DataBaseContext();
            LoadData(context);
            LoadRequests(context);
        }
        public void LoadRequests(DataBaseContext context)
        {
            
            foreach (TourRequest request in context.TourRequests.ToList())
            {
                if (LoggedUser.id == request.guestId)
                {
                    requests.Add(new TourRequestDTO(request.city,request.country,request.language,request.startDate.ToShortDateString(),request.endDate.ToShortDateString(),request.status));
                }
            }

            foreach (ComplexTourRequest complexRequest in context.ComplexTourRequests.ToList()) 
            {
                foreach (TourRequest request in complexRequest.singleRequestIds)
                {

                
                    DateTime date = Convert.ToDateTime(request.acceptedDate);                    
                    string acceptedDate;

                    if (date.ToShortDateString().Equals("1/1/0001"))
                    {
                        acceptedDate = "-";
                    }
                    else {
                        acceptedDate = date.ToShortDateString();
                    }
                    complexRequests.Add(new TourRequestDTO(request.city,
                                                           request.country,
                                                           request.language,
                                                           request.startDate.ToShortDateString(),
                                                           request.endDate.ToShortDateString(),
                                                           acceptedDate.ToString(),
                                                           request.status));
                }
            }
        }

        public void LoadData(DataBaseContext context) 
        {
            foreach (Coupon coupon in context.Coupons.ToList()) 
            {
                if (coupon.userId == LoggedUser.id) 
                {
                    NumberOfCoupons++;
                }
            }
            foreach (TourAttendance attendance in context.TourAttendances.ToList())
            {
                if (attendance.guestID == LoggedUser.id)
                {
                    NumberOfVisitedTours++;
                }
            }

        }
    }
}
