namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedVertex : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" }, "dbo.Vertices");
            DropForeignKey("dbo.Walls", new[] { "EndVertexX", "EndVertexY" }, "dbo.Vertices");
            DropForeignKey("dbo.Walls", new[] { "StartVertexX", "StartVertexY" }, "dbo.Vertices");
            DropForeignKey("dbo.Seats", new[] { "EmployeeId", "EmployeeCompanyId" }, "dbo.Employees");
            DropIndex("dbo.Seats", new[] { "EmployeeId", "EmployeeCompanyId" });
            DropIndex("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" });
            DropIndex("dbo.Walls", new[] { "StartVertexX", "StartVertexY" });
            DropIndex("dbo.Walls", new[] { "EndVertexX", "EndVertexY" });
            AddColumn("dbo.Seats", "X", c => c.Int(nullable: false));
            AddColumn("dbo.Seats", "Y", c => c.Int(nullable: false));
            AddColumn("dbo.Seats", "Employee_EmployeeId", c => c.Int());
            AddColumn("dbo.Seats", "Employee_CompanyId", c => c.Int());
            AlterColumn("dbo.Walls", "StartVertexX", c => c.Int(nullable: false));
            AlterColumn("dbo.Walls", "StartVertexY", c => c.Int(nullable: false));
            AlterColumn("dbo.Walls", "EndVertexX", c => c.Int(nullable: false));
            AlterColumn("dbo.Walls", "EndVertexY", c => c.Int(nullable: false));
            CreateIndex("dbo.Seats", new[] { "Employee_EmployeeId", "Employee_CompanyId" });
            AddForeignKey("dbo.Seats", new[] { "Employee_EmployeeId", "Employee_CompanyId" }, "dbo.Employees", new[] { "EmployeeId", "CompanyId" });
            DropColumn("dbo.Seats", "Vertex_X");
            DropColumn("dbo.Seats", "Vertex_Y");
            DropTable("dbo.Vertices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vertices",
                c => new
                    {
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.X, t.Y });
            
            AddColumn("dbo.Seats", "Vertex_Y", c => c.Int(nullable: false));
            AddColumn("dbo.Seats", "Vertex_X", c => c.Int(nullable: false));
            DropForeignKey("dbo.Seats", new[] { "Employee_EmployeeId", "Employee_CompanyId" }, "dbo.Employees");
            DropIndex("dbo.Seats", new[] { "Employee_EmployeeId", "Employee_CompanyId" });
            AlterColumn("dbo.Walls", "EndVertexY", c => c.Int());
            AlterColumn("dbo.Walls", "EndVertexX", c => c.Int());
            AlterColumn("dbo.Walls", "StartVertexY", c => c.Int());
            AlterColumn("dbo.Walls", "StartVertexX", c => c.Int());
            DropColumn("dbo.Seats", "Employee_CompanyId");
            DropColumn("dbo.Seats", "Employee_EmployeeId");
            DropColumn("dbo.Seats", "Y");
            DropColumn("dbo.Seats", "X");
            CreateIndex("dbo.Walls", new[] { "EndVertexX", "EndVertexY" });
            CreateIndex("dbo.Walls", new[] { "StartVertexX", "StartVertexY" });
            CreateIndex("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" });
            CreateIndex("dbo.Seats", new[] { "EmployeeId", "EmployeeCompanyId" });
            AddForeignKey("dbo.Seats", new[] { "EmployeeId", "EmployeeCompanyId" }, "dbo.Employees", new[] { "EmployeeId", "CompanyId" });
            AddForeignKey("dbo.Walls", new[] { "StartVertexX", "StartVertexY" }, "dbo.Vertices", new[] { "X", "Y" });
            AddForeignKey("dbo.Walls", new[] { "EndVertexX", "EndVertexY" }, "dbo.Vertices", new[] { "X", "Y" });
            AddForeignKey("dbo.Seats", new[] { "Vertex_X", "Vertex_Y" }, "dbo.Vertices", new[] { "X", "Y" });
        }
    }
}
