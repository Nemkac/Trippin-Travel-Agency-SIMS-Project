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
        public DbSet<Tour> Tour { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Accommodation> Accommodations { get; set; }    

        public DbSet<AccommodationLocation> LocationsOfAccommodations { get; set; } 


        // svako svoju od nas 4.ice mora da ima. logicno

        public string path = @"C:\Users\Nemanja\Desktop\Projekat\Trippin-Travel-Agency-SIMS-Project-Db_Setup_2\Trippin Travel Agency\InitialProject\InitialProject\MyDatabase.sqlite";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

    }
}
