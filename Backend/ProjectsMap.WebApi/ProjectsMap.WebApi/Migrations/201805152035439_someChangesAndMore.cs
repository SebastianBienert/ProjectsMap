namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someChangesAndMore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Floors", "PhotoPosition_X", c => c.Int(nullable: false));
            AddColumn("dbo.Floors", "PhotoPosition_Y", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Floors", "PhotoPosition_Y");
            DropColumn("dbo.Floors", "PhotoPosition_X");
        }
    }
}
