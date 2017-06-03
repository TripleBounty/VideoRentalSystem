namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFilmmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Summary = c.String(),
                        RealieseDate = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        VideoFormats = c.Int(nullable: false),
                        InStore = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilmId = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Awards", "Film_Id", "dbo.Films");
            DropIndex("dbo.Awards", new[] { "Film_Id" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Awards");
            DropTable("dbo.Films");
        }
    }
}
