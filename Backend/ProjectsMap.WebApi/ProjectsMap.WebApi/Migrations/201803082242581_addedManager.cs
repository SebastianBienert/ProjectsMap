namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedManager : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Developers", "ManagerId", c => c.Int());
            AddColumn("dbo.Developers", "ManagerCompanyId", c => c.Int());
            CreateIndex("dbo.Developers", new[] { "ManagerId", "ManagerCompanyId" });
            AddForeignKey("dbo.Developers", new[] { "ManagerId", "ManagerCompanyId" }, "dbo.Developers", new[] { "DeveloperId", "CompanyId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", new[] { "ManagerId", "ManagerCompanyId" }, "dbo.Developers");
            DropIndex("dbo.Developers", new[] { "ManagerId", "ManagerCompanyId" });
            DropColumn("dbo.Developers", "ManagerCompanyId");
            DropColumn("dbo.Developers", "ManagerId");
        }
    }
}
