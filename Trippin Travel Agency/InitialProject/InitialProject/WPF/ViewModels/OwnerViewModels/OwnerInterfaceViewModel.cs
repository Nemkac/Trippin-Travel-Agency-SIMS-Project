using FontAwesome.Sharp;
using InitialProject.DTO;
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
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerInterfaceViewModel : ViewModelBase
    {
        //Fields
        private ViewModelBase _currentChildView;
        private string _caption;

        private RequestDTO _requestDataGridSelectedItem;

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
            //Default view
            ExecuteShowOwnersBookingViewCommand(null);
        }

        public void ExecuteShowOwnersBookingViewCommand(object obj)
        {
            CurrentChildView = new OwnersBookingDisplayViewModel();
            Caption = "Bookings";
        }

        public void ExecuteShowAcceptDenyViewCommand(object obj)
        {
            CurrentChildView = new AcceptDenyViewModel(this, _requestDataGridSelectedItem);
            Caption = "Requests";
        }

        public void ExecuteShowRequestViewCommand(object obj)
        {
            CurrentChildView = new RequestViewModel(this);
            Caption = "Requests";
        }

        public void ExecuteShowAccommodationRegistrationViewCommand(object obj)
        {
            CurrentChildView = new AccommodationRegistrationViewModel();
            Caption = "New Accommodation";
        }

        public void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new ProfileViewModel(this);
            Caption = "Profile";
        }

        public void ExecuteShowReviewsViewCommand(object obj)
        {
            CurrentChildView = new ReviewsViewModel();
            Caption = "Reviews";
        }

        public void ExecuteShowNotificationsViewCommand(object obj)
        {
            CurrentChildView = new NotificationsViewModel(this);
            Caption = "Notifications";
        }

        public void ExecuteShowGuestRatingCommand(object obj)
        {
            CurrentChildView = new RateGuestViewModel();
            Caption = "Rate Guest";
        }
    }
}