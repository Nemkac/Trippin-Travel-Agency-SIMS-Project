﻿using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class NavigatorViewModel : ViewModelBase
    {
        public ViewModelCommand GoToBookings { get; set; }
        public ViewModelCommand GoToHome { get; set; }
        public ViewModelCommand GoReviews { get; set; }
        public ViewModelCommand GoAccount { get; set; }
        public ViewModelCommand GoDelaymentRequests { get; set; }
        public NavigatorViewModel()
        {
            GoToBookings = new ViewModelCommand(GoToFutureBookings);
            GoToHome = new ViewModelCommand(GoHome);
            GoReviews = new ViewModelCommand(GoToReviews);
            GoAccount = new ViewModelCommand(GoToAccount);
            GoDelaymentRequests = new ViewModelCommand(GoToDelaymentRequests);

        }

        public void GoToFutureBookings(object sender)
        {
            CloseInterfaces();
            FutureBookingsInterface futureBookingsInterface = new FutureBookingsInterface();
            GuestOneStaticHelper.futureBookingsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            futureBookingsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            futureBookingsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            GuestOneStaticHelper.navigator.Close();
            futureBookingsInterface.Show();
        }

        public void GoToDelaymentRequests(object sender)
        {
            CloseInterfaces();
            GuestsBookingDelaymentRequestsInterface guestsBookingDelaymentRequestsInterface = new GuestsBookingDelaymentRequestsInterface();
            GuestOneStaticHelper.guestsBookingDelaymentRequestsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            guestsBookingDelaymentRequestsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestsBookingDelaymentRequestsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            GuestOneStaticHelper.navigator.Close();
            guestsBookingDelaymentRequestsInterface.Show();
        }

        public void GoHome(object sender)
        {
            CloseInterfaces();
            GuestOneInterface guestOneInterface = new GuestOneInterface();
            GuestOneStaticHelper.guestOneInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            guestOneInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestOneInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            GuestOneStaticHelper.navigator.Close();
            guestOneInterface.Show();
        }

        public void GoToReviews(object sender)
        {
            CloseInterfaces();
            GuestsReviewsInterface guestsReviewsInterface = new GuestsReviewsInterface();
            GuestOneStaticHelper.guestsReviewsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            guestsReviewsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            guestsReviewsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestsReviewsInterface.Show();
        }

        public void GoToAccount(object sender)
        {
            CloseInterfaces();
            GuestsAccountInterface guestsAccountInterface = new GuestsAccountInterface();
            GuestOneStaticHelper.guestsAccountInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
            guestsAccountInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            guestsAccountInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestsAccountInterface.Show();
        }

        public void CloseInterfaces()
        {
            GuestOneStaticHelper.navigator.Close();
            GuestOneStaticHelper.guestOneInterface?.Close();
            GuestOneStaticHelper.bookAccommodationInterface?.Close();
            GuestOneStaticHelper.pastBookingsInterface?.Close();
            GuestOneStaticHelper.rateAccommodationInterface?.Close();
            GuestOneStaticHelper.sendBookingDelaymentInterface?.Close();
            GuestOneStaticHelper.futureBookingsInterface?.Close();
            GuestOneStaticHelper.guestsReviewsInterface?.Close();
            GuestOneStaticHelper.renovationSuggestionInterface?.Close();
            GuestOneStaticHelper.guestsAccountInterface?.Close();
        }
    }
}
