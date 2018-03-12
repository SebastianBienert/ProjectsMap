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
                        CompanyId = c.Int(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        WantToHelp = c.Boolean(nullable: false),
                        Photo = c.Binary(),
                        JobTitle = c.String(),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeveloperId, t.CompanyId })
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.CompanyId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.ProjectRoles",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        DevelopersCompanyId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => new { t.DeveloperId, t.ProjectId })
                .ForeignKey("dbo.Developers", t => new { t.DeveloperId, t.DevelopersCompanyId }, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => new { t.DeveloperId, t.DevelopersCompanyId })
                .Index(t => t.ProjectId);
            
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
                        ProductOwner_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Developers", t => new { t.ProductOwner_DeveloperId, t.ProductOwner_CompanyId })
                .Index(t => t.CompanyId)
                .Index(t => new { t.ProductOwner_DeveloperId, t.ProductOwner_CompanyId });
            
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
                "dbo.Walls",
                c => new
                    {
                        WallId = c.Int(nullable: false, identity: true),
                        StartVertexX = c.Int(),
                        StartVertexY = c.Int(),
                        EndVertexX = c.Int(),
                        EndVertexY = c.Int(),
                        FloorId = c.Int(),
                    })
                .PrimaryKey(t => t.WallId)
                .ForeignKey("dbo.Vertices", t => new { t.EndVertexX, t.EndVertexY })
                .ForeignKey("dbo.Floors", t => t.FloorId)
                .ForeignKey("dbo.Vertices", t => new { t.StartVertexX, t.StartVertexY })
                .Index(t => new { t.StartVertexX, t.StartVertexY })
                .Index(t => new { t.EndVertexX, t.EndVertexY })
                .Index(t => t.FloorId);
            
            CreateTable(
                "dbo.Vertices",
                c => new
                    {
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.X, t.Y });
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        DeveloperId = c.Int(),
                        DeveloperCompanyId = c.Int(),
                        Vertex_X = c.Int(nullable: false),
                        Vertex_Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Developers", t => new { t.DeveloperId, t.DeveloperCompanyId })
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Vertices", t => new { t.Vertex_X, t.Vertex_Y })
                .Index(t => t.RoomId)
                .Index(t => new { t.DeveloperId, t.DeveloperCompanyId })
                .Index(t => new { t.Vertex_X, t.Vertex_Y });
            
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
                "dbo.WallRoom",
                c => new
                    {
                        WallRefId = c.Int(nullable: false),
                        RoomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WallRefId, t.RoomRefId })
                .ForeignKey("dbo.Walls", t => t.WallRefId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomRefId, cascadeDelete: true)
                .Index(t => t.WallRefId)
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
                "dbo.DeveloperTechnology",
                c => new
                    {
                        DeveloperRefId = c.Int(nullable: false),
                        DeveloperCompanyRefId = c.Int(nullable: false),
                        TechnologyRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeveloperRefId, t.DeveloperCompanyRefId, t.TechnologyRefId })
                .ForeignKey("dbo.Developers", t => new { t.DeveloperRefId, t.DeveloperCompanyRefId }, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyRefId, cascadeDelete: true)
                .Index(t => new { t.DeveloperRefId, t.DeveloperCompanyRefId })
                .Index(t => t.TechnologyRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buildings", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Developers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.DeveloperTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.DeveloperTechnology", new[] { "DeveloperRefId", "DeveloperCompanyRefId" }, "dbo.Developers");
            DropForeignKey("dbo.ProjectRoles", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.ProjectTechnology", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.ProjectRoom", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.Rooms", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Walls", new[] { "StartVertexX", "StartVertexY" }, "dbo.Vertices");
            DropForeignKey("dbo.WallRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.WallRoom", "WallRefId", "dbo.Walls");
            DropForeignKey("dbo.Walls", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Walls", new[] { "EndVertexX", "EndVertexY" }, "dbo.Vertices");
            DropForeignKey("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" }, "dbo.Vertices");
            DropForeignKey("dbo.Seats", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Seats", new[] { "DeveloperId", "DeveloperCompanyId" }, "dbo.Developers");
            DropForeignKey("dbo.Floors", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Projects", new[] { "ProductOwner_DeveloperId", "ProductOwner_CompanyId" }, "dbo.Developers");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.ProjectRoles", new[] { "DeveloperId", "DevelopersCompanyId" }, "dbo.Developers");
            DropForeignKey("dbo.Developers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.DeveloperTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.DeveloperTechnology", new[] { "DeveloperRefId", "DeveloperCompanyRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "ProjectRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "RoomRefId" });
            DropIndex("dbo.ProjectRoom", new[] { "ProjectRefId" });
            DropIndex("dbo.WallRoom", new[] { "RoomRefId" });
            DropIndex("dbo.WallRoom", new[] { "WallRefId" });
            DropIndex("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" });
            DropIndex("dbo.Seats", new[] { "DeveloperId", "DeveloperCompanyId" });
            DropIndex("dbo.Seats", new[] { "RoomId" });
            DropIndex("dbo.Walls", new[] { "FloorId" });
            DropIndex("dbo.Walls", new[] { "EndVertexX", "EndVertexY" });
            DropIndex("dbo.Walls", new[] { "StartVertexX", "StartVertexY" });
            DropIndex("dbo.Floors", new[] { "BuildingId" });
            DropIndex("dbo.Rooms", new[] { "FloorId" });
            DropIndex("dbo.Projects", new[] { "ProductOwner_DeveloperId", "ProductOwner_CompanyId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.ProjectRoles", new[] { "ProjectId" });
            DropIndex("dbo.ProjectRoles", new[] { "DeveloperId", "DevelopersCompanyId" });
            DropIndex("dbo.Developers", new[] { "User_UserId" });
            DropIndex("dbo.Developers", new[] { "CompanyId" });
            DropIndex("dbo.Buildings", new[] { "CompanyId" });
            DropTable("dbo.DeveloperTechnology");
            DropTable("dbo.ProjectTechnology");
            DropTable("dbo.ProjectRoom");
            DropTable("dbo.WallRoom");
            DropTable("dbo.Users");
            DropTable("dbo.Technologies");
            DropTable("dbo.Seats");
            DropTable("dbo.Vertices");
            DropTable("dbo.Walls");
            DropTable("dbo.Floors");
            DropTable("dbo.Rooms");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectRoles");
            DropTable("dbo.Developers");
            DropTable("dbo.Companies");
            DropTable("dbo.Buildings");
        }
    }
}
