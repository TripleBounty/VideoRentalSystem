namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class increasecountryname : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Countries", "IX_UniqueName");
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.Countries", "Name", unique: true, name: "IX_UniqueName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Countries", "IX_UniqueName");
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 40));
            CreateIndex("dbo.Countries", "Name", unique: true, name: "IX_UniqueName");
        }
    }
}
