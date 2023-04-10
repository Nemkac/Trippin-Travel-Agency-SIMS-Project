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



        // Apsolute paths for every memeber of the team

        // Aleksa Simeunovic -> Desktop PC
        // public string path = @"C:\Users\ifeel\Documents\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Aleksa Simeunovic -> Laptop
        // public string path = @"C:\\Users\\aleks\\OneDrive\\Dokumenti\\GitHub\\Trippin-Travel-Agency-SIMS-Project\\Trippin Travel Agency\\InitialProject\\InitialProject\\MyDatabase.sqlite";

        // Nemanja Ranitovic
         public string path = @"C:\Users\Nemanja\Desktop\Faks\Semestar 6\Projekat c#\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Nemanja Todorovic
        // public string path = @"C:\Users\Nemanja\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir Blanusa lap
        // public string path = @"C:\Users\abc\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir BLanusa pc
        // public string path = @"C:\Users\vlada\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }
    }
}
