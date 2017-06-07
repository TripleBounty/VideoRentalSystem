namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
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
                        Town_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.Town_Id)
                .Index(t => t.Town_Id);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Code = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_UniqueName")
                .Index(t => t.Code, unique: true, name: "IX_UniqueCode");
            
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Date = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Salary = c.Int(nullable: false),
                        Manager_Id = c.Int(),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Manager_Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.FilmStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        OriginePlace_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.OriginePlace_Id)
                .Index(t => t.OriginePlace_Id);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Summary = c.String(nullable: false, maxLength: 512),
                        ReleaseDate = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Double(nullable: false),
                        Description = c.String(nullable: false, maxLength: 40),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        VideoFormat = c.Int(nullable: false),
                        Film_Id = c.Int(),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Film_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.FilmFilmStaffs",
                c => new
                    {
                        Film_Id = c.Int(nullable: false),
                        FilmStaff_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_Id, t.FilmStaff_Id })
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .ForeignKey("dbo.FilmStaffs", t => t.FilmStaff_Id, cascadeDelete: true)
                .Index(t => t.Film_Id)
                .Index(t => t.FilmStaff_Id);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storages", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Employees", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Stores", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Storages", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Reviews", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.FilmStaffs", "OriginePlace_Id", "dbo.Countries");
            DropForeignKey("dbo.FilmFilmStaffs", "FilmStaff_Id", "dbo.FilmStaffs");
            DropForeignKey("dbo.FilmFilmStaffs", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Awards", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Employees", "Manager_Id", "dbo.Employees");
            DropForeignKey("dbo.Addresses", "Town_Id", "dbo.Towns");
            DropForeignKey("dbo.Towns", "Country_Id", "dbo.Countries");
            DropIndex("dbo.FilmFilmStaffs", new[] { "FilmStaff_Id" });
            DropIndex("dbo.FilmFilmStaffs", new[] { "Film_Id" });
            DropIndex("dbo.Stores", new[] { "Address_Id" });
            DropIndex("dbo.Storages", new[] { "Store_Id" });
            DropIndex("dbo.Storages", new[] { "Film_Id" });
            DropIndex("dbo.Reviews", new[] { "Film_Id" });
            DropIndex("dbo.FilmStaffs", new[] { "OriginePlace_Id" });
            DropIndex("dbo.Employees", new[] { "Store_Id" });
            DropIndex("dbo.Employees", new[] { "Manager_Id" });
            DropIndex("dbo.Awards", new[] { "Film_Id" });
            DropIndex("dbo.Countries", "IX_UniqueCode");
            DropIndex("dbo.Countries", "IX_UniqueName");
            DropIndex("dbo.Towns", new[] { "Country_Id" });
            DropIndex("dbo.Addresses", new[] { "Town_Id" });
            DropTable("dbo.FilmFilmStaffs");
            DropTable("dbo.Stores");
            DropTable("dbo.Storages");
            DropTable("dbo.Reviews");
            DropTable("dbo.Films");
            DropTable("dbo.FilmStaffs");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Awards");
            DropTable("dbo.Countries");
            DropTable("dbo.Towns");
            DropTable("dbo.Addresses");
        }
    }
}
