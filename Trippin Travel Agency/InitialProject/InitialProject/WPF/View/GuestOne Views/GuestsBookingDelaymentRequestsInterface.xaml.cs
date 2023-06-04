using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.WPF.ViewModels.GuestOneViewModels;


namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for GuestsBookingDelaymentRequestsInterface.xaml
    /// </summary>
    public partial class GuestsBookingDelaymentRequestsInterface : Window
    {
        private BookingDelaymentRequestService bookingDelaymentRequestService;
        private AccommodationRepository accommodationRepository;
        private BookingService bookingService;
        private AccommodationService accommodationService;
        bool isHelpOn = false;
        public GuestsBookingDelaymentRequestsInterface()
        {
            InitializeComponent();
            this.DataContext = new GuestsBookingDelaymentRequestsViewModel();
            GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface = this;
            this.Loaded += ShowDelaymentRequests;
            this.accommodationRepository = new AccommodationRepository();
            this.accommodationService = new AccommodationService(accommodationRepository);
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            BookingDelaymentRequestRepository bookingDelaymentRequestRepository = new BookingDelaymentRequestRepository();
            this.bookingDelaymentRequestService = new BookingDelaymentRequestService(bookingDelaymentRequestRepository);
        }

        public void ShowDelaymentRequests(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            UserService userService = new UserService();
            List<BookingDelaymentRequest> resolvedDelaymentRequests = userService.GetResolvedBookingDelaymentRequests();
            List<BookingDelaymentRequest> pendingDelaymentRequests = userService.GetPendingBookingDelaymentRequests();

            if (resolvedDelaymentRequests != null)
            {
                FillResolvedRequestsGrid(bookingService, accommodationService, resolvedDelaymentRequests);

            } else
            {
                resolvedRequestsGrid.ItemsSource = null;
            }

            if (pendingDelaymentRequests != null)
            {
                FillPendingRequestsGrid(bookingService, accommodationService, pendingDelaymentRequests);
            } else
            {
                pendingRequestsGrid.ItemsSource = null;
            }
        }

        private void FillPendingRequestsGrid(BookingService bookingService, AccommodationService accommodationService, List<BookingDelaymentRequest> pendingDelaymentRequests)
        {
            var pendingDelaymentsToGrid = from bookingDelaymentRequest in pendingDelaymentRequests
                                          select new
                                          {
                                              bookingId = bookingDelaymentRequest.bookingId,
                                              accommodationService.GetById(bookingService.GetById(bookingDelaymentRequest.bookingId).accommodationId).name,
                                              desiredArrival = bookingDelaymentRequest.newArrival.ToString().Substring(0, 9),
                                              desiredDeparture = bookingDelaymentRequest.newDeparture.ToString().Substring(0, 9),
                                              bookingDelaymentRequest.status
                                          };
            this.pendingRequestsGrid.ItemsSource = pendingDelaymentsToGrid;
        }

        private void FillResolvedRequestsGrid(BookingService bookingService, AccommodationService accommodationService, List<BookingDelaymentRequest> resolvedDelaymentRequests)
        {
            var resolvedDelaymentsToGrid = from bookingDelaymentRequest in resolvedDelaymentRequests
                                           select new
                                           {
                                               bookingId = bookingDelaymentRequest.bookingId,
                                               accommodationService.GetById(bookingService.GetById(bookingDelaymentRequest.bookingId).accommodationId).name,
                                               desiredArrival = bookingDelaymentRequest.newArrival.ToString().Substring(0, 9),
                                               desiredDeparture = bookingDelaymentRequest.newDeparture.ToString().Substring(0, 9),
                                               bookingDelaymentRequest.status
                                           };
            this.resolvedRequestsGrid.ItemsSource = resolvedDelaymentsToGrid;
        }

        private void ShowResolvedRequestComment(object sender, RoutedEventArgs e)
        {
            DelaymentRequestComment delaymentRequestComment = new DelaymentRequestComment();
            UserService userService = new UserService();

            int selectedRowIndex = resolvedRequestsGrid.SelectedIndex;

            OpenResolvedtRequestComment(delaymentRequestComment, userService, selectedRowIndex);
        }

        private void OpenResolvedtRequestComment(DelaymentRequestComment delaymentRequestComment, UserService userService, int selectedRowIndex)
        {
            delaymentRequestComment.WindowStartupLocation = WindowStartupLocation.Manual;
            delaymentRequestComment.Left = this.Left + (this.Width - delaymentRequestComment.Width) / 2;
            delaymentRequestComment.Top = this.Top + (this.Height - delaymentRequestComment.Height) / 2;
            delaymentRequestComment.Topmost = true;
            delaymentRequestComment.ownersComment.Text = userService.GetResolvedBookingDelaymentRequests()[selectedRowIndex].comment;
            delaymentRequestComment.Show();
        }

        private void ShowPendingRequestComment(object sender, RoutedEventArgs e)
        {
            DelaymentRequestComment delaymentRequestComment = new DelaymentRequestComment();
            UserService userService = new UserService();

            int selectedRowIndex = pendingRequestsGrid.SelectedIndex;

            OpenPendingRequestComment(delaymentRequestComment, userService, selectedRowIndex);
        }

        private void OpenPendingRequestComment(DelaymentRequestComment delaymentRequestComment, UserService userService, int selectedRowIndex)
        {
            delaymentRequestComment.WindowStartupLocation = WindowStartupLocation.Manual;
            delaymentRequestComment.Left = this.Left + (this.Width - delaymentRequestComment.Width) / 2;
            delaymentRequestComment.Top = this.Top + (this.Height - delaymentRequestComment.Height) / 2;
            delaymentRequestComment.Topmost = true;
            delaymentRequestComment.ownersComment.Text = userService.GetPendingBookingDelaymentRequests()[selectedRowIndex].comment;
            delaymentRequestComment.Show();
        }

        private void DeleteResolvedRequest(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = resolvedRequestsGrid.SelectedIndex;
            UserService userService = new UserService();
            bookingDelaymentRequestService.Delete(userService.GetResolvedBookingDelaymentRequests()[selectedRowIndex]);
            ShowDelaymentRequests(sender, e);

        }

        private void DeletePendingRequest(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = pendingRequestsGrid.SelectedIndex;
            UserService userService = new UserService();
            bookingDelaymentRequestService.Delete(userService.GetPendingBookingDelaymentRequests()[selectedRowIndex]);
            ShowDelaymentRequests(sender, e);
        }

        public void KeyHandler(object s, System.Windows.Input.KeyEventArgs k)
        {
            
            if(Keyboard.Modifiers == ModifierKeys.Control && k.Key == Key.H)
            {
                if(!isHelpOn)
                {
                    HelpLand.Text = "On the left are shown pending requets, and on the right are shown resolved requests";
                    HelpExit.Text = "To exit Help, press CTRL + H again";
                    isHelpOn = true;
                }
                else
                {
                    HelpLand.Text = string.Empty;
                    HelpExit.Text = string.Empty;
                    isHelpOn = false;
                }
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && k.Key == Key.N)
            {
                Navigator navigator = new Navigator();
                navigator.Left = GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface.Left + (GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface.Width - navigator.Width) / 2;
                navigator.Top = GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface.Top + (GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface.Height - navigator.Height) / 2;
                GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                navigator.Show();
            }

            if(k.Key == Key.P)
            {
                pendingRequestsGrid.Focus();
            }

            if(k.Key == Key.R)
            {
                resolvedRequestsGrid.Focus();
            }
        }

        public void PendingKey(object s, System.Windows.Input.KeyEventArgs k)
        {
            if(k.Key == Key.C)
            {
                DelaymentRequestComment delaymentRequestComment = new DelaymentRequestComment();
                UserService userService = new UserService();

                int selectedRowIndex = pendingRequestsGrid.SelectedIndex;

                OpenPendingRequestComment(delaymentRequestComment, userService, selectedRowIndex);
            }

            if(k.Key == Key.Back)
            {
                int selectedRowIndex = pendingRequestsGrid.SelectedIndex;
                UserService userService = new UserService();
                bookingDelaymentRequestService.Delete(userService.GetPendingBookingDelaymentRequests()[selectedRowIndex]);
                DataBaseContext context = new DataBaseContext();
                List<BookingDelaymentRequest> resolvedDelaymentRequests = userService.GetResolvedBookingDelaymentRequests();
                List<BookingDelaymentRequest> pendingDelaymentRequests = userService.GetPendingBookingDelaymentRequests();

                if (resolvedDelaymentRequests != null)
                {
                    FillResolvedRequestsGrid(bookingService, accommodationService, resolvedDelaymentRequests);

                }
                else
                {
                    resolvedRequestsGrid.ItemsSource = null;
                }

                if (pendingDelaymentRequests != null)
                {
                    FillPendingRequestsGrid(bookingService, accommodationService, pendingDelaymentRequests);
                }
                else
                {
                    pendingRequestsGrid.ItemsSource = null;
                }
            } 
        }

        public void ResolvedKey(object s, System.Windows.Input.KeyEventArgs k)
        {
            if (k.Key == Key.C)
            {
                DelaymentRequestComment delaymentRequestComment = new DelaymentRequestComment();
                UserService userService = new UserService();

                int selectedRowIndex = resolvedRequestsGrid.SelectedIndex;

                OpenResolvedtRequestComment(delaymentRequestComment, userService, selectedRowIndex);
            }

            if (k.Key == Key.Back)
            {
                int selectedRowIndex = resolvedRequestsGrid.SelectedIndex;
                UserService userService = new UserService();
                bookingDelaymentRequestService.Delete(userService.GetResolvedBookingDelaymentRequests()[selectedRowIndex]);
                DataBaseContext context = new DataBaseContext();
                List<BookingDelaymentRequest> resolvedDelaymentRequests = userService.GetResolvedBookingDelaymentRequests();
                List<BookingDelaymentRequest> pendingDelaymentRequests = userService.GetPendingBookingDelaymentRequests();

                if (resolvedDelaymentRequests != null)
                {
                    FillResolvedRequestsGrid(bookingService, accommodationService, resolvedDelaymentRequests);

                }
                else
                {
                    resolvedRequestsGrid.ItemsSource = null;
                }

                if (pendingDelaymentRequests != null)
                {
                    FillPendingRequestsGrid(bookingService, accommodationService, pendingDelaymentRequests);
                }
                else
                {
                    pendingRequestsGrid.ItemsSource = null;
                }
            }
        }
    }
}
