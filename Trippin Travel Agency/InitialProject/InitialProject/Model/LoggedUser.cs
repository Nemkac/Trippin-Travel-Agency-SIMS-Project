using InitialProject.WPF.View.Owner_Views;
using InitialProject.WPF.ViewModels.OwnerViewModels;
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

        public static OwnerInterfaceViewModel _mainViewModel { get; set; } 

        public static AccommodationsAnnualStatisticsView accommodationsAnnualStatisticsView { get; set; } 

        public static bool IsChecked { get; set; }
    }
}
