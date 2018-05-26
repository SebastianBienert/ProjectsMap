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
                    .Include(p => p.ProjectRoles.Select(x => x.Employee))
                    .Include(p => p.Technologies.Select(x => x.Projects))
                    .Include(p => p.Technologies.Select(x => x.Employees))
                    .ToList();
            }
        }
        public Project Get(int id)
        {
            var dbContext = new EfDbContext();
            return dbContext.Projects
                    .Include(p => p.ProjectRoles.Select(x => x.Employee))
                    .Include(p => p.Technologies.Select(x => x.Projects))
                    .Include(p => p.Technologies.Select(x => x.Employees))
                    .FirstOrDefault(x => x.ProjectId == id);
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
                    Description = projectDto.Description,
                    DocumentationLink = projectDto.DocumentationLink,
                    RepositoryLink = projectDto.RepositoryLink
                };
                var roles = new List<ProjectRole>();
                foreach (var devRole in projectDto.EmployeesRoles)
                {
                    var employee = dbContext.Employees.FirstOrDefault(d => d.EmployeeId == devRole.EmployeeId);
                    var projectRoleEntity = new ProjectRole()
                    {
                        Employee = employee,
                        EmployeeId = devRole.EmployeeId,
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

        public void Delete(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                var entityToRemove = dbContext.Projects.FirstOrDefault(x => x.ProjectId == id);
                if (entityToRemove != null)
                {
                    dbContext.Projects.Remove(entityToRemove);
                    dbContext.SaveChanges();
                }
             }
        }

        public Project Edit(CreateProject dtoProject)
        {
            using (var dbContext = new EfDbContext())
            {
                var project = dbContext.Projects
                    .Include(p => p.ProjectRoles.Select(x => x.Employee))
                    .Include(p => p.Technologies)
                    .FirstOrDefault(x => x.ProjectId == dtoProject.Id);
                if (project != null)
                {
                    dbContext.Projects.Attach(project);
                    var existingTechnologiesNames = dbContext.Technologies.Select(x => x.Name).ToList();
                    var newTechnologiesNames = dtoProject.Technologies.Where(x => !existingTechnologiesNames.Contains(x));
                    var technologies = dbContext.Technologies.Where(x => dtoProject.Technologies.Contains(x.Name)).ToList();
                    project.RepositoryLink = dtoProject.RepositoryLink;
                    project.DocumentationLink = dtoProject.DocumentationLink;
                    project.Description = dtoProject.Description;
                    project.Technologies = technologies;

                    foreach (var tech in newTechnologiesNames)
                    {
                        var newTechnology = new Technology()
                        {
                            Name = tech,
                            Projects = new List<Project>() { project }
                        };
                        project.Technologies.Add(newTechnology);
                    }

                    //EDITING EMPLOYEES
                    var employeesRemoved = project.ProjectRoles.Where(pr => !dtoProject.EmployeesRoles
                        .Select(er => er.EmployeeId).Contains(pr.EmployeeId)).ToList();
                    for(int i = 0; i < employeesRemoved.Count(); i++)
                    {
                        project.ProjectRoles.Remove(employeesRemoved[i]);
                    }

                    var employeesAdded = dtoProject.EmployeesRoles.Where(er =>
                        !project.ProjectRoles.Select(pr => pr.EmployeeId).Contains(er.EmployeeId));
                    foreach (var added in employeesAdded)
                    {
                        var projectRole = new ProjectRole()
                        {
                            Employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == added.EmployeeId),
                            Project = project,
                            Role = added.Role
                        };
                        project.ProjectRoles.Add(projectRole);
                    }

                    dbContext.SaveChanges();
                }
                return project;
            }

        }
    }
}