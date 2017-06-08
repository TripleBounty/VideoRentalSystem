namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<VideoRentalSystem.Data.VideoRentalContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.MigrationsDirectory = @"Migrations\VideoRentalContext";
        }

        protected override void Seed(VideoRentalSystem.Data.VideoRentalContext context)
        {
        }
    }
}
