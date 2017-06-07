namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FilmFilmStaffs", newName: "FilmStaffFilms");
            DropPrimaryKey("dbo.FilmStaffFilms");
            AddColumn("dbo.FilmGenres", "Customer_Id", c => c.Int());
            AddColumn("dbo.Films", "Customer_Id", c => c.Int());
            AddColumn("dbo.Reviews", "Customer_Id", c => c.Int());
            AddPrimaryKey("dbo.FilmStaffFilms", new[] { "FilmStaff_Id", "Film_Id" });
            CreateIndex("dbo.Films", "Customer_Id");
            CreateIndex("dbo.FilmGenres", "Customer_Id");
            CreateIndex("dbo.Reviews", "Customer_Id");
            AddForeignKey("dbo.Films", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.FilmGenres", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Reviews", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.FilmGenres", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Films", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Reviews", new[] { "Customer_Id" });
            DropIndex("dbo.FilmGenres", new[] { "Customer_Id" });
            DropIndex("dbo.Films", new[] { "Customer_Id" });
            DropPrimaryKey("dbo.FilmStaffFilms");
            DropColumn("dbo.Reviews", "Customer_Id");
            DropColumn("dbo.Films", "Customer_Id");
            DropColumn("dbo.FilmGenres", "Customer_Id");
            AddPrimaryKey("dbo.FilmStaffFilms", new[] { "Film_Id", "FilmStaff_Id" });
            RenameTable(name: "dbo.FilmStaffFilms", newName: "FilmFilmStaffs");
        }
    }
}
