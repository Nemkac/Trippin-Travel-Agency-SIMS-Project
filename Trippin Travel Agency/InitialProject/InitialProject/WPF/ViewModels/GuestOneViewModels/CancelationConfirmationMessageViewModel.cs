using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{

    public class CancelationConfirmationMessageViewModel : ViewModelBase
    {
        public ViewModelCommand Ok { get; set; }
        public CancelationConfirmationMessageViewModel()
        {
            Ok = new ViewModelCommand(Continue);
        }
        public void Continue(object sender)
        {
            GuestOneStaticHelper.cancelationConfirmationMessageInterface.Close();
            GuestOneStaticHelper.futureBookingsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
        }
    }
}
