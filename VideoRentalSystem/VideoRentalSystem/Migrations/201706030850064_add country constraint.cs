namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcountryconstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false, maxLength: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "Code", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
        }
    }
}
