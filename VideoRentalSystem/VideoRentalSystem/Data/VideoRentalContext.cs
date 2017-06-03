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

        public DbSet<Country> Countries { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.CreateAddresseModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void CreateAddresseModels(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
               .HasMaxLength(40)
               .IsRequired();

            modelBuilder.Entity<Country>()
                .Property(c => c.Code)
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}