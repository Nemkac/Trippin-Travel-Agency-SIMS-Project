using InitialProject.Model;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class SendSuggestionConfirmationViewModel : ViewModelBase
    {
        public ViewModelCommand GoToPastBookings { get; set; }
        public SendSuggestionConfirmationViewModel()
        {
            GoToPastBookings = new ViewModelCommand(GoPastBookings);
        }
        public void GoPastBookings(object sender)
        {
            GuestOneStaticHelper.pastBookingsInterface.Show();
            GuestOneStaticHelper.sendSuggestionConfirmationInterface.Close();
            GuestOneStaticHelper.renovationSuggestionInterface.Close();
        }
    }
}
