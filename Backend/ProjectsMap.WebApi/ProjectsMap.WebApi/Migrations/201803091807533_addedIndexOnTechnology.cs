namespace ProjectsMap.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIndexOnTechnology : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Technologies", "Name", c => c.String(maxLength: 50));
            CreateIndex("dbo.Technologies", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Technologies", new[] { "Name" });
            AlterColumn("dbo.Technologies", "Name", c => c.String());
        }
    }
}
