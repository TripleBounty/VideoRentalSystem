namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Films", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.FilmGenres", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Films", new[] { "Customer_Id" });
            DropIndex("dbo.FilmGenres", new[] { "Customer_Id" });
            CreateTable(
                "dbo.FilmCustomers",
                c => new
                    {
                        Film_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_Id, t.Customer_Id })
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Film_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.FilmGenreCustomers",
                c => new
                    {
                        FilmGenre_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmGenre_Id, t.Customer_Id })
                .ForeignKey("dbo.FilmGenres", t => t.FilmGenre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.FilmGenre_Id)
                .Index(t => t.Customer_Id);
            
            DropColumn("dbo.Films", "Customer_Id");
            DropColumn("dbo.FilmGenres", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilmGenres", "Customer_Id", c => c.Int());
            AddColumn("dbo.Films", "Customer_Id", c => c.Int());
            DropForeignKey("dbo.FilmGenreCustomers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.FilmGenreCustomers", "FilmGenre_Id", "dbo.FilmGenres");
            DropForeignKey("dbo.FilmCustomers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.FilmCustomers", "Film_Id", "dbo.Films");
            DropIndex("dbo.FilmGenreCustomers", new[] { "Customer_Id" });
            DropIndex("dbo.FilmGenreCustomers", new[] { "FilmGenre_Id" });
            DropIndex("dbo.FilmCustomers", new[] { "Customer_Id" });
            DropIndex("dbo.FilmCustomers", new[] { "Film_Id" });
            DropTable("dbo.FilmGenreCustomers");
            DropTable("dbo.FilmCustomers");
            CreateIndex("dbo.FilmGenres", "Customer_Id");
            CreateIndex("dbo.Films", "Customer_Id");
            AddForeignKey("dbo.FilmGenres", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Films", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
