﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<AccommodationLocation> LocationsOfAccommodations { get; set; }

        public string path = @".\MyDatabase.sqlite";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

    }
}