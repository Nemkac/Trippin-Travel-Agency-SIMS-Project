using InitialProject.Model;
using InitialProject.WPF.ViewModels;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Context;
using System;
using InitialProject.DTO;
using InitialProject.Model.TransferModels;
using System.Windows;

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Dashboard.xaml
    /// </summary>
    public partial class TourGuide_Dashboard : UserControl
    {
        public TourGuide_Dashboard()
        {
            InitializeComponent();
            DataContext = new TourGuide_DashboardViewModel();
        }
    }
}
