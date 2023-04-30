using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.View.Owner_Views;
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
        public static int tourIdTransfer = -1;
        private TourLocationService tourLocationService;
        public GuestTwoMessages()
        {
            InitializeComponent();
            tourIdTransfer = -1;
            this.tourLocationService = new(new TourLocationRepository());
            this.Loaded += WindowLoaded;            
        }

        public void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UsernameLabel.Content = "Hello, " + LoggedUser.username + "!";
            this.UsernameLabel2.Content = "@" + LoggedUser.username;
            this.AccountTypeLabel.Content = "Account type:  " + LoggedUser.role;
            this.requestDataGrid.ItemsSource = null;
            DataBaseContext context = new DataBaseContext();    
            LoadMessages(context);
        }

        public void LoadMessages(DataBaseContext context) { 
            
            List<TourMessage> tourMessages = new List<TourMessage>();
            foreach (TourMessage message in context.TourMessages.ToList()) {
                if (LoggedUser.id == message.guestId) 
                {
                    tourMessages.Add(message);
                }
            }
            this.dataGrid.ItemsSource = tourMessages;
            LoadAcceptedTours(context);
            LoadNewTourAnnouncements(context);
            
        }
        private void LoadAcceptedTours(DataBaseContext context)
        {
            List<string> announcements = new List<string>();
            foreach (TourRequest tourRequest in context.TourRequests.ToList())
            {
                if (tourRequest.status == TourRequestStatus.Accepted && tourRequest.sent == false && tourRequest.guestId == LoggedUser.id)
                {
                    string poruka = "[AcceptedTour] Tour request accepted by guide: " + tourRequest.id.ToString() + ".  Set date: " + tourRequest.acceptedDate.ToString();
                    announcements.Add(poruka);
                    MessageBox.Show(poruka);
                    tourRequest.sent = true;
                    context.Update(tourRequest);
                    context.SaveChanges();
                    this.requestDataGrid.Items.Add(poruka);
                }
            }
        }
        private void LoadNewTourAnnouncements(DataBaseContext context) {
           
            foreach (UnfulfilledTourCities unfulfilledRequest in context.UnfulfilledTourCities.ToList()) {
                if (unfulfilledRequest.guestId == LoggedUser.id)
                {
                    foreach (Tour tour in context.Tours.ToList())
                    {
                        TourLocation location = this.tourLocationService.GetById(tour.location);
                        if (unfulfilledRequest.city == location.city) {
                            string poruka = "[NewTour] New tour with unfulfiled request: tourId: " + tour.id.ToString();
                            if (!this.requestDataGrid.Items.Contains(poruka))
                            {
                                this.requestDataGrid.Items.Add(poruka);
                            }
                           // context.UnfulfilledTourCities.Remove(unfulfilledRequest);
                           // context.SaveChanges();
                        }
                    }
                }
            }

            foreach (UnfulfilledTourCountries unfulfilledRequest in context.unfulfilledTourCountries.ToList())
            {
                if (unfulfilledRequest.guestId == LoggedUser.id)
                {
                    foreach (Tour tour in context.Tours.ToList())
                    {
                        TourLocation location = this.tourLocationService.GetById(tour.location);
                        if (unfulfilledRequest.country == location.country)
                        {
                            string poruka = "[NewTour] New tour with unfulfiled request: tourId: " + tour.id.ToString();
                            if (!this.requestDataGrid.Items.Contains(poruka))
                            {
                                this.requestDataGrid.Items.Add(poruka);
                            }
                           // context.unfulfilledTourCountries.Remove(unfulfilledRequest);
                           // context.SaveChanges();
                        }
                    }
                }
            }

            foreach (UnfulfilledTourLanguages unfulfilledRequest in context.UnfulfilledTourLanguages.ToList())
            {
                if (unfulfilledRequest.guestId == LoggedUser.id)
                {
                    foreach (Tour tour in context.Tours.ToList())
                    {
                        if (unfulfilledRequest.language == tour.language)
                        {
                            string poruka = "[NewTour] New tour with unfulfiled request: tourId: " + tour.id.ToString();
                            if (!this.requestDataGrid.Items.Contains(poruka))
                            {
                                this.requestDataGrid.Items.Add(poruka);
                            }
                           // context.UnfulfilledTourLanguages.Remove(unfulfilledRequest);
                           // context.SaveChanges();
                        }
                    }
                }
            }

            context.SaveChanges();               
        }               
        private void OpenMessage(object sender, RoutedEventArgs e)
        {
            TourMessage tourMessage = this.dataGrid.SelectedItem as TourMessage;
            DataBaseContext context = new DataBaseContext();
            if (tourMessage != null)
            {
                TourAttendance tourAttendance = new TourAttendance(tourMessage.tourId,tourMessage.keyPointId,tourMessage.guestId,tourMessage.numberOfGuests);
                context.TourAttendances.Add(tourAttendance);
                //context.TourMessages.Remove(tourMessage);
                context.SaveChanges();
            }
        }
        private void PreviewTour(object sender, RoutedEventArgs e) 
        {
            string message = this.requestDataGrid.SelectedItem.ToString();
            if (message.Substring(0, 9) == "[NewTour]") 
            {
                tourIdTransfer = Int32.Parse(message.Substring(message.Length - 2));
            }
        }
    }
}
