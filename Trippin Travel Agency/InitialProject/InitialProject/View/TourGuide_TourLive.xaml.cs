using InitialProject.Context;
using InitialProject.Model.TransferModels;
using InitialProject.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View
{
    public partial class TourGuide_TourLive : UserControl
    {
        public TourGuide_TourLive()
        {
            InitializeComponent();
            this.Loaded += tourDataLoaded; 
        }

        public void tourDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            this.headerTextBlock.Text = requests.Last().tourId.ToString();
        }

    }
}
