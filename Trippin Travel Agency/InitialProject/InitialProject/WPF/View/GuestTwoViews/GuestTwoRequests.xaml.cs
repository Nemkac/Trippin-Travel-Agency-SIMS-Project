using InitialProject.Context;
using InitialProject.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for GuestTwoRequests.xaml
    /// </summary>
    public partial class GuestTwoRequests : UserControl
    {
        public GuestTwoRequests()
        {
            InitializeComponent();
            this.Loaded += WindowLoaded;
        }
        public void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UsernameLabel.Content = "Hello, " + LoggedUser.username + "!";
            this.UsernameLabel2.Content = "@" + LoggedUser.username;
            this.AccountTypeLabel.Content = "Account type:  " + LoggedUser.role;

            DataBaseContext context = new DataBaseContext();
            LoadRequests(context);
        }

        public void LoadRequests(DataBaseContext context)
        {
            List<TourRequest> requests = new List<TourRequest>();
            foreach (TourRequest request in context.TourRequests.ToList())
            {
                if (LoggedUser.id == request.guestId)
                {
                    requests.Add(request);
                }
            }
            this.dataGrid.ItemsSource = requests;
        }
    }
}
