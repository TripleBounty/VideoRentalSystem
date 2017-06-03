namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employees_Reviews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Manager_Id", c => c.Int());
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
            DropColumn("dbo.Employees", "Manager_Id");
        }
    }
}
