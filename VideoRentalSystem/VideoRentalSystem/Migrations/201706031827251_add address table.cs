namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addaddresstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostalCode = c.String(maxLength: 10),
                        Street = c.String(nullable: false, maxLength: 150),
                        Country_Id = c.Int(),
                        Town_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.Towns", t => t.Town_Id)
                .Index(t => t.Country_Id)
                .Index(t => t.Town_Id);
            
            AlterColumn("dbo.Towns", "Name", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Town_Id", "dbo.Towns");
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Addresses", new[] { "Town_Id" });
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            AlterColumn("dbo.Towns", "Name", c => c.String());
            DropTable("dbo.Addresses");
        }
    }
}
