using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_Profile : UserControl
    {
        public TourGuide_Profile()
        {
            InitializeComponent();
            var viewModel = new TourGuide_ProfileViewModel();
            viewModel.ParentWindow = Window.GetWindow(this);
            DataContext = viewModel;
        }
    }
}
