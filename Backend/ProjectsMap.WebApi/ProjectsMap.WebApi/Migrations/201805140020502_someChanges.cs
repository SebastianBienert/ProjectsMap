namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Floors", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Floors", "Photo");
        }
    }
}
