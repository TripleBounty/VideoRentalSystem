namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reviews", name: "Cutsomer_Id", newName: "Customer_Id");
            RenameIndex(table: "dbo.Reviews", name: "IX_Cutsomer_Id", newName: "IX_Customer_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reviews", name: "IX_Customer_Id", newName: "IX_Cutsomer_Id");
            RenameColumn(table: "dbo.Reviews", name: "Customer_Id", newName: "Cutsomer_Id");
        }
    }
}
