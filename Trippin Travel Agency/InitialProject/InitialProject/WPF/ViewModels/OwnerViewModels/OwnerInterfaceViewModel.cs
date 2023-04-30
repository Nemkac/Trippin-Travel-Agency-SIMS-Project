﻿using FontAwesome.Sharp;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.WPF.View;
using InitialProject.WPF.View.Owner_Views;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerInterfaceViewModel : ViewModelBase
    {
        //Fields
        private ViewModelBase _currentChildView;
        private ViewModelBase _secondChildView;
        private string _caption;

        private RequestDTO _requestDataGridSelectedItem;

        public int selectedYearForAnnualReport { get; set; }

        //Properties
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public ViewModelBase SecondChildView
        {
            get
            {
                return _secondChildView;
            }

            set
            {
                _secondChildView = value;
                OnPropertyChanged(nameof(SecondChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public RequestDTO RequestDataGridSelectedItem
        {
            get { return _requestDataGridSelectedItem; }
            set
            {
                _requestDataGridSelectedItem = value;
                OnPropertyChanged(nameof(RequestDataGridSelectedItem));
            }
        }

        //--> Commands
        public ICommand ShowAcceptDenyViewCommand { get; }
        public ICommand ShowReviewsViewCommand { get; }
        public ICommand ShowProfileViewCommand { get; }
        public ICommand ShowOwnersBookingViewCommand { get; }
        public ICommand ShowAccommodationRegistrationViewCommand { get; }
        public ICommand ShowRequestViewCommand { get; }
        public ICommand ShowNotificationsViewCommand { get; }
        public ICommand ShowMyBookingsCommand { get; }
        public ICommand ShowOurRecommendationsCommand { get; }
        public ICommand ShowAnnualStatisticsCommand { get; }
        public ICommand ShowAccommodationMonthlyStatistics { get; }
        public ICommand ShowRenovationsCommand { get; }
        public ICommand ShowNewRenovationCommand { get; }
        public ICommand ShowScheduleNewRenovationCommand { get; }
        public ICommand GenerateReportCommand { get; }

        public ICommand LogOut { get; }
        public OwnerInterfaceViewModel()
        {
            //Initialize commands
            ShowAcceptDenyViewCommand = new ViewModelCommand(ExecuteShowAcceptDenyViewCommand);
            ShowOwnersBookingViewCommand = new ViewModelCommand(ExecuteShowOwnersBookingViewCommand);
            ShowAccommodationRegistrationViewCommand = new ViewModelCommand(ExecuteShowAccommodationRegistrationViewCommand);
            ShowRequestViewCommand = new ViewModelCommand(ExecuteShowRequestViewCommand);
            ShowProfileViewCommand = new ViewModelCommand(ExecuteShowProfileViewCommand);
            ShowReviewsViewCommand = new ViewModelCommand(ExecuteShowReviewsViewCommand);
            ShowNotificationsViewCommand = new ViewModelCommand(ExecuteShowNotificationsViewCommand);
            ShowMyBookingsCommand = new ViewModelCommand(ExecuteShowMyBookingsCommand);
            ShowOurRecommendationsCommand = new ViewModelCommand(ExecuteShowOurRecommendationsViewCommand);
            ShowAccommodationMonthlyStatistics = new ViewModelCommand(ExecuteShowMonthlyStatistics);
            ShowRenovationsCommand = new ViewModelCommand(ExecuteShowRenovationsCommand);
            ShowNewRenovationCommand = new ViewModelCommand(ExecuteShowNewRenovationViewCommand);
            ShowScheduleNewRenovationCommand = new ViewModelCommand(ExecuteShowScheduleNewRenovationCommand);

            GenerateReportCommand = new ViewModelCommand(GenerateAnnualReport);

            //Default view
            LoggedUser._mainViewModel = this;
            ExecuteShowMyBookingsCommand(null);
        }

        public void ExecuteShowOwnersBookingViewCommand(object obj)
        {
            CurrentChildView = new OwnersBookingDisplayViewModel();
            Caption = "Bookings";
        }

        public void ExecuteShowAcceptDenyViewCommand(object obj)
        {
            CurrentChildView = new AcceptDenyViewModel();
            Caption = "Requests";
        }

        public void ExecuteShowRequestViewCommand(object obj)
        {
            CurrentChildView = new RequestViewModel();
            Caption = "Requests";
        }

        public void ExecuteShowAccommodationRegistrationViewCommand(object obj)
        {
            CurrentChildView = new AccommodationRegistrationViewModel();
            Caption = "New Accommodation";
        }

        public void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new ProfileViewModel();
            Caption = "Profile";
        }

        public void ExecuteShowReviewsViewCommand(object obj)
        {
            CurrentChildView = new ReviewsViewModel();
            Caption = "Reviews";
        }

        public void ExecuteShowNotificationsViewCommand(object obj)
        {
            CurrentChildView = new NotificationsViewModel();
            Caption = "Notifications";
        }

        public void ExecuteShowGuestRatingCommand(object obj)
        {
            CurrentChildView = new RateGuestViewModel();
            Caption = "Rate Guest";
        }

        public void ExecuteShowMyBookingsCommand(object obj)
        {
            CurrentChildView = new AccommodationStatisticsViewModel();
            Caption = "My Accommodations";
        }
        public void ExecuteShowOurRecommendationsViewCommand(object obj)
        {
            SecondChildView = new OurRecommendationsViewModel();
            Caption = "My Accommodations";
        }
        public void ExecuteShowAnnualStatisticsCommand(object obj)
        {
            CurrentChildView = new AccommodationAnnualStatisticsViewModel();
            Caption = "My Accommodations";
        }

        public void ExecuteShowMonthlyStatistics(object obj)
        {
            CurrentChildView = new AccommodationMonthlyStatisticsViewModel();
            Caption = "My Accommodations";
        }

        public void ExecuteShowRenovationsCommand(object obj)
        {
            CurrentChildView = new RenovationsViewModel();
            Caption = "Renovations";
        }

        public void ExecuteShowNewRenovationViewCommand(object obj)
        {
            CurrentChildView = new NewRenovationViewModel();
            Caption = "Renovations";
        }

        public void ExecuteShowScheduleNewRenovationCommand(object obj)
        {
            CurrentChildView = new ScheduleNewRenovationViewModel();
            Caption = "Renovations";
        }

        public void GenerateAnnualReport(object obj)
        {
            if(selectedYearForAnnualReport == 0)
            {
                MessageBox.Show("You must select a year before generating a report!");
                return;
            }
            else
            {
                AnnualStatisticsReport report = new AnnualStatisticsReport(selectedYearForAnnualReport);

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(report, "Report");
                }
            }
        }
    }
}