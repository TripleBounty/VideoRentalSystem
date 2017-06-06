namespace VideoRentalSystem.Migrations.VideoRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingGenreModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilmRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgeRating = c.String(nullable: false, maxLength: 40),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.FilmGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Genre = c.String(nullable: false, maxLength: 40),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Film_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmGenres", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.FilmRatings", "Film_Id", "dbo.Films");
            DropIndex("dbo.FilmGenres", new[] { "Film_Id" });
            DropIndex("dbo.FilmRatings", new[] { "Film_Id" });
            DropTable("dbo.FilmGenres");
            DropTable("dbo.FilmRatings");
        }
    }
}
