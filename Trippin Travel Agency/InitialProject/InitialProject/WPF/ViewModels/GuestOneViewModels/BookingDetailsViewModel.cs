using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class BookingDetailsViewModel : ViewModelBase
    {
        public BookingDetailsViewModel()
        {
            Debug = GuestOneStaticHelper.selectedBooking.Id.ToString();
        }


        private string debug;
        public string Debug
        {
            get { return debug; }
            set
            {
                if (debug != value)
                {
                    debug = value;
                    OnPropertyChanged(nameof(Debug));
                }
            }
        }
    }
}
