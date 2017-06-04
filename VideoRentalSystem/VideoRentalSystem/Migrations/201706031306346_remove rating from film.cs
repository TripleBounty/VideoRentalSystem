namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeratingfromfilm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Films", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "Rating", c => c.Single(nullable: false));
        }
    }
}
