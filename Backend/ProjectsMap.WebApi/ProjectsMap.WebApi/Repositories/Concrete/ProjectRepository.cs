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
                var existingTechnologiesNames = dbContext.Technologies.Select(x => x.Name).ToList();
                var newTechnologiesNames = projectDto.Technologies.Where(x => !existingTechnologiesNames.Contains(x));

                var technologies = dbContext.Technologies.Where(x => projectDto.Technologies.Contains(x.Name)).ToList();
                project = new Project()
                {
                    Technologies = technologies,
                    Company = dbContext.Companies.Where(x => x.CompanyId == projectDto.CompanyId).FirstOrDefault(),
                    CompanyId = projectDto.CompanyId,
                    Description = projectDto.Description,
                    DocumentationLink = projectDto.DocumentationLink,
                    RepositoryLink = projectDto.RepositoryLink
                };
                var roles = new List<ProjectRole>();
                foreach (var devRole in projectDto.EmployeesRoles)
                {
                    var employee = dbContext.Employees.FirstOrDefault(d => d.EmployeeId == devRole.EmployeeId &&
                                                                           d.CompanyId == devRole.CompanyId);
                    var projectRoleEntity = new ProjectRole()
                    {
                        Employee = employee,
                        EmployeeId = devRole.EmployeeId,
                        EmployeeCompanyId = devRole.CompanyId,
                        ProjectId = project.ProjectId,
                        Project = project,
                        Role = devRole.Role
                    };
                    roles.Add(projectRoleEntity);
                    if (employee?.ProjectRoles == null)
                        employee.ProjectRoles = new List<ProjectRole>(){projectRoleEntity};
                    else
                        employee?.ProjectRoles.Add(projectRoleEntity);
                }
                project.ProjectRoles = roles;

                foreach (var tech in newTechnologiesNames)
                {
                    var newTechnology = new Technology()
                    {
                        Name = tech,
                        Projects = new List<Project>() { project }
                    };
                    project.Technologies.Add(newTechnology);
                }

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