using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
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

        public int Add(ProjectDto projectDto)
        {
            Project project;
            using (var dbContext = new EfDbContext())
            {
                var technologiesIds = projectDto.Technologies.Select(z => z.Id);
                var developersIds = projectDto.Developers.Select(z => z.Id);
                var roomsIds = projectDto.Rooms.Select(z => z.Id);

                var technologies = dbContext.Technologies.Where(x => technologiesIds.Contains(x.TechnologyId)).ToList();
                var developers = dbContext.Developers.Where(x => developersIds.Contains(x.DeveloperId)).ToList();
                var rooms = dbContext.Rooms.Where(x => roomsIds.Contains(x.RoomId)).ToList();

                project = new Project()
                {
                    Rooms = rooms,
                    Technologies = technologies,
                    Developers = developers,
                    Company = dbContext.Companies.Where(x => x.CompanyId == projectDto.CompanyId).FirstOrDefault(),
                    CompanyId = projectDto.CompanyId,
                    Description = projectDto.Description,
                    DocumentationLink = projectDto.DocumentationLink,
                    ProductOwner = dbContext.Developers.Where(x => x.DeveloperId == projectDto.ProductOwnerId).FirstOrDefault(),
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