namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photoPosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Floors", "XPhoto", c => c.Int());
            AddColumn("dbo.Floors", "YPhoto", c => c.Int());
            DropColumn("dbo.Floors", "PhotoPosition_X");
            DropColumn("dbo.Floors", "PhotoPosition_Y");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Floors", "PhotoPosition_Y", c => c.Int(nullable: false));
            AddColumn("dbo.Floors", "PhotoPosition_X", c => c.Int(nullable: false));
            DropColumn("dbo.Floors", "YPhoto");
            DropColumn("dbo.Floors", "XPhoto");
        }
    }
}
