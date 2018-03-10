namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedProjectRoomRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", new[] { "ProductOwner_DeveloperId", "ProductOwner_CompanyId" }, "dbo.Developers");
            DropForeignKey("dbo.ProjectRoom", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRoom", "RoomRefId", "dbo.Rooms");
            DropIndex("dbo.Projects", new[] { "ProductOwner_DeveloperId", "ProductOwner_CompanyId" });
            DropIndex("dbo.ProjectRoom", new[] { "ProjectRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "RoomRefId" });
            DropColumn("dbo.Projects", "ProductOwner_DeveloperId");
            DropColumn("dbo.Projects", "ProductOwner_CompanyId");
            DropTable("dbo.ProjectRoom");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectRoom",
                c => new
                    {
                        ProjectRefId = c.Int(nullable: false),
                        RoomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectRefId, t.RoomRefId });
            
            AddColumn("dbo.Projects", "ProductOwner_CompanyId", c => c.Int());
            AddColumn("dbo.Projects", "ProductOwner_DeveloperId", c => c.Int());
            CreateIndex("dbo.ProjectRoom", "RoomRefId");
            CreateIndex("dbo.ProjectRoom", "ProjectRefId");
            CreateIndex("dbo.Projects", new[] { "ProductOwner_DeveloperId", "ProductOwner_CompanyId" });
            AddForeignKey("dbo.ProjectRoom", "RoomRefId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.ProjectRoom", "ProjectRefId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", new[] { "ProductOwner_DeveloperId", "ProductOwner_CompanyId" }, "dbo.Developers", new[] { "DeveloperId", "CompanyId" });
        }
    }
}
