using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class RenovationSuggestionViewModel : ViewModelBase
    {
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand GoToPreviousWindow { get; set; }
        public RenovationSuggestionViewModel()
        {
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            GoToPreviousWindow = new ViewModelCommand(ShowPastBookings);
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.renovationSuggestionInterface.Left + (GuestOneStaticHelper.renovationSuggestionInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.renovationSuggestionInterface.Top + (GuestOneStaticHelper.renovationSuggestionInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.renovationSuggestionInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        public void ShowPastBookings(object sender)
        {
            GuestOneStaticHelper.pastBookingsInterface.Top = GuestOneStaticHelper.renovationSuggestionInterface.Top;
            GuestOneStaticHelper.pastBookingsInterface.Left = GuestOneStaticHelper.renovationSuggestionInterface.Left;
            GuestOneStaticHelper.renovationSuggestionInterface.Hide();
            GuestOneStaticHelper.pastBookingsInterface.Show();
        }
    }
}
