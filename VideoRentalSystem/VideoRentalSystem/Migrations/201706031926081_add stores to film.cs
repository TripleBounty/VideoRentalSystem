namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstorestofilm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Films", "Store_Id", "dbo.Stores");
            DropIndex("dbo.Films", new[] { "Store_Id" });
            CreateTable(
                "dbo.StoreFilms",
                c => new
                    {
                        Store_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Store_Id, t.Film_Id })
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .Index(t => t.Store_Id)
                .Index(t => t.Film_Id);
            
            DropColumn("dbo.Films", "Store_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "Store_Id", c => c.Int());
            DropForeignKey("dbo.StoreFilms", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.StoreFilms", "Store_Id", "dbo.Stores");
            DropIndex("dbo.StoreFilms", new[] { "Film_Id" });
            DropIndex("dbo.StoreFilms", new[] { "Store_Id" });
            DropTable("dbo.StoreFilms");
            CreateIndex("dbo.Films", "Store_Id");
            AddForeignKey("dbo.Films", "Store_Id", "dbo.Stores", "Id");
        }
    }
}
