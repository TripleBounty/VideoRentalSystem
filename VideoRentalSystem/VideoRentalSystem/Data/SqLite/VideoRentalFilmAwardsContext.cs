using System;
using System.Data.Entity;
using VideoRentalSystem.Migrations.FilmAwardSqLite;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.SqLite
{
    public partial class VideoRentalFilmAwardsContext : DbContext
    {
        static VideoRentalFilmAwardsContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<VideoRentalFilmAwardsContext, sqLiteConfiguration>(true));
        }

        public VideoRentalFilmAwardsContext()
            : base("SqliteDb")
        {
        }

        public virtual IDbSet<Award> Award { get; set; }

        public virtual IDbSet<Organisation> Organisation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}