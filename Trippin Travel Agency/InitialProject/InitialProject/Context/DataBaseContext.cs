using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace InitialProject.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }    
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TourLocation> TourLocation { get; set; }  
        public DbSet<AccommodationLocation> LocationsOfAccommodations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<KeyPoint> KeyPoints { get; set; }
        public DbSet<GuestRate> GuestRate { get; set; }

        // Apsolute paths for every memeber of the team
        // napisite cije je sta majmuni
        //public string path = @"C:\Users\ifeel\Documents\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";
        // public string path = @"C:\Users\Nemanja\Desktop\Faks\Semestar 6\Projekat c#\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";
        // public string path = @"C:\Users\Nemanja\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

<<<<<<< Updated upstream
        //Nemanja Ranitovic putanja desktop
        //public string path= @"C:\Users\Nemanja\Desktop\Faks\Semestar 6\Projekat c#\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // blanusa lap
        public string path = @"C:\Users\abc\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        //blanusa pc
        //ublic string path = @"C:\Users\vlada\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite"; 
=======
        // Nemanja Todorovic
         //public string path = @"C:\Users\Nemanja\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir Blanusa lap
        // public string path = @"C:\Users\abc\Documents\GitHub\SIMS-HCI-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

        // Vladimir BLanusa pc
        public string path = @"C:\Users\vlada\GitHub\Trippin-Travel-Agency-SIMS-Project\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";

>>>>>>> Stashed changes

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }
    }
}
