using InitialProject.WPF.ViewModels;
﻿using InitialProject.WPF.View.Owner_Views;
using InitialProject.WPF.ViewModels.OwnerViewModels;
using InitialProject.WPF.ViewModels.GuestTwoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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


        public static OwnerInterfaceViewModel _mainViewModel { get; set; } 

        public static AccommodationsAnnualStatisticsView accommodationsAnnualStatisticsView { get; set; } 

        public static bool IsChecked { get; set; }
        public static bool IsLanguageChecked { get; set; }
        public static SolidColorBrush ContentTextColor { get; set; }

        public static bool creatingAccommodationFromRecomendation { get; set; }
        public static string MostPopularCity { get; set; }
        public static string MostPopularCountry { get; set; }
        public static string LeastPopularCountry { get; set; }
        public static string LeastPopularCity { get; set; }

        public static int VisitedForumId { get; set; }
        public static int commentIdToReport { get; set; }
    }
}
