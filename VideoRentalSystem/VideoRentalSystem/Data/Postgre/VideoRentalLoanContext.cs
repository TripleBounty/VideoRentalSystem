using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Postgre
{
    public class VideoRentalLoanContext : DbContext
    {
        public VideoRentalLoanContext()
            : base("name = VideoRentalLoanDBConnectionPostgresql")
        {
        }

        public IDbSet<Loan> LoansTable { get; set; }

        public IDbSet<Tarif> TarifsTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Loan>()
                .Property(l => l.StoreId)
                .IsRequired();

            modelBuilder.Entity<Loan>()
                .Property(l => l.FilmId)
                .IsRequired();

            modelBuilder.Entity<Loan>()
                .Property(l => l.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Tarif>()
                .Property(t => t.Name)
                .HasMaxLength(10);

            base.OnModelCreating(modelBuilder);
        }
    }
}