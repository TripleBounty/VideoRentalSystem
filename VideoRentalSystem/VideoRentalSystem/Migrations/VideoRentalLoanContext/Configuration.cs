namespace VideoRentalSystem.Migrations.VideoRentalLoanContext
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<VideoRentalSystem.Data.Postgre.VideoRentalLoanContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.MigrationsDirectory = @"Migrations\VideoRentalLoanContext";
        }

        protected override void Seed(VideoRentalSystem.Data.Postgre.VideoRentalLoanContext context)
        {
        }
    }
}
