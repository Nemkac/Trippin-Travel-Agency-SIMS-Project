using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.TourGuideViews
{
    public partial class TourGuide_ToursToday : UserControl
    {
        //private TourService tourService;
        public TourGuide_ToursToday()
        {
            InitializeComponent();
            this.DataContext = new TourGuide_ToursTodayViewModel();
        }
    }
}
