using System;
using System.Data.Entity;
using VideoRentalSystem.Migrations.FilmAwardSqLite;

namespace VideoRentalSystem.Data.SqLite
{
    public partial class VideoRentalFilmAwardsContext : DbContext
    {
        public virtual IDbSet<Award> Award { get; set; }
        public virtual IDbSet<Organisation> Organisation { get; set; }

        static VideoRentalFilmAwardsContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<VideoRentalFilmAwardsContext, sqLiteConfiguration>(true));
        }

        public VideoRentalFilmAwardsContext()
            : base("SqliteDb")
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

        //    optionsBuilder.UseSqlite(@"data source=""D:\My Documents\GitHub\VideoRentalSystem\VideoRentalSystem\VideoRentalSystem\Awards.db""");
            
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}