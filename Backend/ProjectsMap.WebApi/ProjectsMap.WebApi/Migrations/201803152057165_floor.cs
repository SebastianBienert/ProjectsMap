namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class floor : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.Floors", "FloorNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Floors", "FloorNumber");
        }
    }
}
