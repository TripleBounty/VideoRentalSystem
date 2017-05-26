using System.Data.Entity;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data
{
    public class VideoRentalContext : DbContext
    {
        public DbSet<Country> CountriesTable { get; set; }

        public DbSet<User> UsersTable { get; set; }

        public DbSet<Employee> EmployeesTable { get; set; }
    }
}