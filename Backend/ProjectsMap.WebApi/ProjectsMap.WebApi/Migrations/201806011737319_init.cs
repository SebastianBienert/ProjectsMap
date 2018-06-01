namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        FloorId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        FloorNumber = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                        Photo = c.String(),
                        XPhoto = c.Int(),
                        YPhoto = c.Int(),
                    })
                .PrimaryKey(t => t.FloorId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
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
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        EmployeeId = c.Int(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        WantToHelp = c.Boolean(nullable: false),
                        ManagerId = c.Int(),
                        Photo = c.String(),
                        JobTitle = c.String(),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .Index(t => t.ManagerId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProjectRoles",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ProjectId })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        RepositoryLink = c.String(),
                        DocumentationLink = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TechnologyId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        WallId = c.Int(nullable: false, identity: true),
                        StartVertexX = c.Int(nullable: false),
                        StartVertexY = c.Int(nullable: false),
                        EndVertexX = c.Int(nullable: false),
                        EndVertexY = c.Int(nullable: false),
                        FloorId = c.Int(),
                    })
                .PrimaryKey(t => t.WallId)
                .ForeignKey("dbo.Floors", t => t.FloorId)
                .Index(t => t.FloorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                "dbo.EmployeeTechnology",
                c => new
                    {
                        EmployeeRefId = c.Int(nullable: false),
                        TechnologyRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeRefId, t.TechnologyRefId })
                .ForeignKey("dbo.Employees", t => t.EmployeeRefId, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyRefId, cascadeDelete: true)
                .Index(t => t.EmployeeRefId)
                .Index(t => t.TechnologyRefId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.WallRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.WallRoom", "WallRefId", "dbo.Walls");
            DropForeignKey("dbo.Walls", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Seats", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Seats", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.EmployeeTechnology", "EmployeeRefId", "dbo.Employees");
            DropForeignKey("dbo.ProjectRoles", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.ProjectTechnology", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRoles", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rooms", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Floors", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.WallRoom", new[] { "RoomRefId" });
            DropIndex("dbo.WallRoom", new[] { "WallRefId" });
            DropIndex("dbo.EmployeeTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.EmployeeTechnology", new[] { "EmployeeRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "ProjectRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Walls", new[] { "FloorId" });
            DropIndex("dbo.Technologies", new[] { "Name" });
            DropIndex("dbo.ProjectRoles", new[] { "ProjectId" });
            DropIndex("dbo.ProjectRoles", new[] { "EmployeeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Employees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Seats", new[] { "Employee_Id" });
            DropIndex("dbo.Seats", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "FloorId" });
            DropIndex("dbo.Floors", new[] { "BuildingId" });
            DropTable("dbo.WallRoom");
            DropTable("dbo.EmployeeTechnology");
            DropTable("dbo.ProjectTechnology");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Walls");
            DropTable("dbo.Technologies");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Employees");
            DropTable("dbo.Seats");
            DropTable("dbo.Rooms");
            DropTable("dbo.Floors");
            DropTable("dbo.Buildings");
        }
    }
}
