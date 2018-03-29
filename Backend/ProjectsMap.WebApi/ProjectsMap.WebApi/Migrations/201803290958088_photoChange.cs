namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photoChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Photo", c => c.Binary());
        }
    }
}
