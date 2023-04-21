using InitialProject.Model;
using InitialProject.WPF.ViewModels;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Controls;

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
            this.usernameTextBlock.Text = LoggedUser.username; 
        }
    }
}
