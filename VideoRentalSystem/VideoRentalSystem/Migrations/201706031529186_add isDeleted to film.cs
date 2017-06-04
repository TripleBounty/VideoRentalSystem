namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisDeletedtofilm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Films", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Awards", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Films", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Films", "Summary", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Films", "Duration", c => c.Time(nullable: false, precision: 0));
            AlterColumn("dbo.Awards", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Films", "RealieseDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "RealieseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Awards", "Name", c => c.String());
            AlterColumn("dbo.Films", "Duration", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Films", "Summary", c => c.String());
            AlterColumn("dbo.Films", "Name", c => c.String());
            DropColumn("dbo.Awards", "IsDeleted");
            DropColumn("dbo.Films", "IsDeleted");
            DropColumn("dbo.Films", "ReleaseDate");
        }
    }
}
