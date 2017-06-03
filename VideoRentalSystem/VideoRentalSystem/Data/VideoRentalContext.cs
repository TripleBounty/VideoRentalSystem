using System.Data.Entity;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data
{
    public class VideoRentalContext : DbContext
    {
        public VideoRentalContext()
            : base("VideoRentalDBConnection")
        {
        }

        public DbSet<Country> CountriesTable { get; set; }

        public DbSet<Employee> EmployeesTable { get; set; }

        public DbSet<Film> FilmTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Review> ReviewsTable { get; set; }
    }
}