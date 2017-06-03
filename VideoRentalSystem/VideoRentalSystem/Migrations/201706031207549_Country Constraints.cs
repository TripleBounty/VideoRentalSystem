namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryConstraints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Manager_Id", c => c.Int());
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Reviews", "Description", c => c.String(nullable: false, maxLength: 40));
            CreateIndex("dbo.Employees", "Manager_Id");
            AddForeignKey("dbo.Employees", "Manager_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "ManagerId");
            DropColumn("dbo.Reviews", "FilmId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "FilmId", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "ManagerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "Manager_Id", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Manager_Id" });
            AlterColumn("dbo.Reviews", "Description", c => c.String());
            AlterColumn("dbo.Countries", "Code", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            DropColumn("dbo.Employees", "Manager_Id");
        }
    }
}
