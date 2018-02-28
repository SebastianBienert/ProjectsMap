namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expand_Developer_Entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Developers", "Email", c => c.String());
            AddColumn("dbo.Developers", "WantToHelp", c => c.Boolean(nullable: false));
            AddColumn("dbo.Developers", "Photo", c => c.Binary());
            AddColumn("dbo.Developers", "JobTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Developers", "JobTitle");
            DropColumn("dbo.Developers", "Photo");
            DropColumn("dbo.Developers", "WantToHelp");
            DropColumn("dbo.Developers", "Email");
        }
    }
}
