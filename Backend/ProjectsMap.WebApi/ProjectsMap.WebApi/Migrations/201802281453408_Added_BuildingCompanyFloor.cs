namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_BuildingCompanyFloor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.BuildingId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        FloorId = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FloorId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            AddColumn("dbo.Developers", "CompanyId", c => c.Int());
            AddColumn("dbo.Projects", "CompanyId", c => c.Int());
            AddColumn("dbo.Rooms", "FloorId", c => c.Int());
            CreateIndex("dbo.Developers", "CompanyId");
            CreateIndex("dbo.Rooms", "FloorId");
            CreateIndex("dbo.Projects", "CompanyId");
            AddForeignKey("dbo.Rooms", "FloorId", "dbo.Floors", "FloorId");
            AddForeignKey("dbo.Projects", "CompanyId", "dbo.Companies", "CompanyId");
            AddForeignKey("dbo.Developers", "CompanyId", "dbo.Companies", "CompanyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Rooms", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Floors", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Buildings", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.Rooms", new[] { "FloorId" });
            DropIndex("dbo.Floors", new[] { "BuildingId" });
            DropIndex("dbo.Buildings", new[] { "CompanyId" });
            DropIndex("dbo.Developers", new[] { "CompanyId" });
            DropColumn("dbo.Rooms", "FloorId");
            DropColumn("dbo.Projects", "CompanyId");
            DropColumn("dbo.Developers", "CompanyId");
            DropTable("dbo.Floors");
            DropTable("dbo.Buildings");
            DropTable("dbo.Companies");
        }
    }
}
