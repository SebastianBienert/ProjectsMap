namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameDevToEmployee : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Developers", newName: "Employees");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Employees", newName: "Developers");
        }
    }
}
