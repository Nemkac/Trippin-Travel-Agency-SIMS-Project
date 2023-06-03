using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Input;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.GuestTwoViewModels;
using InitialProject.Model;
using InitialProject.WPF.View.GuestTwoViews;
using System.Windows.Controls;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoInterfaceViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;

        public string TourNameReport { get; set; }
        public string CityNameReport { get; set; }
        public string CountryNameReport { get; set; }

        public string KeyPointsReport { get; set; } 

        public int GuestNumberReport { get; set; }
        public int Duration { get; set; }


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


        public ICommand ShowTourViewCommand { get; }
        public ICommand ShowGuestTwoCoupons { get; }
        public ICommand ShowBookingConfirmation { get; }
        public ICommand ShowGuestTwoMessages { get; }
        public ICommand ShowLiveTour { get; }
        public ICommand ShowCreateYourOwnTour { get; }
        public ICommand ShowGuestTwoRequests { get; }
        public ICommand ShowGuestTwoStatistics { get; }

        public ICommand GenerateReport { get; }

        public ICommand ExecuteSignOut { get; }


        public GuestTwoInterfaceViewModel()
        {
            ShowTourViewCommand = new ViewModelCommand(ExecuteTourViewCommand);
            //Default view
            ShowGuestTwoCoupons = new ViewModelCommand(ExecuteShowGuestTwoCoupons);

            ShowLiveTour = new ViewModelCommand(ExecuteShowLiveTour);

            ShowGuestTwoMessages = new ViewModelCommand(ExecuteShowGuestTwoMessages);

            ShowCreateYourOwnTour = new ViewModelCommand(ExecuteShowCreateTourOwnTourCommand);

            ShowGuestTwoRequests = new ViewModelCommand(ExecuteShowGuestTwoRequests);

            ShowGuestTwoStatistics = new ViewModelCommand(ExecuteShowGuestTwoStatistics);

            GenerateReport = new ViewModelCommand(ExecuteGenerateReport);

            ExecuteSignOut = new ViewModelCommand(SignOut);

            LoggedUser.GuestTwoInterfaceViewModel = this;

        }

        public void ExecuteTourViewCommand(object obj)
        {
            CurrentChildView = new TourDisplayViewModel();

        }
        public void ExecuteShowDetailedTourView(object obj)
        {

            CurrentChildView = new DetailedTourViewModel();

        }

        public void ExecuteShowGuestTwoCoupons(object obj)
        {
            CurrentChildView = new GuestTwoCouponsViewModel();
        }

        public void ExecuteShowBookingConfirmation(object obj)
        {
            CurrentChildView = new TourConfirmationViewModel(); // OVO MENJAJ NA NOVO IZBACIVANJE 
        }

        public void ExecuteShowLiveTour(object obj)
        {
            CurrentChildView = new JoinLiveTourViewModel();
        }

        public void ExecuteShowGuestTwoMessages(object obj)
        {
            CurrentChildView = new GuestTwoMesagesViewModel();
        }

        public void ExecuteShowCreateTourOwnTourCommand(object obj)
        {
            CurrentChildView = new CreateYourOwnTourViewModel();
        }
        public void ExecuteShowGuestTwoRequests(object obj)
        {
            CurrentChildView = new GuestTwoRequestsViewModel(); 
        }

        public void ExecuteShowGuestTwoStatistics(object obj)
        {
            CurrentChildView = new GuestTwoStatisticsViewModel();
        }

        public void ExecuteGenerateReport(object obj)
        {
            GuestTwoReport report = new GuestTwoReport(TourNameReport, CityNameReport, CountryNameReport, KeyPointsReport, Duration, GuestNumberReport);
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(report, "Report");
            }
        }
        public void SignOut(object obj)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window?.Close();
            SignInForm form = new SignInForm();
            form.Show();

        }

    }
}
