using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.TourServices;
using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitialProject.Service.GuestServices
{
    class UserService
    {
        private AccommodationService accommodationService;
        private BookingService bookingService;
        public UserService()
        {
            BookingRepository bookingRepository = new BookingRepository();
            bookingService = new BookingService(bookingRepository);
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            accommodationService = new AccommodationService(accommodationRepository);
        }

        public User GetById(int id)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Users.SingleOrDefault(u => u.id == id);
        }

        public List<Booking> GetGuestsBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> allBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id)
                {
                    allBookings.Add(booking);
                }
            }
            return allBookings;
        }

        public List<BookingDelaymentRequest> GetBookingDelaymentRequests(int id)
        {
            DataBaseContext context = new DataBaseContext();
            //BookingService bookingService = new BookingService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = context.BookingDelaymentRequests.ToList();
            List<BookingDelaymentRequest> foundBookingDelaymentRequests = new List<BookingDelaymentRequest>();
            foreach (BookingDelaymentRequest bookingDelaymentRequest in bookingDelaymentRequests)
            {
                if (bookingService.GetById(bookingDelaymentRequest.bookingId).guestId == id)
                {
                    foundBookingDelaymentRequests.Add(bookingDelaymentRequest);
                }
            }
            return foundBookingDelaymentRequests;
        }

        public List<BookingDelaymentRequest> GetResolvedBookingDelaymentRequests()
        {
            DataBaseContext context = new DataBaseContext();
            //BookingService bookingService = new BookingService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = context.BookingDelaymentRequests.ToList();
            List<BookingDelaymentRequest> foundRequests = new List<BookingDelaymentRequest>();
            foreach (BookingDelaymentRequest bookingDelaymentRequest in bookingDelaymentRequests)
            {
                if (bookingService.GetById(bookingDelaymentRequest.bookingId).guestId == LoggedUser.id && (bookingDelaymentRequest.status == Status.Accepted || bookingDelaymentRequest.status == Status.Denied))
                {
                    foundRequests.Add(bookingDelaymentRequest);
                }
            }
            if (foundRequests.Count > 0)
            {
                return foundRequests;
            }
            return null;
        }

        public List<BookingDelaymentRequest> GetPendingBookingDelaymentRequests()
        {
            DataBaseContext context = new DataBaseContext();
            //BookingService bookingService = new BookingService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = context.BookingDelaymentRequests.ToList();
            List<BookingDelaymentRequest> foundRequests = new List<BookingDelaymentRequest>();
            foreach (BookingDelaymentRequest bookingDelaymentRequest in bookingDelaymentRequests)
            {
                if (bookingService.GetById(bookingDelaymentRequest.bookingId).guestId == LoggedUser.id && bookingDelaymentRequest.status == Status.Pending)
                {
                    foundRequests.Add(bookingDelaymentRequest);
                }
            }
            if (foundRequests.Count > 0)
            {
                return foundRequests;
            }
            return null;
        }


        public List<Booking> GetGuestsPastBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> pastBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id && DateTime.Parse(booking.departure) < DateTime.Today)
                {
                    pastBookings.Add(booking);
                }
            }
            return pastBookings;
        }

        public List<Booking> GetGuestsFutureBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> futureBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id && DateTime.Parse(booking.arrival) > DateTime.Today)
                {
                    futureBookings.Add(booking);
                }
            }
            return futureBookings;
        }

        public List<Booking> GetGuestsDelayableBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> delayableBookings = new List<Booking>();
            //AccommodationService accommodationService = new AccommodationService();
            bool ifDelayable = false;
            foreach (Booking booking in bookings.ToList())
            {
                ifDelayable = accommodationService.GetById(booking.accommodationId).bookingCancelPeriodDays <= DateTime.Parse(booking.arrival).Subtract(DateTime.Today).Days;
                if (booking.guestId == id && ifDelayable)
                {
                    delayableBookings.Add(booking);
                }
            }
            return delayableBookings;
        }

        public int BookingsInLastYear()
        {
            DataBaseContext context = new DataBaseContext();
            List<SuperGuest> superGuests = context.SuperGuests.ToList();
            DateTime oldSuperGuestAcquisition = new DateTime();
            oldSuperGuestAcquisition = DateTime.Today.AddDays(1);
            foreach (SuperGuest superGuest in superGuests)
            {
                if(superGuest.guestId == LoggedUser.id && superGuest.ifActive == 0)
                {
                    oldSuperGuestAcquisition = superGuest.titleAcquisition;
                }
            }
            int bookingsInLastYearCounter = 0;
            DateTime dayYearBefore = DateTime.Today.AddYears(-1);
            List<Booking> pastBookings = GetGuestsPastBookings(LoggedUser.id);
            if (oldSuperGuestAcquisition != DateTime.Today.AddDays(1))
            {
                foreach (Booking booking in pastBookings)
                {
                    if (DateTime.Parse(booking.departure) > dayYearBefore && oldSuperGuestAcquisition < DateTime.Parse(booking.departure))
                    {
                        bookingsInLastYearCounter++;
                    }
                }
            }
            else
            {
                foreach (Booking booking in pastBookings)
                {
                    if (DateTime.Parse(booking.departure) > dayYearBefore)
                    {
                        bookingsInLastYearCounter++;
                    }
                }
            }
            return bookingsInLastYearCounter;
        }

        public SuperGuest IsSuperGuest()
        {
            DataBaseContext context = new DataBaseContext();
            List<SuperGuest> superGuests = context.SuperGuests.ToList();
            foreach(SuperGuest superGuest in superGuests)
            {
               if(superGuest.guestId == LoggedUser.id && superGuest.ifActive == 1)
                {
                    return superGuest;
                }
            }
            return null;
        }

        public int BookingsSinceSuperGuestAcquisition()
        {
            int bookingsSinceTitleAcquisition = 0;
            BookingService bookingService = new BookingService(new BookingRepository());
            DataBaseContext context = new DataBaseContext();
            List<SuperGuest> superGuests = context.SuperGuests.ToList();
            foreach (SuperGuest superGuest in superGuests)
            {
                if (superGuest.guestId == LoggedUser.id && superGuest.ifActive == 1)
                {
                    foreach(Booking booking in GetGuestsPastBookings(LoggedUser.id))
                    {
                        if(DateTime.Parse(booking.departure) > superGuest.titleAcquisition)
                        {
                            bookingsSinceTitleAcquisition++;
                        }
                    }
                }
            }
            return bookingsSinceTitleAcquisition;
        }

        public bool HasGuestVisitedPlace(int guestId, AccommodationLocation location)
        {
            List<Booking> bookings = GetGuestsBookings(guestId);
            foreach (Booking booking in bookings)
            {
                if (accommodationService.GetAccommodationLocation(accommodationService.GetById(booking.accommodationId).id)[0] == location.country && accommodationService.GetAccommodationLocation(accommodationService.GetById(booking.accommodationId).id)[1] == location.city)
                {
                    return true;
                }
            }

            DataBaseContext context = new DataBaseContext();
            List<TourAttendance> tourAttendances = context.TourAttendances.ToList();
            TourService tourService = new TourService(new TourRepository());
            List<Tour> tours = tourService.GetAllByLocation(location);
            List<TourAttendance> foundTourAttendaces = new List<TourAttendance>();
            if (tours != null)
            {
                foreach (Tour tour in tours)
                {
                    foreach (TourAttendance tourAttendance in tourAttendances)
                    {
                        if (tour.id == tourAttendance.tourId)
                        {
                            foundTourAttendaces.Add(tourAttendance);
                        }
                    }
                }
                foreach (TourAttendance tourAttendance1 in foundTourAttendaces)
                {
                    if (tourAttendance1.guestID == guestId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int CountLocationVisits(int guestId, AccommodationLocation location)
        {
            int counter = 0;
            List<Booking> bookings = GetGuestsBookings(guestId);
            foreach (Booking booking in bookings)
            {
                if (accommodationService.GetAccommodationLocation(accommodationService.GetById(booking.accommodationId).id)[0] == location.country && accommodationService.GetAccommodationLocation(accommodationService.GetById(booking.accommodationId).id)[1] == location.city)
                {
                    counter++;
                }
            }
            DataBaseContext context = new DataBaseContext();
            List<TourAttendance> tourAttendances = context.TourAttendances.ToList();
            TourService tourService = new TourService(new TourRepository());
            List<Tour> tours = tourService.GetAllByLocation(location);
            List<TourAttendance> foundTourAttendaces = new List<TourAttendance>();
            if (tours != null)
            {
                foreach (Tour tour in tours)
                {
                    foreach (TourAttendance tourAttendance in tourAttendances)
                    {
                        if (tour.id == tourAttendance.tourId)
                        {
                            foundTourAttendaces.Add(tourAttendance);
                        }
                    }
                }
                foreach (TourAttendance tourAttendance1 in foundTourAttendaces)
                {
                    if (tourAttendance1.guestID == guestId)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public bool IsOverVisited(Forum forum)
        {
            DataBaseContext context = new DataBaseContext();
            ForumService forumService = new ForumService();
            UserService userService = new UserService();
            List<ForumComment> comments = forumService.GetForumsComments(forum);
            List<User> users = new List<User>();
            foreach (ForumComment comment in comments)
            {
                users.Add(userService.GetById(comment.userId));
            }
            int counter = 0;
            foreach (User user in users)
            {
                counter += CountLocationVisits(user.id, new AccommodationLocation(forumService.GetLocation(forum.id)[0],forumService.GetLocation(forum.id)[1]));
            }
            if (counter >= 20)
            {
                return true;
            }
            if (CountOwnersComments(forum) >= 10)
            {
                return true;
            }

            return false;
        }

        public int CountOwnersComments(Forum forum)
        {
            int counter = 0;
            DataBaseContext context = new DataBaseContext();
            ForumService forumService = new ForumService();
            UserService userService = new UserService();
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            List<ForumComment> comments = forumService.GetForumsComments(forum);
            List<User> users = new List<User>();
            foreach (ForumComment comment in comments)
            {
                users.Add(userService.GetById(comment.userId));
            }

            List<User> owners = new List<User>();
            foreach(User user in users)
            {
                if(user.role == "Owner")
                {
                    owners.Add(user);
                }
            }

            List<Accommodation> accommodations = accommodationService.GetAllByLocation(new AccommodationLocation((forumService.GetLocation(forum.id))[0], (forumService.GetLocation(forum.id))[1]));
            foreach(Accommodation accommodation in accommodations)
            {
                foreach(User user in owners)
                {
                    if(accommodation.ownerId == user.id)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public bool IsForumSuperUseful(Forum forum)
        {
            DataBaseContext context = new DataBaseContext();
            ForumService forumService = new ForumService();
            UserService userService = new UserService();
            List<ForumComment> comments = forumService.GetForumsComments(forum);
            List<User> users = new List<User>();
            int ownerCommentCounter = 0;
            int guestCommentCounter = 0;
            foreach(ForumComment comment in comments)
            {
                if (IfCommentByOwner(comment.userId, new AccommodationLocation((forumService.GetLocation(forum.id))[0], (forumService.GetLocation(forum.id))[1])))
                {
                    ownerCommentCounter++;
                }
                if (IfCommentByGuest(comment.userId, new AccommodationLocation((forumService.GetLocation(forum.id))[0], (forumService.GetLocation(forum.id))[1])))
                {
                    guestCommentCounter++;
                }
            }
            if(ownerCommentCounter >= 10 && guestCommentCounter >= 20)
            {
                return true;
            }
            return false;
        }

        public bool IfCommentByOwner(int ownerId,AccommodationLocation location)
        {
            DataBaseContext context = new DataBaseContext();
            ForumService forumService = new ForumService();
            UserService userService = new UserService();
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            if (userService.GetById(ownerId).role == "Owner")
            {
                List<Accommodation> accommodations = accommodationService.GetAllByLocation(location);
                foreach (Accommodation accommodation in accommodations)
                {

                    if (accommodation.ownerId == ownerId)
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public bool IfCommentByGuest(int guestId, AccommodationLocation location)
        {
            DataBaseContext context = new DataBaseContext();
            ForumService forumService = new ForumService();
            UserService userService = new UserService();
            BookingService bookingService = new BookingService(new BookingRepository());
            List<Booking> bookings = userService.GetGuestsBookings(guestId);
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());

            List<int> visitedAccommodations = new List<int>();
            foreach(Booking booking in bookings)
            {
                visitedAccommodations.Add(accommodationService.GetById(booking.accommodationId).id);
            }
            List<Accommodation> accommodationsAtLocation = accommodationService.GetAllByLocation(location);
            List<Accommodation> intersection = accommodationService.GetMatching(visitedAccommodations, accommodationsAtLocation);

            if(intersection != null)
            {
                return true;
            }
            
            return false;
        }




    }
}
