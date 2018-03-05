namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wallAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VertexRoom", new[] { "VertexRefIdX", "VertexRefIdY" }, "dbo.Vertices");
            DropForeignKey("dbo.VertexRoom", "RoomRefId", "dbo.Rooms");
            DropIndex("dbo.VertexRoom", new[] { "VertexRefIdX", "VertexRefIdY" });
            DropIndex("dbo.VertexRoom", new[] { "RoomRefId" });
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        WallId = c.Int(nullable: false, identity: true),
                        StartVertexX = c.Int(),
                        StartVertexY = c.Int(),
                        EndVertexX = c.Int(),
                        EndVertexY = c.Int(),
                    })
                .PrimaryKey(t => t.WallId)
                .ForeignKey("dbo.Vertices", t => new { t.EndVertexX, t.EndVertexY })
                .ForeignKey("dbo.Vertices", t => new { t.StartVertexX, t.StartVertexY })
                .Index(t => new { t.StartVertexX, t.StartVertexY })
                .Index(t => new { t.EndVertexX, t.EndVertexY });
            
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
            
            DropTable("dbo.VertexRoom");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VertexRoom",
                c => new
                    {
                        VertexRefIdX = c.Int(nullable: false),
                        VertexRefIdY = c.Int(nullable: false),
                        RoomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VertexRefIdX, t.VertexRefIdY, t.RoomRefId });
            
            DropForeignKey("dbo.Walls", new[] { "StartVertexX", "StartVertexY" }, "dbo.Vertices");
            DropForeignKey("dbo.WallRoom", "RoomRefId", "dbo.Rooms");
            DropForeignKey("dbo.WallRoom", "WallRefId", "dbo.Walls");
            DropForeignKey("dbo.Walls", new[] { "EndVertexX", "EndVertexY" }, "dbo.Vertices");
            DropIndex("dbo.WallRoom", new[] { "RoomRefId" });
            DropIndex("dbo.WallRoom", new[] { "WallRefId" });
            DropIndex("dbo.Walls", new[] { "EndVertexX", "EndVertexY" });
            DropIndex("dbo.Walls", new[] { "StartVertexX", "StartVertexY" });
            DropTable("dbo.WallRoom");
            DropTable("dbo.Walls");
            CreateIndex("dbo.VertexRoom", "RoomRefId");
            CreateIndex("dbo.VertexRoom", new[] { "VertexRefIdX", "VertexRefIdY" });
            AddForeignKey("dbo.VertexRoom", "RoomRefId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.VertexRoom", new[] { "VertexRefIdX", "VertexRefIdY" }, "dbo.Vertices", new[] { "X", "Y" }, cascadeDelete: true);
        }
    }
}
