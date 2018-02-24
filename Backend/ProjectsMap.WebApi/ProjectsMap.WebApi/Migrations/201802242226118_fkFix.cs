namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "DeveloperId", "dbo.Seats");
            AddColumn("dbo.Seats", "DeveloperId", c => c.Int(nullable: false));
            CreateIndex("dbo.Seats", "DeveloperId");
            AddForeignKey("dbo.Seats", "DeveloperId", "dbo.Developers", "DeveloperId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seats", "DeveloperId", "dbo.Developers");
            DropIndex("dbo.Seats", new[] { "DeveloperId" });
            DropColumn("dbo.Seats", "DeveloperId");
            AddForeignKey("dbo.Developers", "DeveloperId", "dbo.Seats", "SeatId");
        }
    }
}
