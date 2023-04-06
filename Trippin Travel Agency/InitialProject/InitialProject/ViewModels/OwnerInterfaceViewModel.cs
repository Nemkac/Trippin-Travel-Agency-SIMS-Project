using FontAwesome.Sharp;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.View.Owner_Views;
using InitialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class OwnerInterfaceViewModel : ViewModelBase
    {
        //Fields
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

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

        public IconChar Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowAcceptDenyViewCommand { get; }
        public ICommand ShowOwnersBookingViewCommand { get; }
        public ICommand ShowAccommodationRegistrationViewCommand { get; }
        public ICommand ShowRequestViewCommand { get; }
        public OwnerInterfaceViewModel()
        {
            //Initialize commands
            ShowAcceptDenyViewCommand = new ViewModelCommand(ExecuteShowAcceptDenyViewCommand);
            ShowOwnersBookingViewCommand = new ViewModelCommand(ExecuteShowOwnersBookingViewCommand);
            ShowAccommodationRegistrationViewCommand = new ViewModelCommand(ExecuteShowAccommodationRegistrationViewCommand);
            ShowRequestViewCommand = new ViewModelCommand(ExecuteShowRequestViewCommand);
            //Default view
            ExecuteShowOwnersBookingViewCommand(null);
        }

        public void ExecuteShowOwnersBookingViewCommand(object obj)
        {
            CurrentChildView = new OwnersBookingDisplayViewModel();
            Caption = "Bookings";
            Icon = IconChar.Bookmark;
        }

        public void ExecuteShowAcceptDenyViewCommand(object obj)
        {
            CurrentChildView = new AcceptDenyViewModel();
     
            Caption = "Requests";
            Icon = IconChar.ArrowDown;
        }

        public void ExecuteShowRequestViewCommand(object obj)
        {
            CurrentChildView = new RequestViewModel(this);
            Caption = "Requests";
            Icon = IconChar.ArrowDown;
        }

        public void ExecuteShowAccommodationRegistrationViewCommand(object obj)
        {
            CurrentChildView = new AccommodationRegistrationViewModel();
            Caption = "New Accommodation";
            Icon = IconChar.Plus;
        }
    }
}