using Microsoft.EntityFrameworkCore;
using InitialProject.Model;
using InitialProject.DTO;
using System.Security.Policy;
using InitialProject.Model.TransferModels;

namespace InitialProject.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }    
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TourLocation> TourLocation { get; set; }  
        public DbSet<AccommodationLocation> AccommodationLocation { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<KeyPoint> KeyPoints { get; set; }
        public DbSet<GuestRate> GuestRate { get; set; }
        public DbSet<TourReservation> TourReservations { get; set; }
        public DbSet<BookingDelaymentRequest> BookingDelaymentRequests { get; set; }
        public DbSet<RequestDTO> SelectedRequestTransfers { get; set; }
        public DbSet<AccommodationRate> AccommodationRates { get; set; }    
        public DbSet<TourLiveViewTransfer> TourLiveViewTransfers { get; set; }
        public DbSet<DetailedTourViewTransfer> detailedTourViewTransfers { get; set; }
        public DbSet<Coupon> Coupons { get; set; }  
        public DbSet<TourMessage> TourMessages { get; set; }
        public DbSet<TourAttendance> TourAttendances { get; set; }
        public DbSet<TourBookingTransfer> tourBookingTransfers { get; set; }
        public DbSet<TourAndGuideRate> TourAndGuideRates { get; set; } 
        public DbSet<BookingTransfer> SelectedRatingNotificationTransfer { get; set; }
        public DbSet<CanceledBooking> CanceledBookings { get; set; }
        public DbSet<BookingCancelationMessage> BookingCancelationMessages { get; set; }
        public DbSet<TourStatisticsDTO> TourStatisticsTransfer { get; set; }
        public DbSet<DelayedBookings> DelayedBookings { get; set; }
        public DbSet<AnnualAccommodationTransfer> AccommodationAnnualStatisticsTransfer { get; set; }
        public DbSet<MonthlyAccommodationTransfer> AccommodationsMonthlyStatisticsTransfer { get; set; } 
        public DbSet<TourRequest> TourRequests { get; set; }
        public DbSet<AcceptedTourRequestViewTransfer> AcceptedTourRequestViewTransfers { get; set; }
        public DbSet<RequestMessage> RequestMessages { get; set; }
        public DbSet<TourLocationTransfer> TourLocationTransfers { get; set; }
        public DbSet<TourLanguageTransfer> TourLanguageTransfers { get; set; }
        public DbSet<TourFlagTransfer> TourFlagTransfers { get; set; }
        public DbSet<TourTodayImagesTransfer> TourTodayImagesTransfers { get; set; }
        public DbSet<AccommodationRenovation> AccommodationRenovations { get; set; }




        // Apsolute paths for every memeber of the team

        // Aleksa Simeunovic -> Desktop PC
        // public string path = @"C:\Users\ifeel\Documents\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Aleksa Simeunovic -> Laptop
        public string path = @"C:\\Users\\aleks\\OneDrive\\Dokumenti\\GitHub\\Trippin-Travel-Agency-SIMS-Project\\Trippin Travel Agency\\InitialProject\\InitialProject\\MyDatabase.sqlite"; 

        // Nemanja Ranitovic
         public string path = @"C:\Users\Nemanja\Desktop\Faks\Semestar 6\Projekat c#\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Nemanja Todorovic
        // public string path = @"C:\Users\Nemanja\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir Blanusa lap
        // public string path = @"C:\Users\abc\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir BLanusa pc
        // public string path = @"C:\Users\vlada\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir Blanusinog brata komp 
        // public string path = @"C:\Users\Dusan\Documents\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookingDelaymentRequest>()
                .HasOne<Booking>()
                .WithMany()
                .HasForeignKey(bookingDelaymentRequest => bookingDelaymentRequest.bookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne<Accommodation>()
                .WithMany()
                .HasForeignKey(booking => booking.accommodationId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
