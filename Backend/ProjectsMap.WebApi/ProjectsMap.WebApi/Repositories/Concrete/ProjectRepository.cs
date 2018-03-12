using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
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
                return dbContext.Projects
                    .Include(p => p.ProjectRoles).ToList();
            }
        }
        public Project Get(int id)
        {
            var dbContext = new EfDbContext();
            return dbContext.Projects.Include(p => p.ProjectRoles).FirstOrDefault(x => x.ProjectId == id);
        }

        public int Add(CreateProject projectDto)
        {
            Project project;
            using (var dbContext = new EfDbContext())
            {
                var technologiesIds = projectDto.TechnologiesIDs.ToList();
                var technologies = dbContext.Technologies.Where(x => technologiesIds.Contains(x.TechnologyId)).ToList();

                var roles = new List<ProjectRole>();
                foreach (var devRole in projectDto.DevelopersRoles)
                {
                    roles.Add(new ProjectRole()
                    {
                        Employee = dbContext.Developers.FirstOrDefault(d => d.DeveloperId == devRole.DeveloperId &&
                                                                             d.CompanyId == devRole.CompanyId)
                    });
                }
                project = new Project()
                {
                    ProjectRoles = roles,
                    Technologies = technologies,
                    Company = dbContext.Companies.Where(x => x.CompanyId == projectDto.CompanyId).FirstOrDefault(),
                    CompanyId = projectDto.CompanyId,
                    Description = projectDto.Description,
                    DocumentationLink = projectDto.DocumentationLink,
                    RepositoryLink = projectDto.RepositoryLink
                };

                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
            }

            return project.ProjectId;
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