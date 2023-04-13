using InitialProject.Context;
using InitialProject.DTO;
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
    /// Interaction logic for GuestTwoMessages.xaml
    /// </summary>
    public partial class GuestTwoMessages : UserControl
    {
        public GuestTwoMessages()
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
            LoadMessages(context);
        }

        public void LoadMessages(DataBaseContext context) { 
            
            List<TourMessage> tourMessages = new List<TourMessage>();
            foreach (TourMessage message in context.TourMessages.ToList()) {
                if (LoggedUser.id == message.guestId) {
                    tourMessages.Add(message);
                }
            }
            this.dataGrid.ItemsSource = tourMessages;
        }

        private void OpenMessage(object sender, RoutedEventArgs e)
        {
            TourMessage tourMessage = this.dataGrid.SelectedItem as TourMessage;
            DataBaseContext context = new DataBaseContext();
            if (tourMessage != null)
            {
                TourAttendance tourAttendance = new TourAttendance(tourMessage.tourId,tourMessage.keyPointId,tourMessage.guestId,tourMessage.numberOfGuests);
                context.TourAttendances.Add(tourAttendance);
               // context.TourMessages.Remove(tourMessage);
                context.SaveChanges();
            }
        }
    }
}
