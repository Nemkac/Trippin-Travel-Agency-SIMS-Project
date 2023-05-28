using FontAwesome.Sharp;
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
using System.Windows.Media;

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

        private SolidColorBrush _menuColor;
        public SolidColorBrush MenuColor 
        {
            get { return _menuColor; }
            set
            {
                _menuColor = value;
                OnPropertyChanged(nameof(MenuColor));
            }
        }


        private SolidColorBrush _menuActiveButtonColor;
        public SolidColorBrush MenuActiveButtonColor
        {
            get { return _menuActiveButtonColor; }
            set
            {
                _menuActiveButtonColor = value;
                OnPropertyChanged(nameof(MenuActiveButtonColor));
            }
        }

        private SolidColorBrush _contentSectionBackgroundColor;
        public SolidColorBrush ContentSectionBackgroundColor
        {
            get { return _contentSectionBackgroundColor; }
            set
            {
                _contentSectionBackgroundColor = value;
                OnPropertyChanged(nameof(ContentSectionBackgroundColor));
            }
        }

        private SolidColorBrush _captionHeaderText;
        public SolidColorBrush CaptionHeaderText
        {
            get { return _captionHeaderText; }
            set
            {
                _captionHeaderText = value;
                OnPropertyChanged(nameof(CaptionHeaderText));
            }
        }

        private SolidColorBrush _headerButtonIconColor;
        public SolidColorBrush HeaderButtonIconColor
        {
            get { return _headerButtonIconColor; }
            set
            {
                _headerButtonIconColor = value;
                OnPropertyChanged(nameof(HeaderButtonIconColor));
            }
        }

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }

        private SolidColorBrush _darkThemeChecked;
        public SolidColorBrush DarkThemeChecked
        {
            get { return _darkThemeChecked; }
            set
            {
                _darkThemeChecked = value;
                OnPropertyChanged(nameof(DarkThemeChecked));
            }
        }

        private SolidColorBrush _lightThemeChecked;
        public SolidColorBrush LightThemeChecked
        {
            get { return _lightThemeChecked; }
            set
            {
                _lightThemeChecked = value;
                OnPropertyChanged(nameof(LightThemeChecked));
            }
        }

        private SolidColorBrush _engChecked;
        public SolidColorBrush EngChecked
        {
            get { return _engChecked; }
            set
            {
                _engChecked = value;
                OnPropertyChanged(nameof(EngChecked));
            }
        }

        private SolidColorBrush _srbChecked;
        public SolidColorBrush SrbChecked
        {
            get { return _srbChecked; }
            set
            {
                _srbChecked = value;
                OnPropertyChanged(nameof(SrbChecked));
            }
        }


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

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                LoggedUser.IsChecked = _isChecked;
                LightOrDarkTheme();
                OnPropertyChanged(nameof(IsChecked));

                Mediator.OnIsCheckedChanged(_isChecked);
            }
        }

        private bool _isLanguageChecked;

        public bool IsLanguageChecked
        {
            get { return _isLanguageChecked; }
            set
            {
                _isLanguageChecked = value;
                LoggedUser.IsLanguageChecked = _isLanguageChecked;
                EngOrSrbLanguage();
                OnPropertyChanged(nameof(IsLanguageChecked));

                Mediator.OnIsLanguageCheckedChanged(IsLanguageChecked);
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
        public ICommand SignOutCommand { get; }
        public ICommand CreateAccommodationFromRecommendations { get; }
        public ICommand CloseExistingAccommodationCommand { get; }
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
            CloseExistingAccommodationCommand = new ViewModelCommand(ExecuteCloseExistingAccommodationCommand);
            //SignOutCommand = new ViewModelCommand(ExecuteSignOutCommand);
            IsChecked = false;
            IsLanguageChecked = false;
            Mediator.OnIsCheckedChanged(IsChecked);
            Mediator.OnIsUserLogged(LoggedUser.id);
            Mediator.OnIsLanguageCheckedChanged(IsLanguageChecked);
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
            LoggedUser.creatingAccommodationFromRecomendation = false;
            CurrentChildView = new AccommodationRegistrationViewModel();
            Caption = "New Accommodation";
        }

        public void ExecuteCreateNewAccommodationFromRecommendationViewCommand(object obj)
        {
            LoggedUser.creatingAccommodationFromRecomendation = true;
            CurrentChildView = new AccommodationRegistrationViewModel();
            Caption = "New Accommodation";
        }

        public void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new ProfileViewModel();
            Caption = "Profile";
        }

        public void ExecuteCloseExistingAccommodationCommand(object obj)
        {
            CurrentChildView = new CloseExistingAccommodationViewModel();
            Caption = "Close Accommodation";
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

        /*public void ExecuteSignOutCommand(object obj)
        {
            
        }*/

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

        public void LightOrDarkTheme()
        {
            if(IsChecked == true)
            {
                MenuColor = (SolidColorBrush)Application.Current.Resources["DarkNavigationMenuColor"];
                MenuActiveButtonColor = (SolidColorBrush)Application.Current.Resources["DarkActiveButtonColor"];
                ContentSectionBackgroundColor = (SolidColorBrush)Application.Current.Resources["DarkContentSectionBackgroundColor"];
                CaptionHeaderText = (SolidColorBrush)Application.Current.Resources["DarkCaptionHeaderText"];
                HeaderButtonIconColor = (SolidColorBrush)Application.Current.Resources["DarkHeaderButtonIconColor"];
                ContentTextColor = "F4F6F8";
                LightThemeChecked = (SolidColorBrush)Application.Current.Resources["ModeUnchecked"];
                DarkThemeChecked = (SolidColorBrush)Application.Current.Resources["ModeChecked"];
            }
            else
            {
                MenuColor = (SolidColorBrush)Application.Current.Resources["NavigationMenuColor"];
                MenuActiveButtonColor = (SolidColorBrush)Application.Current.Resources["ActiveButtonColor"];
                ContentSectionBackgroundColor = (SolidColorBrush)Application.Current.Resources["ContentSectionBackgroundColor"];
                CaptionHeaderText = (SolidColorBrush)Application.Current.Resources["CaptionHeaderText"];
                HeaderButtonIconColor = (SolidColorBrush)Application.Current.Resources["HeaderButtonIconColor"];
                ContentTextColor = "#192a56";
                DarkThemeChecked = (SolidColorBrush)Application.Current.Resources["ModeUnchecked"];
                LightThemeChecked = (SolidColorBrush)Application.Current.Resources["ModeChecked"];

            }
        }

        public void EngOrSrbLanguage()
        {
            if(IsLanguageChecked == true)
            {
                SrbChecked = (SolidColorBrush)Application.Current.Resources["ModeChecked"];
                EngChecked = (SolidColorBrush)Application.Current.Resources["ModeUnchecked"];
            }
            else
            {
                EngChecked = (SolidColorBrush)Application.Current.Resources["ModeChecked"];
                SrbChecked = (SolidColorBrush)Application.Current.Resources["ModeUnchecked"];
            }
        }
    }
}