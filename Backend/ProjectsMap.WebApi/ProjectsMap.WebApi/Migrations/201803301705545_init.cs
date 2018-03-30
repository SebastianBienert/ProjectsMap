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
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        WantToHelp = c.Boolean(nullable: false),
                        ManagerId = c.Int(),
                        ManagerCompanyId = c.Int(),
                        Photo = c.Binary(),
                        JobTitle = c.String(),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.CompanyId })
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => new { t.ManagerId, t.ManagerCompanyId })
                .Index(t => t.CompanyId)
                .Index(t => new { t.ManagerId, t.ManagerCompanyId });
            
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
                        Employee_EmployeeId = c.Int(nullable: false),
                        Employee_CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => new { t.Employee_EmployeeId, t.Employee_CompanyId })
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => new { t.Employee_EmployeeId, t.Employee_CompanyId });
            
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
                        EmployeeCompanyId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ProjectId })
                .ForeignKey("dbo.Employees", t => new { t.EmployeeId, t.EmployeeCompanyId }, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => new { t.EmployeeId, t.EmployeeCompanyId })
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
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
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
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        EmployeeId = c.Int(),
                        EmployeeCompanyId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                        Employee_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Employees", t => new { t.Employee_EmployeeId, t.Employee_CompanyId })
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => new { t.Employee_EmployeeId, t.Employee_CompanyId });
            
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
                        FloorNumber = c.Int(nullable: false),
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
                "dbo.EmployeeTechnology",
                c => new
                    {
                        EmployeeRefId = c.Int(nullable: false),
                        EmployeeCompanyRefId = c.Int(nullable: false),
                        TechnologyRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeRefId, t.EmployeeCompanyRefId, t.TechnologyRefId })
                .ForeignKey("dbo.Employees", t => new { t.EmployeeRefId, t.EmployeeCompanyRefId }, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyRefId, cascadeDelete: true)
                .Index(t => new { t.EmployeeRefId, t.EmployeeCompanyRefId })
                .Index(t => t.TechnologyRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Buildings", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmployeeTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.EmployeeTechnology", new[] { "EmployeeRefId", "EmployeeCompanyRefId" }, "dbo.Employees");
            DropForeignKey("dbo.Seats", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.WallRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.WallRoom", "WallRefId", "dbo.Walls");
            DropForeignKey("dbo.Walls", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Floors", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Seats", new[] { "Employee_EmployeeId", "Employee_CompanyId" }, "dbo.Employees");
            DropForeignKey("dbo.ProjectRoles", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectTechnology", "TechnologyRefId", "dbo.Technologies");
            DropForeignKey("dbo.ProjectTechnology", "ProjectRefId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.ProjectRoles", new[] { "EmployeeId", "EmployeeCompanyId" }, "dbo.Employees");
            DropForeignKey("dbo.Employees", new[] { "ManagerId", "ManagerCompanyId" }, "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "Employee_EmployeeId", "Employee_CompanyId" }, "dbo.Employees");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.EmployeeTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.EmployeeTechnology", new[] { "EmployeeRefId", "EmployeeCompanyRefId" });
            DropIndex("dbo.WallRoom", new[] { "RoomRefId" });
            DropIndex("dbo.WallRoom", new[] { "WallRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "TechnologyRefId" });
            DropIndex("dbo.ProjectTechnology", new[] { "ProjectRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Walls", new[] { "FloorId" });
            DropIndex("dbo.Floors", new[] { "BuildingId" });
            DropIndex("dbo.Rooms", new[] { "FloorId" });
            DropIndex("dbo.Seats", new[] { "Employee_EmployeeId", "Employee_CompanyId" });
            DropIndex("dbo.Seats", new[] { "RoomId" });
            DropIndex("dbo.Technologies", new[] { "Name" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.ProjectRoles", new[] { "ProjectId" });
            DropIndex("dbo.ProjectRoles", new[] { "EmployeeId", "EmployeeCompanyId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Employee_EmployeeId", "Employee_CompanyId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Employees", new[] { "ManagerId", "ManagerCompanyId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropIndex("dbo.Buildings", new[] { "CompanyId" });
            DropTable("dbo.EmployeeTechnology");
            DropTable("dbo.WallRoom");
            DropTable("dbo.ProjectTechnology");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Walls");
            DropTable("dbo.Floors");
            DropTable("dbo.Rooms");
            DropTable("dbo.Seats");
            DropTable("dbo.Technologies");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.Buildings");
        }
    }
}
