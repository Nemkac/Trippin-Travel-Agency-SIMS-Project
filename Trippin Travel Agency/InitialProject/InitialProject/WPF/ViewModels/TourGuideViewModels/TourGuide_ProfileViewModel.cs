using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_ProfileViewModel : ViewModelBase
    {
        public string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public string userLastName;
        public string UserLastName
        {
            get { return userLastName; }
            set
            {
                if (userLastName != value)
                {
                    userLastName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        private Window parentWindow;
        public Window ParentWindow
        {
            get { return parentWindow; }
            set
            {
                if (parentWindow != value)
                {
                    parentWindow = value;
                    OnPropertyChanged(nameof(ParentWindow));
                }
            }
        }

        public ViewModelCommand ShowDashboardCommand { get; private set; }
        public ViewModelCommand LogoutCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ProfileViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
            LogoutCommand = new ViewModelCommand(Logout);
            userName = LoggedUser.firstName; 
            userLastName = LoggedUser.lastName;
        }
        public void Logout(object parameter)
        {
            ParentWindow?.Close();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
        }
        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }

    }
}
