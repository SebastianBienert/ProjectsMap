namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wallFloor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Walls", "FloorId", c => c.Int());
            CreateIndex("dbo.Walls", "FloorId");
            AddForeignKey("dbo.Walls", "FloorId", "dbo.Floors", "FloorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Walls", "FloorId", "dbo.Floors");
            DropIndex("dbo.Walls", new[] { "FloorId" });
            DropColumn("dbo.Walls", "FloorId");
        }
    }
}
