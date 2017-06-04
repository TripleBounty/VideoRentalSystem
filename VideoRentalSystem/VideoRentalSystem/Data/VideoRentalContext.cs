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

        public IDbSet<Town> TownsTable { get; set; }

        public IDbSet<Address> AddressesTable { get; set; }

        public IDbSet<Employee> EmployeesTable { get; set; }

        public IDbSet<Customer> CustomersTable { get; set; }

        public IDbSet<Review> ReviewsTable { get; set; }

        public IDbSet<Film> FilmTable { get; set; }

        public IDbSet<Award> AwardTable { get; set; }

        public IDbSet<Store> StoreTable { get; set; }

        public IDbSet<FilmStaff> FilmStaffTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.CreateAddresseModels(modelBuilder);
            this.CreateReviewModel(modelBuilder);
            this.CreateEmployeeModel(modelBuilder);
            this.CreateCustomerModel(modelBuilder);
            this.CreateFilmModels(modelBuilder);
            this.CreateAwardModels(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateEmployeeModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName).HasMaxLength(25)
                                             .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName).HasMaxLength(25)
                                            .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary).IsRequired();
        }

        private void CreateCustomerModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.FirstName).HasMaxLength(25)
                                             .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(e => e.LastName).HasMaxLength(25)
                                            .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(e => e.BirthDate).IsRequired();
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

            modelBuilder.Entity<Town>()
                .Property(t => t.Name)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.PostalCode)
                .HasMaxLength(10);
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