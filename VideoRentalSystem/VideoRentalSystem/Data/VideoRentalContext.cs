using System.Data.Entity;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data
{
    public class VideoRentalContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
    }
}