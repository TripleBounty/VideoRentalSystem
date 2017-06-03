namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstoretable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            AddColumn("dbo.Employees", "Store_Id", c => c.Int());
            AddColumn("dbo.Films", "Store_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Store_Id");
            CreateIndex("dbo.Films", "Store_Id");
            AddForeignKey("dbo.Employees", "Store_Id", "dbo.Stores", "Id");
            AddForeignKey("dbo.Films", "Store_Id", "dbo.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Films", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Employees", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Stores", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Stores", new[] { "Address_Id" });
            DropIndex("dbo.Films", new[] { "Store_Id" });
            DropIndex("dbo.Employees", new[] { "Store_Id" });
            DropColumn("dbo.Films", "Store_Id");
            DropColumn("dbo.Employees", "Store_Id");
            DropTable("dbo.Stores");
        }
    }
}
