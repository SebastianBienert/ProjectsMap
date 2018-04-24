using ProjectsMap.WebApi.Repositories.EntityFramework;

namespace ProjectsMap.WebApi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjectsMap.WebApi.Infrastructure;
    using ProjectsMap.WebApi.Repositories;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectsMap.WebApi.Repositories.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectsMap.WebApi.Repositories.EfDbContext context)
        {
            var seed = new ProjectsMapDbInitializer();
            seed.InitializeDatabase(context);
        }
    }
}
