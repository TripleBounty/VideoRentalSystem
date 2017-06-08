namespace VideoRentalSystem.Migrations.FilmAwardSqLite
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 2147483647),
                        Year = c.String(maxLength: 2147483647),
                        IsDeleted = c.Boolean(nullable: false),
                        OrganisationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organisations", t => t.OrganisationId, cascadeDelete: true)
                .Index(t => t.OrganisationId);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 2147483647),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Awards", "OrganisationId", "dbo.Organisations");
            DropIndex("dbo.Awards", new[] { "OrganisationId" });
            DropTable("dbo.Organisations");
            DropTable("dbo.Awards");
        }
    }
}
