using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    static class GuestOneStaticHelper
    {
        public static int id { get; set; }
        public static dynamic result { get; set; }
        public static Booking selectedBookingToDelay { get; set; }
        public static FutureBookingsInterface futureBookingsInterface { get; set; }
        public static int selectedBookingIdToRate { get; set; }
        public static PastBookingsInterface pastBookingsInterface { get; set; }
        public static GuestOneInterface guestOneInterface { get; set; }
        public static BookAccommodationInterface bookAccommodationInterface { get; set; }
        public static RateAccommodationInterface rateAccommodationInterface { get; set; }
        public static SendBookingDelaymentInterface sendBookingDelaymentInterface { get; set;}
        public static BookingConfirmationInterface bookingConfirmationInterface { get; set; }
        public static BookingDelaymentConfirmationInterface bookingDelaymentConfirmationInterface { get;set; }
        public static CancelationConfirmationMessageInterface cancelationConfirmationMessageInterface { get; set;}
        public static RateAccommodationConfirmationInterface rateAccommodationConfirmationInterface { get; set; }
        public static GuestsReviewsInterface guestsReviewsInterface { get; set; }
        public static SelectedGuestReviewInterface selectedGuestReviewInterface { get; set; }
        public static RenovationSuggestionInterface renovationSuggestionInterface { get; set; }
        public static GuestsAccountInterface guestsAccountInterface { get; set; }
        public static Navigator navigator { get; set; }
        public static GuestRate guestRate { get; set; }

    }
}
