namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.DeveloperId)
                .ForeignKey("dbo.Seats", t => t.DeveloperId)
                .ForeignKey("dbo.Users", t => t.DeveloperId)
                .Index(t => t.DeveloperId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Vertices", t => t.SeatId)
                .Index(t => t.SeatId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Vertices",
                c => new
                    {
                        VertexId = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VertexId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TechnologyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.VertexRoom",
                c => new
                    {
                        VertexRefId = c.Int(nullable: false),
                        RoomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VertexRefId, t.RoomRefId })
                .ForeignKey("dbo.Vertices", t => t.VertexRefId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomRefId, cascadeDelete: true)
                .Index(t => t.VertexRefId)
                .Index(t => t.RoomRefId);
            
            CreateTable(
                "dbo.ProjectRoom",
                c => new
                    {
                        ProjectRefId = c.Int(nullable: false),
                        RoomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectRefId, t.RoomRefId })
                .ForeignKey("dbo.Projects", t => t.ProjectRefId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomRefId, cascadeDelete: true)
                .Index(t => t.ProjectRefId)
                .Index(t => t.RoomRefId);
            
            CreateTable(
                "dbo.ProjectTechnology",
                c => new
                    {
                        ProjectRefId = c.Int(nullable: false),
                        TechnologyRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectRefId, t.TechnologyRefId })
                .ForeignKey("dbo.Projects", t => t.ProjectRefId, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyRefId, cascadeDelete: true)
                .Index(t => t.ProjectRefId)
                .Index(t => t.TechnologyRefId);
            
            CreateTable(
                "dbo.DeveloperProject",
                c => new
                    {
                        DeveloperRefId = c.Int(nullable: false),
                        ProjectRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeveloperRefId, t.ProjectRefId })
                .ForeignKey("dbo.Developers", t => t.DeveloperRefId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectRefId, cascadeDelete: true)
                .Index(t => t.DeveloperRefId)
                .Index(t => t.ProjectRefId);
            
            CreateTable(
                "dbo.DeveloperTechnology",
                c => new
                    {
                        DeveloperRefId = c.Int(nullable: false),
                        TechnologyRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeveloperRefId, t.TechnologyRefId })
                .ForeignKey("dbo.Developers", t => t.DeveloperRefId, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyRefId, cascadeDelete: true)
                .Index(t => t.DeveloperRefId)
                .Index(t => t.TechnologyRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "DeveloperId", "dbo.Users");
            DropForeignKey("dbo.DeveloperTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.DeveloperTechnology", "DeveloperRefId", "dbo.Developers");
            DropForeignKey("dbo.DeveloperProject", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.DeveloperProject", "DeveloperRefId", "dbo.Developers");
            DropForeignKey("dbo.ProjectTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.ProjectTechnology", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.ProjectRoom", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.Seats", "SeatId", "dbo.Vertices");
            DropForeignKey("dbo.VertexRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.VertexRoom", "VertexRefId", "dbo.Vertices");
            DropForeignKey("dbo.Seats", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Developers", "DeveloperId", "dbo.Seats");
            DropIndex("dbo.DeveloperTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.DeveloperTechnology", new[] { "DeveloperRefId" });
            DropIndex("dbo.DeveloperProject", new[] { "ProjectRefId" });
            DropIndex("dbo.DeveloperProject", new[] { "DeveloperRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "ProjectRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "RoomRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "ProjectRefId" });
            DropIndex("dbo.VertexRoom", new[] { "RoomRefId" });
            DropIndex("dbo.VertexRoom", new[] { "VertexRefId" });
            DropIndex("dbo.Seats", new[] { "RoomId" });
            DropIndex("dbo.Seats", new[] { "SeatId" });
            DropIndex("dbo.Developers", new[] { "DeveloperId" });
            DropTable("dbo.DeveloperTechnology");
            DropTable("dbo.DeveloperProject");
            DropTable("dbo.ProjectTechnology");
            DropTable("dbo.ProjectRoom");
            DropTable("dbo.VertexRoom");
            DropTable("dbo.Users");
            DropTable("dbo.Technologies");
            DropTable("dbo.Vertices");
            DropTable("dbo.Seats");
            DropTable("dbo.Rooms");
            DropTable("dbo.Projects");
            DropTable("dbo.Developers");
        }
    }
}
