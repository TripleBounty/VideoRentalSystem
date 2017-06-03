namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeeTestv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Manager_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Manager_Id");
            AddForeignKey("dbo.Employees", "Manager_Id", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Manager_Id", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Manager_Id" });
            DropColumn("dbo.Employees", "Manager_Id");
        }
    }
}
