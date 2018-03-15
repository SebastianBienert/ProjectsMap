using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class TechnologyRepository : ITechnologyRepository
    {
        public IEnumerable<Technology> Technologies
        {
            get
            {
                using (var dbContext = new EfDbContext())
                {
                    return dbContext.Technologies
                        .Include(t => t.Employees)
                        .Include(t => t.Projects)
                        .ToList();
                }
            }
        }

        public Technology Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Technologies
                    .Include(t => t.Employees)
                    .Include(t => t.Projects).
                    FirstOrDefault(t => t.TechnologyId == id);
            }
        }

        //Add technology with existing projects and developers
        public int Add(TechnologyDto technologyDto)
        {
            using (var dbContext = new EfDbContext())
            {
                var developersFromDataBase = dbContext.Employees.Where(x => technologyDto.EmployeesId.Contains(x.EmployeeId)).ToList();
                var projectsFromDatabase = dbContext.Projects.Where(x => technologyDto.ProjectsId.Contains(x.ProjectId)).ToList();
                var technology = new Technology()
                {
                    Name = technologyDto.Name,
                    Employees = technologyDto.EmployeesId.Count() == 0 ? null : developersFromDataBase, 
                    Projects = technologyDto.ProjectsId.Count() == 0 ? null : projectsFromDatabase
                };

                dbContext.Technologies.Add(technology);
                dbContext.SaveChanges();

                return technology.TechnologyId;
            }
        }

        public IEnumerable<Technology> GetTechnologiesByName(string name)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Technologies
                    .Include(t => t.Employees)
                    .Include(t => t.Projects)
                    .Where(t => t.Name.StartsWith(name)).ToList();
            }
        }


        public void Delete(Technology technology)
        {
            throw new NotImplementedException();
        }

        public void Update(Technology technology)
        {
            throw new NotImplementedException();
        }
    }
}