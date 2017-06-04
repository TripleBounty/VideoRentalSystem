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

        public IDbSet<Country> CountriesTable { get; set; }

        public IDbSet<Employee> EmployeesTable { get; set; }

        public IDbSet<Review> ReviewsTable { get; set; }

        public IDbSet<Film> FilmTable { get; set; }

        public IDbSet<Award> AwardTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.CreateAddresseModels(modelBuilder);
            this.CreateReviewModel(modelBuilder);
            this.CreateFilmModels(modelBuilder);
            this.CreateAwardModels(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateReviewModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .Property(r => r.Description)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .IsRequired();
        }

        private void CreateAddresseModels(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
               .HasMaxLength(40)
               .IsRequired();

            // TODO: fix country code max length
            modelBuilder.Entity<Country>()
                .Property(c => c.Code)
                .HasMaxLength(2)
                .IsRequired();
        }

        private void CreateFilmModels(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Film>()
                .Property(x => x.Summary)
                .HasMaxLength(512)
                .IsRequired();

            modelBuilder.Entity<Film>()
                .Property(x => x.ReleaseDate)
                .IsRequired();

            modelBuilder.Entity<Film>()
                .Property(x => x.Duration)
                .HasPrecision(0)
                .IsRequired();
        }

        private void CreateAwardModels(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Award>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Award>()
                .Property(x => x.Date)
                .IsRequired();
        }
    }
}