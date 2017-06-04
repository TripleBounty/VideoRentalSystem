namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class award : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Films", "InStore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "InStore", c => c.Int(nullable: false));
        }
    }
}
