namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testmaxlenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Awards", "Name", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Awards", "Name", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
