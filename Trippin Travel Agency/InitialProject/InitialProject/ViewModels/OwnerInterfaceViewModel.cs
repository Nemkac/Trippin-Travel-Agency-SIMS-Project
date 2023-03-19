using FontAwesome.Sharp;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class MainViewModel : ViewModelBase
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
        public ICommand ShowOwnerInterfaceViewCommand { get; }
        public ICommand ShowAccommodationRegistrationViewCommand { get; }

        public MainViewModel()
        {
            //Initialize commands
            ShowOwnerInterfaceViewCommand = new ViewModelCommand(ExecuteShowOwnerInterfaceViewCommand);
            ShowAccommodationRegistrationViewCommand = new ViewModelCommand(ExecuteShowAccommodationRegistrationViewCommand);

            //Default view
            ExecuteShowOwnerInterfaceViewCommand(null);
        }

        private void ExecuteShowOwnerInterfaceViewCommand(object obj)
        {
            CurrentChildView = new OwnersBookingDisplayViewModel();
            Caption = "Bookings";
            Icon = IconChar.Bookmark;
        }

        private void ExecuteShowAccommodationRegistrationViewCommand(object obj)
        {
            CurrentChildView = new AccommodationRegistrationViewModel();
            Caption = "New Accommodation";
            Icon = IconChar.FloppyDisk;
        }
    }
}