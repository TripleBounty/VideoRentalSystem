﻿using System.Data.Entity;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data
{
    public class VideoRentalContext : DbContext
    {
        public VideoRentalContext()
            :base("VideoRentalDBConnection")
        {

        }

        public DbSet<Country> CountriesTable { get; set; }

        public DbSet<Employee> EmployeesTable { get; set; }
    }
}