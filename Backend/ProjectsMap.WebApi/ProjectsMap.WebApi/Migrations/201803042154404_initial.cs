namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        WantToHelp = c.Boolean(nullable: false),
                        Photo = c.Binary(),
                        JobTitle = c.String(),
                        CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.DeveloperId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Users", t => t.DeveloperId)
                .Index(t => t.DeveloperId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        RepositoryLink = c.String(),
                        DocumentationLink = c.String(),
                        CompanyId = c.Int(),
                        ProductOwner_DeveloperId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Developers", t => t.ProductOwner_DeveloperId)
                .Index(t => t.CompanyId)
                .Index(t => t.ProductOwner_DeveloperId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        FloorId = c.Int(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Floors", t => t.FloorId)
                .Index(t => t.FloorId);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        FloorId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FloorId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        DeveloperId = c.Int(),
                        Vertex_X = c.Int(nullable: false),
                        Vertex_Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Vertices", t => new { t.Vertex_X, t.Vertex_Y })
                .Index(t => t.RoomId)
                .Index(t => t.DeveloperId)
                .Index(t => new { t.Vertex_X, t.Vertex_Y });
            
            CreateTable(
                "dbo.Vertices",
                c => new
                    {
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.X, t.Y });
            
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
                        VertexRefIdX = c.Int(nullable: false),
                        VertexRefIdY = c.Int(nullable: false),
                        RoomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VertexRefIdX, t.VertexRefIdY, t.RoomRefId })
                .ForeignKey("dbo.Vertices", t => new { t.VertexRefIdX, t.VertexRefIdY }, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomRefId, cascadeDelete: true)
                .Index(t => new { t.VertexRefIdX, t.VertexRefIdY })
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
            DropForeignKey("dbo.Buildings", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Developers", "DeveloperId", "dbo.Users");
            DropForeignKey("dbo.DeveloperTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.DeveloperTechnology", "DeveloperRefId", "dbo.Developers");
            DropForeignKey("dbo.DeveloperProject", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.DeveloperProject", "DeveloperRefId", "dbo.Developers");
            DropForeignKey("dbo.ProjectTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.ProjectTechnology", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.ProjectRoom", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" }, "dbo.Vertices");
            DropForeignKey("dbo.VertexRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.VertexRoom", new[] { "VertexRefIdX", "VertexRefIdY" }, "dbo.Vertices");
            DropForeignKey("dbo.Seats", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Seats", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.Rooms", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Floors", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Projects", "ProductOwner_DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Developers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.DeveloperTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.DeveloperTechnology", new[] { "DeveloperRefId" });
            DropIndex("dbo.DeveloperProject", new[] { "ProjectRefId" });
            DropIndex("dbo.DeveloperProject", new[] { "DeveloperRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "ProjectRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "RoomRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "ProjectRefId" });
            DropIndex("dbo.VertexRoom", new[] { "RoomRefId" });
            DropIndex("dbo.VertexRoom", new[] { "VertexRefIdX", "VertexRefIdY" });
            DropIndex("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" });
            DropIndex("dbo.Seats", new[] { "DeveloperId" });
            DropIndex("dbo.Seats", new[] { "RoomId" });
            DropIndex("dbo.Floors", new[] { "BuildingId" });
            DropIndex("dbo.Rooms", new[] { "FloorId" });
            DropIndex("dbo.Projects", new[] { "ProductOwner_DeveloperId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.Developers", new[] { "CompanyId" });
            DropIndex("dbo.Developers", new[] { "DeveloperId" });
            DropIndex("dbo.Buildings", new[] { "CompanyId" });
            DropTable("dbo.DeveloperTechnology");
            DropTable("dbo.DeveloperProject");
            DropTable("dbo.ProjectTechnology");
            DropTable("dbo.ProjectRoom");
            DropTable("dbo.VertexRoom");
            DropTable("dbo.Users");
            DropTable("dbo.Technologies");
            DropTable("dbo.Vertices");
            DropTable("dbo.Seats");
            DropTable("dbo.Floors");
            DropTable("dbo.Rooms");
            DropTable("dbo.Projects");
            DropTable("dbo.Developers");
            DropTable("dbo.Companies");
            DropTable("dbo.Buildings");
        }
    }
}
