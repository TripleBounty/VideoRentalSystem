namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removevideoformatfromfilm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Films", "VideoFormats");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "VideoFormats", c => c.Int(nullable: false));
        }
    }
}
