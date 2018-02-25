using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class ProjectRepository : IProjectRepository
    {
        public IEnumerable<Project> Projects
        {
            get
            {
                var dbContext = new EfDbContext();
                return dbContext.Projects.ToList();
            }
        }
        public Project Get(int id)
        {
            var dbContext = new EfDbContext();
            return dbContext.Projects.FirstOrDefault(x => x.ProjectId == id);
        }

        public void Add(Project project)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Project project)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Projects.Remove(project);
                dbContext.SaveChanges();
            }
        }

        public void Update(Project project)
        {
            using (var dbContext = new EfDbContext())
            {
                var dev = dbContext.Projects.FirstOrDefault(x => x.ProjectId == project.ProjectId);
                dev = project;
                dbContext.SaveChanges();
            }
        }
    }
}