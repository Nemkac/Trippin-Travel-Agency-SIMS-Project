using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.GuestTwoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    static class LoggedUser
    {
        public static int id { get; set; } 

        public static string username { get; set; }

        public static string firstName { get; set; }
        public static string lastName { get; set; }
        public static string email { get; set; }

        public static string role { get; set; }
        public static TourGuide_MainViewModel TourGuide_MainViewModel { get; set; }
        public static GuestTwoInterfaceViewModel GuestTwoInterfaceViewModel { get; set; }


    }
}
