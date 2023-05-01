﻿using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoMesagesViewModel : ViewModelBase
    {
        public static int tourIdTransfer = -1;

        private readonly GuestTwoInterfaceViewModel _mainViewModel;

        private readonly TourLocationService tourLocationService = new(new TourLocationRepository());
        public ViewModelCommand DetailedTourViewCommand { get; private set; }
        public ViewModelCommand OpenMessage { get; private set; }
        public ObservableCollection<string>? requestMessages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<TourMessage> responseMessages { get; set; } = new ObservableCollection<TourMessage>();

        private string usernameLabel;
        public string UsernameLabel
        {
            get { return usernameLabel; }
            set
            {
                if (usernameLabel != value)
                {
                    usernameLabel = value;
                    OnPropertyChanged(nameof(UsernameLabel));
                }
            }
        }

        private string usernameLabel2;
        public string UsernameLabel2
        {
            get { return usernameLabel2; }
            set
            {
                if (usernameLabel2 != value)
                {
                    usernameLabel2 = value;
                    OnPropertyChanged(nameof(UsernameLabel2));
                }
            }
        }

        private string accountType;
        public string AccountType
        {
            get { return accountType; }
            set
            {
                if (accountType != value)
                {
                    accountType = value;
                    OnPropertyChanged(nameof(AccountType));
                }
            }
        }
        
        private string selectedRequest;
        public string SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
                if (selectedRequest != value)
                {
                    selectedRequest = value;
                    OnPropertyChanged(nameof(SelectedRequest));
                }
            }
        }

        private TourMessage selectedResponseMessage;
        public TourMessage SelectedResponseMessage
        {
            get { return selectedResponseMessage; }
            set
            {
                if (selectedResponseMessage != value)
                {
                    selectedResponseMessage = value;
                    OnPropertyChanged(nameof(SelectedResponseMessage));
                }
            }
        }


        public GuestTwoMesagesViewModel()
        {
            
            this.tourLocationService = new(new TourLocationRepository());
            _mainViewModel = LoggedUser.GuestTwoInterfaceViewModel;
            DetailedTourViewCommand = new ViewModelCommand(ShowDetailedTourView);
            OpenMessage = new ViewModelCommand(OpenSelectedMessage);
            WindowLoaded();
            tourIdTransfer = -1;
        }

        public void WindowLoaded()
        {
            UsernameLabel = "Hello, " + LoggedUser.username + "!";
            UsernameLabel2 = "@" + LoggedUser.username;
            AccountType = "Account type:  " + LoggedUser.role;
            DataBaseContext context = new DataBaseContext();
            LoadMessages(context);
        }
        public void LoadMessages(DataBaseContext context)
        {

            
            foreach (TourMessage message in context.TourMessages.ToList())
            {
                if (LoggedUser.id == message.guestId)
                {
                    responseMessages.Add(message);
                }
            }
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
                    requestMessages.Add(poruka);
                }
            }
        }

        private void LoadNewTourAnnouncements(DataBaseContext context)
        {

            foreach (UnfulfilledTourCities unfulfilledRequest in context.UnfulfilledTourCities.ToList())
            {
                if (unfulfilledRequest.guestId == LoggedUser.id)
                {
                    foreach (Tour tour in context.Tours.ToList())
                    {
                        TourLocation location = this.tourLocationService.GetById(tour.location);
                        if (unfulfilledRequest.city == location.city)
                        {
                            string poruka = "[NewTour] New tour with unfulfiled request: tourId: " + tour.id.ToString();
                            if (!requestMessages.Contains(poruka))
                            {
                                requestMessages.Add(poruka);
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
                            if (!requestMessages.Contains(poruka))
                            {
                                requestMessages.Add(poruka);
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
                            if (!requestMessages.Contains(poruka))
                            {
                                requestMessages.Add(poruka);
                            }
                            // context.UnfulfilledTourLanguages.Remove(unfulfilledRequest);
                            // context.SaveChanges();
                        }
                    }
                }
            }            
            context.SaveChanges();
        }

        public void OpenSelectedMessage(object obj) 
        {
            TourMessage tourMessage = SelectedResponseMessage;
            DataBaseContext context = new DataBaseContext();
            if (tourMessage != null)
            {
                TourAttendance tourAttendance = new TourAttendance(tourMessage.tourId, tourMessage.keyPointId, tourMessage.guestId, tourMessage.numberOfGuests);
                context.TourAttendances.Add(tourAttendance);
                //context.TourMessages.Remove(tourMessage);
                context.SaveChanges();
            }
        }
        public void ShowDetailedTourView(object obj)
        {
            string message = SelectedRequest;
            if (message.Substring(0, 9) == "[NewTour]")
            {
                _mainViewModel.ExecuteShowDetailedTourView(null);
                tourIdTransfer = Int32.Parse(message.Substring(message.Length - 2));
            }            
        }
    }
}
