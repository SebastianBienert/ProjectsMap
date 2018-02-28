namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expand_Project_Entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "RepositoryLink", c => c.String());
            AddColumn("dbo.Projects", "DocumentationLink", c => c.String());
            AddColumn("dbo.Projects", "ProductOwner_DeveloperId", c => c.Int());
            CreateIndex("dbo.Projects", "ProductOwner_DeveloperId");
            AddForeignKey("dbo.Projects", "ProductOwner_DeveloperId", "dbo.Developers", "DeveloperId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProductOwner_DeveloperId", "dbo.Developers");
            DropIndex("dbo.Projects", new[] { "ProductOwner_DeveloperId" });
            DropColumn("dbo.Projects", "ProductOwner_DeveloperId");
            DropColumn("dbo.Projects", "DocumentationLink");
            DropColumn("dbo.Projects", "RepositoryLink");
        }
    }
}
