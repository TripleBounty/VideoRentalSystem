namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedFiledaddedtogenreagerating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilmRatings", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.FilmGenres", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilmGenres", "IsDeleted");
            DropColumn("dbo.FilmRatings", "IsDeleted");
        }
    }
}
