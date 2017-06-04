namespace VideoRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Film_Id", c => c.Int());
            CreateIndex("dbo.Reviews", "Film_Id");
            AddForeignKey("dbo.Reviews", "Film_Id", "dbo.Films", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Film_Id", "dbo.Films");
            DropIndex("dbo.Reviews", new[] { "Film_Id" });
            DropColumn("dbo.Reviews", "Film_Id");
        }
    }
}
